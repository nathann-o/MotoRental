using System;
using System.Text.Json.Serialization;

namespace MotoRental.Application.DTOs
{
    public record RentalReturnDto
    {
        [JsonPropertyName("locacao_id")]
        public Guid RentalId { get; init; }

        [JsonPropertyName("data_devolucao")]
        public DateTime ActualReturnDate { get; init; }
    }
}
