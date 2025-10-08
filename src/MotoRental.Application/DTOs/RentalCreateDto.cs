using System;

namespace MotoRental.Application.DTOs
{
    public record RentalCreateDto
    {
        public Guid RiderId { get; init; }
        public Guid MotorcycleId { get; init; }
        public int PlanDays { get; init; }
        public DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    }
}
