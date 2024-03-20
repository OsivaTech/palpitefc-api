﻿using MailKit.Net.Smtp;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using MimeKit;
using PalpiteApi.Application.Interfaces;
using PalpiteApi.Application.Requests;
using PalpiteApi.Domain.Errors;
using PalpiteApi.Domain.Result;
using PalpiteApi.Domain.Settings;

namespace PalpiteApi.Application.Services;

public class EmailService : IEmailService
{
    private readonly IUserService _userService;
    private readonly IMemoryCache _cache;
    private readonly IOptions<MailingSettings> _options;

    public EmailService(IUserService userService, IMemoryCache cache, IOptions<MailingSettings> options)
    {
        _userService = userService;
        _cache = cache;
        _options = options;
    }

    public async Task<Result<string>> SendEmailCodeAsync(SendEmailCodeRequest request, CancellationToken cancellationToken)
    {
        var user = await _userService.GetByEmail(request.Email!);

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

        _cache.Set($"PasswordReset:{request.Email}", code, expiration);

        return ResultHelper.Success($"O código foi enviado para o email {request.Email}");
    }
}