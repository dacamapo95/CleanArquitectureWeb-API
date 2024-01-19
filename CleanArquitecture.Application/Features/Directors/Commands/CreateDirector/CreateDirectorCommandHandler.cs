using AutoMapper;
using CleanArchitecture.Domain;
using CleanArquitecture.Application.Contracts.Persistence;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArquitecture.Application.Features.Directors.Commands.CreateDirector;

public class CreateDirectorCommandHandler : IRequestHandler<CreateDirectorCommand, int>
{
    private readonly ILogger<CreateDirectorCommandHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CreateDirectorCommandHandler(ILogger<CreateDirectorCommandHandler> logger, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(CreateDirectorCommand request, CancellationToken cancellationToken)
    {
        var director = _mapper.Map<Director>(request);
        _unitOfWork.Repository<Director>().AddEntity(director);
        await _unitOfWork.SaveTransaction();
        return director.Id;
    }
}
