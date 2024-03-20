using PalpiteApi.Application.Interfaces;
using PalpiteApi.Application.Requests;

namespace PalpiteApi.Api.Endpoints;

public static class SendMail
{
    public static void MapSendEmailEndpoints(this WebApplication app)
    {
        app.MapPost("/sendemailcode", async (SendEmailCodeRequest request, IEmailService service, CancellationToken cancellationToken) =>
        {
            var result = await service.SendEmailCodeAsync(request, cancellationToken);

            return Results.Ok(result.Data);
        });
    }
}
