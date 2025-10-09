using System;
using System.Text.Json.Serialization;

namespace MotoRental.Application.DTOs
{
    public record RentalCreateDto
    {
        [JsonPropertyName("entregador_id")]
        public Guid RiderId { get; init; }

        [JsonPropertyName("moto_id")]
        public Guid MotorcycleId { get; init; }

        [JsonPropertyName("data_inicio")]
        public DateTime CreatedAt { get; init; }

        [JsonPropertyName("data_termino")]
        public DateTime EndDate { get; init; }

        [JsonPropertyName("data_previsao_termino")]
        public DateTime ExpectedEndDate { get; init; }

        [JsonPropertyName("plano")]
        public int PlanDays { get; init; } 
    }
}
