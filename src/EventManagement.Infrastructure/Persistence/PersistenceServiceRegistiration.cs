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

public static class PersistenceServiceRegistiration
{
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
