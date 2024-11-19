using Core.Consts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core.Enums.BookingEnums;

namespace Core.Dtos
{
    public record BookingDto
    {
        public string Id { get; set; } = null!;
        [Required(ErrorMessage = PostConst.ErrorEmptyId)]
        public string PostId { get; set; } = null!;
        [Required(ErrorMessage = UserConst.ErrorEmptyLandlordId)]
        public string LandlordId { get; set; } = null!;
        public DateTime IntendedIntoDate { get; set; }
        public int NumberOfTenant { get; set; }
        public string? Note { get; set; }
        public StatusBooking Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
