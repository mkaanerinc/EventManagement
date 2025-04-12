using Core.Infrastructure.Persistence.Repositories;
using EventManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Services.Repositories;

public interface IReportRepository : IAsyncRepository<Report,Guid>, IRepository<Report, Guid>
{
}
