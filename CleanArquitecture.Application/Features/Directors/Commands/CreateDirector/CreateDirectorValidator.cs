using FluentValidation;

namespace CleanArquitecture.Application.Features.Directors.Commands.CreateDirector;

public class CreateDirectorValidator : AbstractValidator<CreateDirectorCommand>
{
    public CreateDirectorValidator()
    {
        RuleFor(director => director.Name)
            .NotNull()
            .WithMessage("{Nombre} no puede ser nulo.");

        RuleFor(director => director.LastName)
             .NotNull()
             .WithMessage("{Nombre} no puede ser nulo.");
    }
}