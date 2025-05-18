namespace Core.Entities;

public partial class VisitStat : BaseEntity
{
    public int Year { get; set; }
    public int Month { get; set; }
    public int VisitCount { get; set; }
}
