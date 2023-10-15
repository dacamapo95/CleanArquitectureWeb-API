using FluentValidation;

namespace CleanArquitecture.Application.Features.Streamers.Commands.CreateStreamer
{
    public class CreateStreamerCommandValidator : AbstractValidator<CreateStreamerCommand>
    {
        public CreateStreamerCommandValidator()
        {
            RuleFor(streamer => streamer.Name)
                .NotEmpty().WithMessage("Nombre Streamer no puede ser vacio.")
                .NotNull().WithMessage("El Nombre Streamer no puede ser null.")
                .MaximumLength(50).WithMessage("{Nombre} no puede exceder los 50 caracteres.");

            RuleFor(streamer => streamer.Url)
                .NotEmpty().WithMessage("Url no puede estar vacia.");
        }
    }
}
