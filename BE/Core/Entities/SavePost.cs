﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public partial class SavePost
    {
        public string Id { get; set; } = null!;
        [ForeignKey("User")]
        public string? UserId { get; set; }
        [ForeignKey("Post")]
        public string? PostId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual User? User { get; set; }
        public virtual Post? Post { get; set; }
    }
}