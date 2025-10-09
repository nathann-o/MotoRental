using System;
using System.Text.Json.Serialization;

namespace MotoRental.Application.DTOs
{
    public record MotorcycleDto
    {
        [JsonPropertyName("identificador")]
        public Guid Id { get; init; }

        [JsonPropertyName("ano")]
        public int Year { get; init; }

        [JsonPropertyName("modelo")]
        public string Model { get; init; } = null!;

        [JsonPropertyName("placa")]
        public string Plate { get; init; } = null!;
    }
}
