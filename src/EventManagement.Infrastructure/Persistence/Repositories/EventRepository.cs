using Core.Infrastructure.Persistence.Repositories;
using EventManagement.Application.Services.Repositories;
using EventManagement.Domain.Entities;
using EventManagement.Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Infrastructure.Persistence.Repositories;

/// <summary>
/// Represents the repository for performing operations on <see cref="Event"/> entities.
/// Inherits from <see cref="EFRepositoryBase{Event, Guid, BaseDbContext}"/> to provide common repository functionality.
/// </summary>
public class EventRepository : EFRepositoryBase<Event, Guid, BaseDbContext>, IEventRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EventRepository"/> class.
    /// </summary>
    /// <param name="baseDbContext">The <see cref="BaseDbContext"/> instance used for data access.</param>
    public EventRepository(BaseDbContext baseDbContext) : base(baseDbContext)
    {

    }
}
