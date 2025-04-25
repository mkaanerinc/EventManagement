using AutoMapper;
using Core.Application.Rules;
using EventManagement.Application.Features.Reports.Rules;
using EventManagement.Application.Services.Repositories;
using EventManagement.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.Reports.Commands.Delete;

/// <summary>
/// Command to delete a report by its ID.
/// </summary>
public class DeleteReportCommand : IRequest<DeletedReportResponse>
{
    /// <summary>
    /// Gets or sets the ID of the report to be deleted.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Handles the <see cref="DeleteReportCommand"/> request.
    /// </summary>
    public class DeleteReportCommandHandler : IRequestHandler<DeleteReportCommand, DeletedReportResponse>
    {
        private readonly IReportRepository _reportRepository;
        private readonly IMapper _mapper;
        private readonly ReportBusinessRules _reportBusinessRules;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteReportCommandHandler"/> class.
        /// </summary>
        /// <param name="reportRepository">The report repository for data access.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        /// <param name="reportBusinessRules">The business rules for validating report-specific constraints.</param>
        public DeleteReportCommandHandler(IReportRepository reportRepository, IMapper mapper, ReportBusinessRules reportBusinessRules)
        {
            _reportRepository = reportRepository;
            _mapper = mapper;
            _reportBusinessRules = reportBusinessRules;
        }

        /// <summary>
        /// Handles the delete operation for a report.
        /// </summary>
        /// <param name="request">The delete report command containing the report ID.</param>
        /// <param name="cancellationToken">A cancellation token for the asynchronous operation.</param>
        /// <returns>A <see cref="DeletedReportResponse"/> representing the deleted report.</returns>
        /// <exception cref="NotFoundException">Thrown when no report is found with the specified report ID.</exception>
        public async Task<DeletedReportResponse> Handle(DeleteReportCommand request, CancellationToken cancellationToken)
        {
            await RuleRunner.RunAsync(
                async () => await _reportBusinessRules.CheckReportExistsByIdAsync(request.Id)
            );

            Report? deletedReport = await _reportRepository.GetAsync
                (predicate: r => r.Id == request.Id,
                cancellationToken: cancellationToken
                );

            // Use this overload of Map to update the existing entity instance (deletedReport)
            // instead of creating a new one. This way, EF Core keeps tracking the original entity,
            // and only the necessary fields from 'request' are mapped onto it.
            // This approach is preferred for update operations.
            deletedReport = _mapper.Map(request, deletedReport);

            await _reportRepository.DeleteAsync(deletedReport!);

            DeletedReportResponse response = _mapper.Map<DeletedReportResponse>(deletedReport);

            return response;
        }
    }
}