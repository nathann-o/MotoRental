public class MotorcycleRegisteredHandler : IDomainEventHandler<MotorcycleRegisteredEvent>
{
    public Task HandleAsync(MotorcycleRegisteredEvent @event, CancellationToken cancellationToken)
    {
        Console.WriteLine($"[EVENT] Motorcycle registered: {@event.Model} ({@event.Plate})");
        return Task.CompletedTask;
    }
}
