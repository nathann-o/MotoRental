using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MotoRental.Application.DTOs;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

public class MotorcycleCreatedConsumer : BackgroundService
{
    private readonly IConfiguration _cfg;
    private readonly ILogger<MotorcycleCreatedConsumer> _logger;
    private IConnection _connection;
    private IModel _channel;

    public MotorcycleCreatedConsumer(IConfiguration cfg, ILogger<MotorcycleCreatedConsumer> logger)
    {
        _cfg = cfg;
        _logger = logger;
    }

    public override Task StartAsync(CancellationToken cancellationToken)
    {
        var host = _cfg["RabbitMq:Host"];
        var user = _cfg["RabbitMq:User"];
        var pass = _cfg["RabbitMq:Password"];
        var exchange = _cfg["RabbitMq:Exchange"];
        var queue = _cfg["RabbitMq:Queue"];
        var routingKey = _cfg["RabbitMq:RoutingKey"];

        var factory = new ConnectionFactory { HostName = host, UserName = user, Password = pass, DispatchConsumersAsync = false };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();

        _channel.ExchangeDeclare(exchange: exchange, type: ExchangeType.Direct, durable: true);
        _channel.QueueDeclare(queue: queue, durable: true, exclusive: false, autoDelete: false, arguments: null);
        _channel.QueueBind(queue: queue, exchange: exchange, routingKey: routingKey);

        return base.StartAsync(cancellationToken);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        if (_channel == null) return Task.CompletedTask;

        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (sender, ea) =>
        {
            try
            {
                var bytes = ea.Body.ToArray();
                var evt = JsonSerializer.Deserialize<MotorcycleDto>(bytes);
                if (evt != null)
                {
                    if (evt.Year == 2024)
                    {
                        // NOTIFICAÇÃO simples: log + console
                        _logger.LogInformation("NOTIFICAÇÃO: Moto 2024 cadastrada: {Model} - {Plate}", evt.Model, evt.Plate);
                        Console.WriteLine($"[NOTIFICAÇÃO] Moto 2024 cadastrada: {evt.Model} - {evt.Plate}");
                        // aqui você pode chamar um INotificationService.SendAsync(...)
                    }
                    else
                    {
                        _logger.LogDebug("Evento recebido (não 2024): {Year} - {Plate}", evt.Year, evt.Plate);
                    }
                }

                _channel.BasicAck(ea.DeliveryTag, multiple: false);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro processando mensagem RabbitMQ");
                // opcional: BasicNack / requeue = true se quiser reprocessar
                _channel.BasicNack(ea.DeliveryTag, multiple: false, requeue: false);
            }
        };

        _channel.BasicConsume(queue: _cfg["RabbitMq:Queue"], autoAck: false, consumer: consumer);
        return Task.CompletedTask;
    }

    public override void Dispose()
    {
        _channel?.Close();
        _connection?.Close();
        base.Dispose();
    }
}
