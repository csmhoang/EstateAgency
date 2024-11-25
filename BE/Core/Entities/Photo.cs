using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public partial class Photo
    {
        [Key]
        [MaxLength(36)]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [ForeignKey("Room")]
        [MaxLength(36)]
        public string RoomId { get; set; } = null!;
        [MaxLength(256)]
        public string Url { get; set; } = null!;
        [MaxLength(256)]
        public string PublicId { get; set; } = null!;
        public virtual Room? Room { get; set; }
    }
}
