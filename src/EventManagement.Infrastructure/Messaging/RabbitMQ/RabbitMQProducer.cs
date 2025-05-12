using Core.CrossCuttingConcerns.Logging;
using Core.Infrastructure.Messaging.RabbitMQ.Interfaces;
using Core.Infrastructure.Messaging.RabbitMQ.Services;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EventManagement.Infrastructure.Messaging.RabbitMQ;

/// <summary>
/// Provides functionality to publish messages to RabbitMQ using a specified exchange and routing key.
/// </summary>
public class RabbitMQProducer : IRabbitMQProducer
{
    private readonly RabbitMQClientService _clientService;
    private readonly ILoggerService _loggerServiceBase;

    /// <summary>
    /// Initializes a new instance of the <see cref="RabbitMQProducer"/> class.
    /// </summary>
    /// <param name="clientService">The RabbitMQ client service used to establish connections.</param>
    /// <param name="loggerServiceBase">The logger service for logging operations and errors.</param>
    public RabbitMQProducer(RabbitMQClientService clientService, ILoggerService loggerServiceBase)
    {
        _clientService = clientService;
        _loggerServiceBase = loggerServiceBase;
    }

    /// <summary>
    /// Publishes a message to a specified RabbitMQ exchange with the given routing key.
    /// </summary>
    /// <typeparam name="T">The type of the message to be published.</typeparam>
    /// <param name="message">The message object to be published.</param>
    /// <param name="exchangeName">The name of the RabbitMQ exchange.</param>
    /// <param name="routingKey">The routing key to use for message delivery.</param>
    /// <returns>A task that represents the asynchronous publish operation.</returns>
    /// <exception cref="Exception">Thrown when the message cannot be published due to an error.</exception>
    public async Task PublishMessage<T>(T message, string exchangeName, string routingKey)
    {
        try
        {
            var channel = await _clientService.ConnectAsync();
            var bodyString = JsonSerializer.Serialize(message);
            var bodyByte = Encoding.UTF8.GetBytes(bodyString);

            var properties = new BasicProperties
            {
                Persistent = true
            };

            await channel.BasicPublishAsync(
                exchange: exchangeName,
                routingKey: routingKey,
                mandatory: false,
                basicProperties: properties,
                body: bodyByte
            );

            _loggerServiceBase.LogInformation($"Message published to exchange: {exchangeName}, routing key: {routingKey}");
        }
        catch (Exception ex)
        {
            _loggerServiceBase.LogError("Failed to publish message.",ex);
            throw;
        }
    }
}