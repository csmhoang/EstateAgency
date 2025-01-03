﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public partial class Participant : BaseEntity
    {
        [ForeignKey("User")]
        [MaxLength(36)]
        public string? UserId { get; set; }
        [ForeignKey("Conversation")]
        [MaxLength(36)]
        public string? ConversationId { get; set; }
        public virtual User? User { get; set; }
        public virtual Conversation? Conversation { get; set; }
    }
}
