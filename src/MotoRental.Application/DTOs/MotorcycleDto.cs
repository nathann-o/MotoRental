using System;

namespace MotoRental.Application.DTOs
{
    public record MotorcycleDto
    {
        public Guid Id { get; init; }
        public int Year { get; init; }
        public string Model { get; init; } = null!;
        public string Plate { get; init; } = null!;
    }
}
