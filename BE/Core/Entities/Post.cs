﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core;

public partial class Post : BaseEntity
{
    public Post()
    {
        Feedbacks = new HashSet<Feedback>();
        SavePosts = new HashSet<SavePost>();
    }
    [ForeignKey("Room")]
    [MaxLength(36)]
    public string? RoomId { get; set; }
    [ForeignKey("Landlord")]
    [MaxLength(36)]
    public string? LandlordId { get; set; }
    [MaxLength(100)]
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    [Column(TypeName = "date")]
    public DateTime AvailableFrom { get; set; }
    public bool IsHide { get; set; }
    public PostEnums.IsAcceptPost IsAccept { get; set; }
    public PostEnums.StatusPost Status { get; set; }

    public virtual User? Landlord { get; set; }
    public virtual Room? Room { get; set; }
    public virtual ICollection<SavePost> SavePosts { get; set; }
    public virtual ICollection<Feedback> Feedbacks { get; set; }
}
