using Core.Consts;
using System.ComponentModel.DataAnnotations;

namespace Core.Dtos
{
    public record FeedbackDto
    {
        public string? Id { get; set; }
        [Required(ErrorMessage = UserConst.ErrorEmptyTenantId)]
        public string? TenantId { get; set; }
        [Required(ErrorMessage = PostConst.ErrorEmptyId)]
        public string PostId { get; set; } = null!;
        public string? ReplyId { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime? CreatedAt { get; set; }
        public UserDto? Tenant { get; set; }
        public ICollection<FeedbackDto>? Replies { get; set; }

    }
}
