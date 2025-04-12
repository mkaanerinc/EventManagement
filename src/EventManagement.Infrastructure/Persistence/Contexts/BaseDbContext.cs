using EventManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Infrastructure.Persistence.Contexts;

/// <summary>
/// Represents the base DbContext for the Event Management application.
/// Manages database access and entity configurations.
/// </summary>
public class BaseDbContext : DbContext
{
    /// <summary>
    /// Gets or sets the application configuration.
    /// Used for accessing configuration values such as connection strings.
    /// </summary>
    protected IConfiguration Configuration { get; set; }

    /// <summary>
    /// Gets or sets the Events DbSet.
    /// </summary>
    public DbSet<Event> Events { get; set; }

    /// <summary>
    /// Gets or sets the Tickets DbSet.
    /// </summary>
    public DbSet<Ticket> Tickets { get; set; }

    /// <summary>
    /// Gets or sets the Attendees DbSet.
    /// </summary>
    public DbSet<Attendee> Attendees { get; set; }

    /// <summary>
    /// Gets or sets the Reports DbSet.
    /// </summary>
    public DbSet<Report> Reports { get; set; }


    /// <summary>
    /// Initializes a new instance of the <see cref="BaseDbContext"/> class.
    /// </summary>
    /// <param name="options">The options to be used by the DbContext.</param>
    /// <param name="configuration">The application configuration object.</param>
    public BaseDbContext(DbContextOptions<BaseDbContext> options, IConfiguration configuration) : base(options)
    {
        Configuration = configuration;
    }

    /// <summary>
    /// Configures the entity mappings using configurations from the current assembly.
    /// </summary>
    /// <param name="modelBuilder">The model builder used to configure entities.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}