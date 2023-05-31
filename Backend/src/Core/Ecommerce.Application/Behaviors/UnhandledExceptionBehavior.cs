using MediatR;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Application.Behaviors
{
    public class UnhandledExceptionBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<TRequest> _logger;

        public UnhandledExceptionBehavior(ILogger<TRequest> logger)
        {
            this._logger = logger;
        }


        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
                return await next();
            }
            catch (System.Exception ex)
            {
                var requestName = typeof(TRequest).Name;
                _logger.LogError(ex, "Application Request: Sucedio una excepción para el request {Name} {@Request}", requestName, request);
                throw new Exception("Application Request con errores");
            }
        }
    }
}