using AutoMapper;
using CleanArchitecture.Domain;
using CleanArquitecture.Application.Contracts.Persistence;
using CleanArquitecture.Application.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArquitecture.Application.Features.Streamers.Commands.DeleteStreamer;

public class DeleteStreamerHandler : IRequestHandler<DeleteStreamerCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ILogger<DeleteStreamerHandler> _logger;

    public DeleteStreamerHandler(IUnitOfWork unitOfWork, IMapper mapper, ILogger<DeleteStreamerHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Unit> Handle(DeleteStreamerCommand request, CancellationToken cancellationToken)
    {
        var streamerToDelete = await _unitOfWork.Repository<Streamer>().GetByIdAsync(request.Id);

        if (streamerToDelete == null)
        {
            _logger.LogError($"Streamer con Id: {request.Id}, no existe en el sistema.");
            throw new NotFoundException(nameof(Streamer), request.Id);
        }

        _unitOfWork.Repository<Streamer>().DeleteEntity(streamerToDelete);
        await _unitOfWork.SaveTransaction();
        _logger.LogInformation($"El streamer con Id: {request.Id} fue eliminado exitosamente.");

        return Unit.Value;
    }
}
