using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArquitecture.Application.Features.Streamers.Commands.UpdateStreamer
{
    public class UpdateStreamerCommand : IRequest
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Url { get; set; } = string.Empty;
    }
}
