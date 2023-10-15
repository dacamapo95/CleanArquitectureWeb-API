using AutoMapper;
using CleanArchitecture.Domain;
using CleanArquitecture.Application.Contracts.Persistence;
using CleanArquitecture.Application.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArquitecture.Application.Features.Streamers.Commands.UpdateStreamer
{
    public class UpdateStreamerCommandHandler : IRequestHandler<UpdateStreamerCommand> 
    {
        private readonly IStreamerRepository _streamerRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateStreamerCommandHandler> _logger;

        public UpdateStreamerCommandHandler(IStreamerRepository streamerRepository, 
                                            IMapper mapper,
                                            ILogger<UpdateStreamerCommandHandler> logger)
        {
            _streamerRepository = streamerRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Unit> Handle(UpdateStreamerCommand request, CancellationToken cancellationToken)
        {
            var streamer = await _streamerRepository.GetByIdAsync(request.Id);

            if (streamer == null)
            {
                _logger.LogError($"No se encontró Streamer id: {request.Id}.");
                throw new NotFoundException(nameof(streamer), request.Id);
            }

            _mapper.Map(request, streamer, typeof(UpdateStreamerCommand), typeof(Streamer));
            await _streamerRepository.UpdateAsync(streamer);
            _logger.LogInformation($"La operación fue exitosa actualizando el streamer {request?.Id}.");

            return Unit.Value;
        }

        Task IRequestHandler<UpdateStreamerCommand>.Handle(UpdateStreamerCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
