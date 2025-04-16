using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Core.Application.Pipelines.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Core.Application.Rules;

namespace EventManagement.Application;

/// <summary>
/// A static class that contains methods for registering application services into the dependency injection container.
/// </summary>
public static class ApplicationServiceRegistiration
{
    /// <summary>
    /// Registers AutoMapper, FluentValidation and MediatR services to the provided <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> where services will be registered.</param>
    /// <param name="configuration">The <see cref="IConfiguration"/> that provides application settings.</param>
    /// <returns>The <see cref="IServiceCollection"/> with the added application services.</returns>
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {

        // Add all subclasses of BaseBusinessRules from the executing assembly to the service collection.
        // This method will register the subclasses with a default Scoped lifecycle.
        services.AddSubClassesOfType(Assembly.GetExecutingAssembly(), typeof(BaseBusinessRules));

        // 1. services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        // This line scans all assemblies in the application for AutoMapper profiles. 
        // If profiles are located in multiple layers, it will load all assemblies.
        // 2. services.AddAutoMapper(Assembly.GetExecutingAssembly());
        // This line scans only the current (executing) assembly for AutoMapper profiles. 
        // If your AutoMapper profiles are only present in one layer, this method is preferred.
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        // 1. configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        // This line registers services from the assembly where the current code is executing. It scans and registers services only from the executing assembly.
        // 2. configuration.RegisterServicesFromAssembly(typeof(ApplicationServiceRegistiration).Assembly);
        // This line registers services from the assembly where the ApplicationServiceRegistiration class is defined.
        // It is useful when you want to register services from a specific layer or assembly, typically for a defined module or application layer.
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());

            configuration.AddOpenBehavior(typeof(RequestValidationBehavior<,>));
        });

        return services;
    }

    /// <summary>
    /// Registers all subclasses of a specified type from a given assembly into the <see cref="IServiceCollection"/>.
    /// Optionally allows specifying a custom service registration with lifecycle.
    /// </summary>
    /// <param name="services">The service collection to add the types to.</param>
    /// <param name="assembly">The assembly to search for subclasses in.</param>
    /// <param name="type">The base type to search for subclasses of.</param>
    /// <param name="addWithLifeCycle">Optional function to customize how to add services with a specified lifecycle.</param>
    /// <returns>The updated <see cref="IServiceCollection"/> with the registered types.</returns>
    public static IServiceCollection AddSubClassesOfType(
       this IServiceCollection services,
       Assembly assembly,
       Type type,
       Func<IServiceCollection, Type, IServiceCollection>? addWithLifeCycle = null
    )
    {
        var types = assembly.GetTypes().Where(t => t.IsSubclassOf(type) && type != t).ToList();
        foreach (var item in types)
            if (addWithLifeCycle == null)
                services.AddScoped(item);

            else
                addWithLifeCycle(services, type);
        return services;
    }
}


