using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Api.ExceptionHandlers;

public class GlobalExceptionHandler : IExceptionHandler
{
    #region Fields

    private readonly ILogger<GlobalExceptionHandler> _logger;

    #endregion

    #region Constructors

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    #endregion

    #region Public Methods

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var problemDetails = new ProblemDetails
        {
            Status = (int)HttpStatusCode.InternalServerError,
            Type = exception.GetType().Name,
            Title = "An unexpected error occurred",
            Detail = exception.Message,
            Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}",
            Extensions = new Dictionary<string, object?>
            {
                { "TraceKey", httpContext.TraceIdentifier }
            }
        };

        _logger.LogError(exception, "Exception occurred: {Message}", exception.Message);

        await Results.Problem(problemDetails).ExecuteAsync(httpContext);

        return true;
    }

    #endregion
}
