using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos
{
    public record OptionDto
    {
        public List<string>? values { get; set; }
    }
}
