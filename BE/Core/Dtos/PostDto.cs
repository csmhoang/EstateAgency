using Core.Consts;
using System.ComponentModel.DataAnnotations;
using static Core.Enums.PostEnums;

namespace Core.Dtos
{
    public record PostDto
    {
        public Guid? Id { get; set; }
        public int PostCode { get; set; }
        [Required(ErrorMessage = RoomConst.ErrorEmptyId)]
        public string? RoomId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime AvailableFrom { get; set; }
        public IsAcceptPost? IsAccept { get; set; }
        public StatusPost? Status { get; set; }
    }
}
