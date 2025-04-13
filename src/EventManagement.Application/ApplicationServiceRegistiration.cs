using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application;

/// <summary>
/// A static class that contains methods for registering application services into the dependency injection container.
/// </summary>
public static class ApplicationServiceRegistiration
{
    /// <summary>
    /// Registers AutoMapper and MediatR services to the provided <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> where services will be registered.</param>
    /// <param name="configuration">The <see cref="IConfiguration"/> that provides application settings.</param>
    /// <returns>The <see cref="IServiceCollection"/> with the added application services.</returns>
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {

        // 1. services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        // This line scans all assemblies in the application for AutoMapper profiles. 
        // If profiles are located in multiple layers, it will load all assemblies.
        // 2. services.AddAutoMapper(Assembly.GetExecutingAssembly());
        // This line scans only the current (executing) assembly for AutoMapper profiles. 
        // If your AutoMapper profiles are only present in one layer, this method is preferred.
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        // 1. configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        // This line registers services from the assembly where the current code is executing. It scans and registers services only from the executing assembly.
        // 2. configuration.RegisterServicesFromAssembly(typeof(ApplicationServiceRegistiration).Assembly);
        // This line registers services from the assembly where the ApplicationServiceRegistiration class is defined.
        // It is useful when you want to register services from a specific layer or assembly, typically for a defined module or application layer.
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });

        return services;
    }
}


