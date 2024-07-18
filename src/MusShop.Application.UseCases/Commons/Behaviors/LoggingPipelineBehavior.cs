using MediatR;
using MusShop.Domain.Model.ResultItems;
using Serilog;

namespace MusShop.Application.UseCases.Commons.Behaviors;

public sealed class LoggingPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class
    where TResponse : DomainResult<TRequest>
{
    private readonly ILogger _logger = Log.ForContext<LoggingPipelineBehavior<TRequest, TResponse>>();

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        string requestName = typeof(TRequest).Name;
        
        _logger.Information(
            "Processing request {RequestName}", requestName);

        TResponse result = await next();
        
        return result;
    }
}