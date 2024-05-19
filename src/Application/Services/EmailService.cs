using MailKit.Net.Smtp;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using MimeKit;
using PalpiteFC.Api.Application.Interfaces;
using PalpiteFC.Api.Application.Requests;
using PalpiteFC.Api.CrossCutting.Errors;
using PalpiteFC.Api.CrossCutting.Result;
using PalpiteFC.Api.CrossCutting.Settings;

namespace PalpiteFC.Api.Application.Services;

public class EmailService : IEmailService
{
    private readonly IUserService _userService;
    private readonly IDistributedCache _cache;
    private readonly IOptions<MailingSettings> _options;

    public EmailService(IUserService userService, IDistributedCache cache, IOptions<MailingSettings> options)
    {
        _userService = userService;
        _cache = cache;
        _options = options;
    }

    public async Task<Result<string>> SendEmailCodeAsync(SendEmailCodeRequest request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetByEmail(request.Email, cancellationToken);

        if (user is null)
        {
            return ResultHelper.Failure<string>(SignInErrors.UserNotFound);
        }

        var code = new Random().Next(111111, 999999).ToString();
        var expiration = DateTimeOffset.UtcNow.AddHours(1);

        var message = new MimeMessage();

        message.From.Add(new MailboxAddress(_options.Value.Name, _options.Value.Address));
        message.To.Add(new MailboxAddress(user.Data.Name ?? request.Email, request.Email));

        message.Subject = "[Palpitefc] Solicitação de reset de senha";
        message.Body = new TextPart("html")
        {
            Text = @$"<p>
                        Foi solicitada uma alteração de senha do seu acesso!<br>
                        Seu código para alteração de senha é <b>{code}</b> <br><br>
                        
                        Esse código é válido até {expiration.AddHours(-3):dd/MM/yyyy HH:mm:ss} (horário de Brasília).
                      </p>"
        };

        using var client = new SmtpClient();

        client.Connect(_options.Value.Host, _options.Value.Port, _options.Value.UseSsl, cancellationToken);
        client.Authenticate(_options.Value.Address, _options.Value.Password, cancellationToken);

        await client.SendAsync(message, cancellationToken);

        client.Disconnect(true, cancellationToken);

        _cache.SetString($"PasswordReset:{request.Email}", code, new() { AbsoluteExpiration = expiration });

        return ResultHelper.Success($"O código foi enviado para o email {request.Email}");
    }
}
