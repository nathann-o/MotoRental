using System;

namespace MotoRental.Application.DTOs
{
    public record RiderCreateDto
    {
        public string Name { get; init; } = null!;
        public string Cnpj { get; init; } = null!;
        public DateTime BirthDate { get; init; }
        public string CnhNumber { get; init; } = null!;
        public string CnhType { get; init; } = null!;
    }
}
