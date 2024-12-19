using System.ComponentModel.DataAnnotations;
using static Core.Enums.NotificationEnums;
using Core.Consts;

namespace Core.Dtos
{
    public record NotificationDto
    {
        public string? Id { get; set; }
        [Required(ErrorMessage = UserConst.ErrorEmptyReceiverId)]
        public string ReceiverId { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public StatusNotification? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
