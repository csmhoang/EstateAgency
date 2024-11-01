using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public partial class Photo
    {
        public string Id { get; set; } = null!;
        [ForeignKey("Room")]
        public string? RoomId { get; set; }
        public string Url { get; set; } = null!;
        public string PublicId { get; set; } = null!;
        public virtual Room? Room { get; set; }
    }
}
