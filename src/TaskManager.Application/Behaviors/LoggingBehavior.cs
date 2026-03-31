using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;

namespace TaskManager.Application.Behaviors;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken
    )
    {
        TResponse result = default;
        _logger.LogInformation(typeof(TRequest).Name);
        Stopwatch stopWatch = new Stopwatch();
        stopWatch.Start();
        
        try
        {
            result = await next();
        }
        catch (Exception e)
        {
            _logger.LogError(e.ToString());
            throw;
        }
        
        stopWatch.Stop();
        _logger.LogInformation(typeof(TRequest).Name + stopWatch.ElapsedMilliseconds);
        return result;
    }
}