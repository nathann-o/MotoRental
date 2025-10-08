using System;

namespace MotoRental.Application.DTOs
{
    public record RentalDto
    {
        public Guid Id { get; init; }
        public Guid RiderId { get; init; }
        public Guid MotorcycleId { get; init; }
        public int PlanDays { get; init; }
        public DateTime StartDate { get; init; }
        public DateTime ExpectedEndDate { get; init; }
        public DateTime CreatedAt { get; init; }
        public bool IsActive { get; init; }
    }
}
