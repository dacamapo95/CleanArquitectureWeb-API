using MediatR;

namespace CleanArquitecture.Application.Features.Streamers.Commands.DeleteStreamer
{
    public class DeleteStreamerCommand : IRequest
    {
        public int Id { get; set; }

        public DeleteStreamerCommand(int id)
        {
            Id = id;   
        }
    }

   
}
