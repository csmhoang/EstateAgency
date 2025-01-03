﻿using Core.Consts;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core.Enums.LeaseEnums;

namespace Core.Dtos
{
    public record LeaseDto
    {
        public string? Id { get; set; }
        [Required(ErrorMessage = BookingConst.ErrorEmptyId)]
        public string BookingId { get; set; } = null!;
        public string Lessor { get; set; } = null!;
        public string Lessee { get; set; } = null!;
        public string? Terms { get; set; }
        public DateTime? SignedDate { get; set; }
        public StatusLease? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public ICollection<LeaseDetailDto>? LeaseDetails { get; set; }
    }
}
