using AutoMapper;
using CleanArchitecture.Domain;
using CleanArquitecture.Application.Contracts.Persistence;
using MediatR;

namespace CleanArquitecture.Application.Features.Streamers.Commands.CreateStreamer
{
    public class CreateCommandHandler : IRequestHandler<CreateStreamerCommand, int>
    {
        private readonly IStreamerRepository _streamerRepository;
        private IMapper _mapper;

        public CreateCommandHandler(IStreamerRepository streamerRepository,
                                    IMapper mapper)
        {
            _streamerRepository = streamerRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateStreamerCommand request, CancellationToken cancellationToken)
        {
            Streamer streamerEntity = _mapper.Map<CreateStreamerCommand, Streamer>(request);
            Streamer newStreamerEntity = await _streamerRepository.AddAsync(streamerEntity);
            return newStreamerEntity.Id;
        }
    }
}
