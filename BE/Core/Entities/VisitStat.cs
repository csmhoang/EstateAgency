using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public partial class VisitStat : BaseEntity
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int VisitCount { get; set; }
    }
}
