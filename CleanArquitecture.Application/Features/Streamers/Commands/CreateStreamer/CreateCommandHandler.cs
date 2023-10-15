using AutoMapper;
using CleanArchitecture.Domain;
using CleanArquitecture.Application.Contracts.Infrastructure;
using CleanArquitecture.Application.Contracts.Persistence;
using CleanArquitecture.Application.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArquitecture.Application.Features.Streamers.Commands.CreateStreamer
{
    public class CreateCommandHandler : IRequestHandler<CreateStreamerCommand, int>
    {

        private readonly IStreamerRepository _streamerRepository;
        private IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly ILogger<CreateCommandHandler> _logger;

        public CreateCommandHandler(IStreamerRepository streamerRepository,
                                    IMapper mapper,
                                    IEmailService emailService,
                                    ILogger<CreateCommandHandler> logger)
        {
            _streamerRepository = streamerRepository;
            _mapper = mapper;
            _emailService = emailService;
            _logger = logger;
        }

        public async Task<int> Handle(CreateStreamerCommand request, CancellationToken cancellationToken)
        {
            Streamer streamerEntity = _mapper.Map<CreateStreamerCommand, Streamer>(request);

            Streamer newStreamerEntity = await _streamerRepository.AddAsync(streamerEntity);

            return newStreamerEntity.Id;
        }

        private async Task SendEmail(Streamer streamer)
        {
            Email email = new()
            {
                To = "danielcami782@gmail.com",
                Body = "Streamer creado exitosamente.",
                Subject = "Mensaje de alerta"
            };

            try
            {
                await _emailService.SendEmailAsync(email);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ocurrio un error al evniar mail del streamer {streamer.Id}");
            }
        }
    }
}
