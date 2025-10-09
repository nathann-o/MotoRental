using MotoRental.Application.DTOs;

namespace MotoRental.Application.Messaging;

public interface IRabbitMqPublisher
{
    Task PublishMotorcycleCreatedAsync(MotorcycleDto evt);
}
