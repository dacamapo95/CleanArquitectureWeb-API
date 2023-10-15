using AutoMapper;
using CleanArchitecture.Domain;
using CleanArquitecture.Application.Features.Streamers.Commands.CreateStreamer;
using CleanArquitecture.Application.Features.Videos.Queries.GetVideos;

namespace CleanArquitecture.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Video, VideoVm>();
            CreateMap<CreateStreamerCommand, Streamer>();
        }
    }
}
