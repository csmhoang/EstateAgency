using Core.Consts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core.Enums.BookingEnums;

namespace Core.Dtos
{
    public record BookingUpdateDto
    {
        public DateTime IntendedIntoDate { get; set; }
        public DateTime EndDate { get; set; }
        public int NumberOfTenant { get; set; }
        public string? RejectionReason { get; set; }
        public string? Note { get; set; }
    }
}
