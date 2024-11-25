using Core.Consts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Core.Enums.PostEnums;

namespace Core.Dtos
{
    public record PostUpdateDto
    {
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime AvailableFrom { get; set; }
    }
}
