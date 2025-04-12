using Core.Infrastructure.Persistence.Repositories;
using EventManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Services.Repositories;

/// <summary>
/// Represents the contract for the Event repository.
/// Provides asynchronous and synchronous data access methods specific to the <see cref="Event"/> entity.
/// </summary>
public interface IEventRepository : IAsyncRepository<Event, Guid>, IRepository<Event, Guid>
{
}
