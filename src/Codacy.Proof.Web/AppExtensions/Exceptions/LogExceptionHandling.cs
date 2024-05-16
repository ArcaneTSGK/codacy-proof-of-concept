using Microsoft.AspNetCore.Diagnostics;

namespace Codacy.Proof.Web.AppExtensions.Exceptions;

internal sealed class LogExceptionHandling : IExceptionHandler
{
    private readonly ILogger<LogExceptionHandling> _logger;

    public LogExceptionHandling(ILogger<LogExceptionHandling> logger)
    {
        _logger = logger;
    }

    public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var exceptionMessage = exception.Message;

        _logger.LogCritical("message with TraceId [{TraceId}] failed with message: {exceptionMessage}", httpContext.TraceIdentifier, exceptionMessage);

        return ValueTask.FromResult(false);
    }
}
