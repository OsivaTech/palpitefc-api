using PalpiteFC.Api.Extensions;
using System.Text;

namespace PalpiteFC.Api.Middlewares;

public class HttpContextLoggerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public HttpContextLoggerMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));
        _logger = loggerFactory.CreateLogger<HttpContextLoggerMiddleware>() ?? throw new ArgumentNullException(nameof(loggerFactory));
    }

    public async Task Invoke(HttpContext context)
    {
        var originalBodyStream = context.Response.Body;
        using var responseBodyStream = new MemoryStream();
        context.Response.Body = responseBodyStream;

        var requestBody = await context.Request.GetRequestBody();

        await _next(context);

        var responseBody = await responseBodyStream.GetString();

        if (context.Response.StatusCode >= 400)
        {
            _logger.LogInformation("{@LogContext}", new
            {
                IpAddress = context.Connection.RemoteIpAddress?.ToString(),
                Host = context.Request.Host.ToString(),
                Path = context.Request.Path.ToString(),
                context.Request.Method,
                QueryString = context.Request.QueryString.ToString(),
                Headers = context.Request.Headers.ToDictionary(x => x.Key, y => y.Value.ToString()),
                RequestBody = requestBody,
                ResponseBody = responseBody
            });
        }

        await responseBodyStream.CopyToAsync(originalBodyStream);
    }
}