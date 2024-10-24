using Core.Consts;
using System.ComponentModel.DataAnnotations;
using static Core.Enums.RoomEnums;

namespace Core.Dtos
{
    public record RoomDto
    {
        public Guid? Id { get; set; }
        public int RoomCode { get; set; }
        [Required(ErrorMessage = UserConst.ErrorEmptyLandlordId)]
        public string? LandlordId { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Category { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string? Ward { get; set; }
        public string? Province { get; set; }
        public string? District { get; set; }
        public int Bedroom { get; set; }
        public int Bathroom { get; set; }
        public int Toilet { get; set; }
        public Interior Interior { get; set; }
        public decimal Area { get; set; }
        public decimal Price { get; set; }
        public ConditionRoom Condition { get; set; }
    }
}
