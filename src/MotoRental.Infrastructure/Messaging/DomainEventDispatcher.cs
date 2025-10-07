using Microsoft.Extensions.DependencyInjection;

public class DomainEventDispatcher : IDomainEventDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public DomainEventDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task DispatchAsync(IEnumerable<IDomainEvent> events, CancellationToken cancellationToken = default)
    {
        if (events == null || !events.Any())
            return;

        using var scope = _serviceProvider.CreateScope();
        var provider = scope.ServiceProvider;

        foreach (var domainEvent in events)
        {
            var eventType = domainEvent.GetType();
            var handlerType = typeof(IDomainEventHandler<>).MakeGenericType(eventType);

            var handlers = provider.GetServices(handlerType).ToList();

            foreach (var handler in handlers)
            {
                var method = handlerType.GetMethod("HandleAsync");
                if (method is null) continue;

                await (Task)method.Invoke(handler, new object[] { domainEvent, cancellationToken })!;
            }
        }
    }
}
