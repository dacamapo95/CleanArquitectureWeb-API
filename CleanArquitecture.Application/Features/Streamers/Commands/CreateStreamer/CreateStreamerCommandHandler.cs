using AutoMapper;
using CleanArchitecture.Domain;
using CleanArquitecture.Application.Contracts.Persistence;
using FluentValidation;
using MediatR;

namespace CleanArquitecture.Application.Features.Streamers.Commands.CreateStreamer
{
    public class CreateCommandHandler : IRequestHandler<CreateStreamerCommand, int>
    {
        private IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CreateStreamerCommand request, CancellationToken cancellationTo5ken)
        {
            Streamer streamerEntity = _mapper.Map<CreateStreamerCommand, Streamer>(request);
            _unitOfWork.StreamerRepository.AddEntity(streamerEntity);
            await _unitOfWork.SaveTransaction();
            return streamerEntity.Id;
        }
    }
}
