using Core.Infrastructure.Persistence.Repositories;
using EventManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Services.Repositories;

/// <summary>
/// Represents the contract for the Report repository.
/// Provides asynchronous and synchronous data access methods specific to the <see cref="Report"/> entity.
/// </summary>
public interface IReportRepository : IAsyncRepository<Report,Guid>, IRepository<Report, Guid>
{
}
