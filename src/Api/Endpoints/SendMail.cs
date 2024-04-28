using PalpiteFC.Api.Application.Interfaces;
using PalpiteFC.Api.Application.Requests;

namespace PalpiteFC.Api.Endpoints;

public static class SendMail
{
    #region Public Methods

    public static void MapSendEmailEndpoints(this WebApplication app)
    {
        app.MapPost("/sendemailcode", SendAsync)
           .WithSummary("Send an email code.")
           .WithOpenApi();
    }

    #endregion

    #region Non-Public Methods

    private async static Task<IResult> SendAsync(SendEmailCodeRequest request, IEmailService service, CancellationToken cancellationToken)
    {
        var result = await service.SendEmailCodeAsync(request, cancellationToken);

        return Results.Ok(result.Data);
    }

    #endregion
}