using FluentValidation;
using MediatR;

namespace CleanArquitecture.Application.Behaviours;

public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>

{
    private readonly IEnumerable<IValidator> _validators;

    public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);
            // Ejecuta todas las validaciones dentro del PipeLine
            var validatorResults =
                await Task.WhenAll(_validators.Select(validation => validation.ValidateAsync(context, cancellationToken)));

            var failures = validatorResults.Where(result => result.Errors != null && result.Errors?.Count > 0)
                                           .SelectMany(result => result.Errors)
                                           .ToArray();

            if (failures.Any())
            {
                throw new Exceptions.ValidationException(failures);
            }
        }

        return await next();
    }
}
