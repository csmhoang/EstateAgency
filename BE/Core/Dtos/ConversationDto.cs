using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos
{
    public record ConversationDto
    {
        public string? Id { get; set; }
        public ICollection<ParticipantDto>? Participants { get; set; }
        public ICollection<MessageDto>? Messages { get; set; }
    }
}
