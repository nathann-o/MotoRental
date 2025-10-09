using Microsoft.Extensions.Configuration;
using MotoRental.Application.DTOs;
using MotoRental.Application.Messaging;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

public class RabbitMqPublisher : IRabbitMqPublisher
{
    private readonly IConfiguration _cfg;
    public RabbitMqPublisher(IConfiguration cfg) => _cfg = cfg;

    public Task PublishMotorcycleCreatedAsync(MotorcycleDto evt)
    {
        var host = _cfg["RabbitMq:Host"];
        var user = _cfg["RabbitMq:User"];
        var pass = _cfg["RabbitMq:Password"];
        var exchange = _cfg["RabbitMq:Exchange"];
        var routingKey = _cfg["RabbitMq:RoutingKey"];

        // operação síncrona simples executada em thread pool para não bloquear
        return Task.Run(() =>
        {
            var factory = new ConnectionFactory { HostName = host, UserName = user, Password = pass };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.ExchangeDeclare(exchange: exchange, type: ExchangeType.Direct, durable: true);
            var body = JsonSerializer.SerializeToUtf8Bytes(evt);
            var props = channel.CreateBasicProperties();
            props.Persistent = true;

            channel.BasicPublish(exchange: exchange, routingKey: routingKey, basicProperties: props, body: body);
        });
    }
}
