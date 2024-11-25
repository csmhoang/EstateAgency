using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public partial class MaintenanceImage
    {
        [Key]
        [MaxLength(36)]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [ForeignKey("MaintenanceRequest")]
        [MaxLength(36)]
        public string MaintenanceRequestId { get; set; } = null!;
        [MaxLength(256)]
        public string Url { get; set; } = null!;
        [MaxLength(256)]
        public string PublicId { get; set; } = null!;
        public virtual MaintenanceRequest? MaintenanceRequest { get; set; }
    }
}
