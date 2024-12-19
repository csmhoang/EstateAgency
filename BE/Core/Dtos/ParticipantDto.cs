using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Consts;

namespace Core.Dtos
{
    public record ParticipantDto
    {
        public string? Id { get; set; }
        [Required(ErrorMessage = UserConst.ErrorEmptyId)]
        public string UserId { get; set; } = null!;
        [Required(ErrorMessage = ConversationConst.ErrorEmptyId)]
        public string ConversationId { get; set; } = null!;

        public UserDto? User { get; set; }
        public ConversationDto? Conversation { get; set; }
    }
}
