using Core.Infrastructure.Messaging.RabbitMQ.Extensions;
using Core.Infrastructure.Messaging.RabbitMQ.Interfaces;
using EventManagement.Infrastructure.Messaging.RabbitMQ;
using EventManagement.Infrastructure.Persistence;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Infrastructure;

/// <summary>
/// Provides extension methods for registering infrastructure-related services in the dependency injection container.
/// </summary>
public static class InfrastructureServiceRegistiration
{
    /// <summary>
    /// Adds infrastructure services to the specified <see cref="IServiceCollection"/>.
    /// This includes persistence services, RabbitMQ services, and message producer services.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
    /// <param name="configuration">The <see cref="IConfiguration"/> instance to retrieve configuration settings.</param>
    /// <returns>The updated <see cref="IServiceCollection"/> with the infrastructure services added.</returns>
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Register persistence services (e.g., database context, repositories)
        services.AddPersistenceServices(configuration);

        // Register persistence services (e.g., database context, repositories)
        services.AddRabbitMQ();

        // Register the RabbitMQ producer as a scoped service
        services.AddScoped<IRabbitMQProducer, RabbitMQProducer>();

        return services;
    }
}