using FluentValidation;

namespace CleanArquitecture.Application.Features.Streamers.Commands.UpdateStreamer
{
    public class UpdateStreamerCommandValidator : AbstractValidator<UpdateStreamerCommand>
    {
        public UpdateStreamerCommandValidator()
        {
            RuleFor(streamer => streamer.Name)
                .NotEmpty()
                    .WithMessage("Nombre Streamer no puede ser vacio.")
                .NotNull()
                    .WithMessage("El Nombre Streamer no puede ser null.")
                .MaximumLength(50).WithMessage("{Nombre} no puede exceder los 50 caracteres.");

            RuleFor(streamer => streamer.Url)
                .NotEmpty()
                    .WithMessage("Url no puede estar vacia.");
        }
    }
}
