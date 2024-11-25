namespace Core.Dtos
{
    public record BookingUpdateDto
    {
        public DateTime IntendedIntoDate { get; set; }
        public DateTime EndDate { get; set; }
        public int NumberOfTenant { get; set; }
        public string? RejectionReason { get; set; }
        public string? Note { get; set; }
    }
}
