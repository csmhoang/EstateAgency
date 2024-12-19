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
    public record MessageDto
    {
        public string? Id { get; set; }
        [Required(ErrorMessage = UserConst.ErrorEmptySenderId)]
        public string SenderId { get; set; } = null!;
        [Required(ErrorMessage = UserConst.ErrorEmptyReceiverId)]
        public string ReceiverId { get; set; } = null!;
        [Required(ErrorMessage = ConversationConst.ErrorEmptyId)]
        public string ConversationId { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateTime SentAt { get; set; } = DateTime.Now;

        public virtual UserDto? Receiver { get; set; }
        public virtual UserDto? Sender { get; set; }
    }
}
