using System;
using System.Text.Json.Serialization;

namespace MotoRental.Application.DTOs
{
    public record RiderCreateDto
    {
        [JsonPropertyName("identificador")]
        public Guid Id { get; init; }

        [JsonPropertyName("nome")]
        public string Name { get; init; } = null!;

        [JsonPropertyName("cnpj")]
        public string Cnpj { get; init; } = null!;

        [JsonPropertyName("data_nascimento")]
        public DateTime BirthDate { get; init; }

        [JsonPropertyName("numero_cnh")]
        public string CnhNumber { get; init; } = null!;

        [JsonPropertyName("tipo_cnh")]
        public string CnhType { get; init; } = null!;

        [JsonPropertyName("imagem_cnh")]
        public string ImageCnh { get; init; } = null!;
    }
}
