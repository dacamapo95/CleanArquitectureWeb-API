using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArquitecture.Application.Behaviours
{
    public class UnhandledExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest
    {
        private readonly ILogger _logger;

        public UnhandledExceptionBehaviour(ILogger logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
                return await next();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Application Request: Sucedio una excepción inesperada para el request {name}, {@Request}.", typeof(TRequest).Name, request);
                throw;
            }
        }
    }
}
