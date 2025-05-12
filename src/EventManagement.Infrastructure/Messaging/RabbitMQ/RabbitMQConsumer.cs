using Core.CrossCuttingConcerns.Logging;
using Core.Infrastructure.Messaging.RabbitMQ.Interfaces;
using Core.Infrastructure.Messaging.RabbitMQ.Services;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EventManagement.Infrastructure.Messaging.RabbitMQ;

/// <summary>
/// Consumes messages from RabbitMQ queues and processes them using provided handlers.
/// </summary>
public class RabbitMQConsumer : IRabbitMQConsumer
{
    private readonly RabbitMQClientService _clientService;
    private readonly ILoggerService _loggerServiceBase;
    private IChannel? _channel;

    /// <summary>
    /// Initializes a new instance of the <see cref="RabbitMQConsumer"/> class.
    /// </summary>
    /// <param name="clientService">The RabbitMQ client service for managing connections.</param>
    /// <param name="loggerServiceBase">The logger service for logging operations and errors.</param>
    public RabbitMQConsumer(RabbitMQClientService clientService, ILoggerService loggerServiceBase)
    {
        _clientService = clientService;
        _loggerServiceBase = loggerServiceBase;
    }

    /// <summary>
    /// Subscribes to a RabbitMQ queue and starts consuming messages of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of message to be deserialized and processed.</typeparam>
    /// <param name="queueName">The name of the RabbitMQ queue to subscribe to.</param>
    /// <param name="onMessageReceived">A callback function to handle each received message.</param>
    /// <returns>A task that represents the asynchronous subscription operation.</returns>
    /// <exception cref="Exception">Thrown when the subscription or message handling fails.</exception>
    public async Task Subscribe<T>(string queueName, Func<T, Task> onMessageReceived)
    {
        try
        {
            _channel = await _clientService.ConnectAsync();
            await _channel.BasicQosAsync(prefetchSize: 0, prefetchCount: 1, global: false);

            var consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.ReceivedAsync += async (model, ea) =>
            {
                try
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    var deserializedMessage = JsonSerializer.Deserialize<T>(message);
                    await onMessageReceived(deserializedMessage!);
                    await _channel.BasicAckAsync(deliveryTag: ea.DeliveryTag, multiple: false);
                    _loggerServiceBase.LogInformation($"Message consumed from queue: {queueName}");
                }
                catch (Exception ex)
                {
                    _loggerServiceBase.LogError("Failed to process message.", ex);
                    await _channel.BasicNackAsync(deliveryTag: ea.DeliveryTag, multiple: false, requeue: true);
                }
            };

            await _channel.BasicConsumeAsync(queue: queueName, autoAck: false, consumer: consumer);
        }
        catch (Exception ex)
        {
            _loggerServiceBase.LogError("Failed to subscribe to RabbitMQ queue.", ex);
            throw;
        }
    }

    /// <summary>
    /// Disposes the underlying RabbitMQ client service and releases resources.
    /// </summary>
    public void Dispose()
    {
        _clientService.Dispose();
    }
}