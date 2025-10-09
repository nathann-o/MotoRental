using System;
using System.Text.Json.Serialization;

namespace MotoRental.Application.DTOs
{
    public record RentalDto
    {
        [JsonPropertyName("identificador")]
        public Guid Id { get; init; }

        [JsonPropertyName("valor_diaria")]
        public int PlanDays { get; init; }

        [JsonPropertyName("entregador_id")]
        public Guid RiderId { get; init; }

        [JsonPropertyName("moto_id")]
        public Guid MotorcycleId { get; init; }

        [JsonPropertyName("data_inicio")]
        public DateTime StartDate { get; init; }

        [JsonPropertyName("data_criacao")]
        public DateTime CreatedAt { get; init; }

        [JsonPropertyName("data_termino")]
        public DateTime EndDate { get; init; }

        [JsonPropertyName("data_previsao_termino")]
        public DateTime ExpectedEndDate { get; init; }

        [JsonPropertyName("ativo")]
        public bool IsActive { get; init; }
    }
}
