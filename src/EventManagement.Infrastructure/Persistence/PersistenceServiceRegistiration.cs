using EventManagement.Application.Services.Repositories;
using EventManagement.Infrastructure.Persistence.Contexts;
using EventManagement.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Infrastructure.Persistence;

/// <summary>
/// A static class responsible for registering the persistence services in the dependency injection container.
/// </summary>
public static class PersistenceServiceRegistiration
{
    /// <summary>
    /// Registers the DbContext and repository services to the provided <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> where services will be registered.</param>
    /// <param name="configuration">The <see cref="IConfiguration"/> that provides application settings, including the connection string.</param>
    /// <returns>The <see cref="IServiceCollection"/> with the added persistence services.</returns>
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<BaseDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("EventManagement"));
        });

        services.AddScoped<IAttendeeRepository,AttendeeRepository>();
        services.AddScoped<IEventRepository, EventRepository>();
        services.AddScoped<IReportRepository, ReportRepository>();
        services.AddScoped<ITicketRepository, TicketRepository>();

        return services;
    }
}
