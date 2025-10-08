using System;

namespace MotoRental.Application.DTOs
{
    public record RentalReturnDto
    {
        public Guid RentalId { get; init; }
        public DateTime ActualReturnDate { get; init; }
    }
}
