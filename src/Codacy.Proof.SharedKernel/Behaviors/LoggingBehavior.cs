using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Codacy.Proof.SharedKernel.Behaviors;

internal sealed class LoggingBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : IErrorOr
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_logger.IsEnabled(LogLevel.Information))
        {
            _logger.LogInformation("Request: {@Request}", request);
        }

        var response = await next();

        if (_logger.IsEnabled(LogLevel.Information))
            _logger.LogInformation("Response: {@Response}", response);

        return response;
    }
}
