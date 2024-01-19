using MediatR;

namespace CleanArquitecture.Application.Features.Directors.Commands.CreateDirector;

public class CreateDirectorCommand : IRequest<int>
{
    public string Name { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public int VideoId { get; set; }
}
