using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Codacy.Proof.Web.AppExtensions.Exceptions;

internal sealed class GlobalExceptionHandler : IExceptionHandler
{
    const string BadRequestRfc = "https://tools.ietf.org/html/rfc7231#section-6.5.1";
    const string UnauthorizedAccessRfc = "https://tools.ietf.org/html/rfc7235#section-3.1";
    const string InternalServerErrorRfc = "https://tools.ietf.org/html/rfc7231#section-6.6.1";

    public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is OperationCanceledException)
            return ValueTask.FromResult(true);

        if (exception is BadHttpRequestException)
        {
            CreateResponse(BadRequestRfc, httpContext, exception, cancellationToken);
            return ValueTask.FromResult(true);
        }

        if (exception is UnauthorizedAccessException)
        {
            CreateResponse(UnauthorizedAccessRfc, httpContext, exception, cancellationToken);
            return ValueTask.FromResult(true);
        }

        CreateResponse(InternalServerErrorRfc, httpContext, exception, cancellationToken);
        return ValueTask.FromResult(true);
    }

    private static void CreateResponse(string responseRfcType, HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        httpContext.Response.WriteAsJsonAsync(new ProblemDetails
        {
            Status = httpContext.Response.StatusCode,
            Type = responseRfcType,
            Detail = exception.Message
        }, cancellationToken: cancellationToken);
    }
}
