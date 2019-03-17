namespace Process.Pipeline
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.Extensions.Logging;

    public class PipelineBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        readonly ILogger logger;

        public PipelineBehavior(ILogger logger)
        {
            this.logger = logger;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            try
            {
                TResponse result = await next();
                return result;
            }
            catch(Exception exception)
            {
                logger.Log(
                    LogLevel.Error,
                    $"Error executing {request.GetType().FullName}, " +
                    $"exception={exception.Message}");
                
                throw;
            }
        }
    }
}
