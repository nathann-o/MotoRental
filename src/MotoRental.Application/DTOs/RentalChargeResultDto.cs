namespace MotoRental.Application.DTOs
{
    public record RentalChargeResultDto
    {
        public decimal BaseCharge { get; init; }
        public decimal Penalty { get; init; }
        public decimal LateFee { get; init; }
        public decimal Total { get; init; }
        public int UsedDays { get; init; }
        public int ExtraDays { get; init; }
        public int UnusedDays { get; init; }
    }
}
