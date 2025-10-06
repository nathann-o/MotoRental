using MotoRental.Domain.Events;
using System;

public sealed record MotorcycleRegisteredEvent(Guid MotorcycleId, int Year, string Model, string Plate) : IDomainEvent
{
    public DateTime OccurredOn { get; init; } = DateTime.UtcNow;
}
