using MediatR;

namespace CleanArquitecture.Application.Features.Streamers.Commands.CreateStreamer
{
    public record class CreateStreamerCommand : IRequest<int>
    {
        public string Name { get; set; } = string.Empty;

        public string Url { get; set; } = string.Empty;
    }
}
