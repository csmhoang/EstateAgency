﻿using System.ComponentModel.DataAnnotations;

namespace Core;

public record RoomDto
{
    public string? Id { get; set; }
    [Required(ErrorMessage = UserConst.ErrorEmptyLandlordId)]
    public string LandlordId { get; set; } = null!;
    public string Name { get; set; } = null!;
    public RoomEnums.Category Category { get; set; }
    public string Address { get; set; } = null!;
    public string Ward { get; set; } = null!;
    public string Province { get; set; } = null!;
    public string District { get; set; } = null!;
    public int Bedroom { get; set; }
    public int Bathroom { get; set; }
    public int Toilet { get; set; }
    public RoomEnums.Interior Interior { get; set; }
    public decimal Area { get; set; }
    public decimal Price { get; set; }
    public bool? Visibility { get; set; }
    public RoomEnums.ConditionRoom? Condition { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    public UserDto? Landlord { get; set; }
    public ICollection<PhotoDto>? Photos { get; set; }
    public ICollection<PostDto>? Posts { get; set; }
    public ICollection<BookingDetailDto>? BookingDetails { get; set; }
    public ICollection<LeaseDetailDto>? LeaseRooms { get; set; }
}
