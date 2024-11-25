using Core.Consts;
using Core.Entities;
using System.ComponentModel.DataAnnotations;
using static Core.Enums.RoomEnums;

namespace Core.Dtos
{
    public record RoomDto
    {
        public string? Id { get; set; }
        [Required(ErrorMessage = UserConst.ErrorEmptyLandlordId)]
        public string LandlordId { get; set; } = null!;
        public string Name { get; set; } = null!;
        public Category Category { get; set; }
        public string Address { get; set; } = null!;
        public string Ward { get; set; } = null!;
        public string Province { get; set; } = null!;
        public string District { get; set; } = null!;
        public int Bedroom { get; set; }
        public int Bathroom { get; set; }
        public int Toilet { get; set; }
        public Interior Interior { get; set; }
        public decimal Area { get; set; }
        public decimal Price { get; set; }
        public decimal Deposite { get; set; }
        public ConditionRoom? Condition { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public UserDto? Landlord { get; set; }
        public ICollection<PhotoDto>? Photos { get; set; }
        public ICollection<PostDto>? Posts { get; set; }
    }
}
