using Core.CrossCuttingConcerns.Logging;
using Core.Infrastructure.Messaging.RabbitMQ.Interfaces;
using EventManagement.Application.Services.Repositories;
using EventManagement.Domain.Enums;
using EventManagement.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EventManagement.WorkerService.Workers;

/// <summary>
/// A background worker service that consumes messages from RabbitMQ and processes reports.
/// </summary>
public class ReportWorker : BackgroundService
{
    private readonly IRabbitMQConsumer _rabbitMQConsumer;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ILoggerService _loggerServiceBase;

    /// <summary>
    /// Initializes a new instance of the <see cref="ReportWorker"/> class.
    /// </summary>
    /// <param name="rabbitMQConsumer">The RabbitMQ consumer used to receive messages from the queue.</param>
    /// <param name="serviceScopeFactory">The factory for creating service scopes to resolve scoped dependencies like <see cref="IReportRepository"/>.</param>
    /// <param name="loggerServiceBase">The logger service for logging information and errors during report processing.</param>
    public ReportWorker(IRabbitMQConsumer rabbitMQConsumer, IServiceScopeFactory serviceScopeFactory, ILoggerService loggerServiceBase)
    {
        _rabbitMQConsumer = rabbitMQConsumer;
        _serviceScopeFactory = serviceScopeFactory;
        _loggerServiceBase = loggerServiceBase;
    }

    /// <summary>
    /// Executes the background worker by subscribing to a RabbitMQ queue and processing incoming report messages.
    /// </summary>
    /// <param name="stoppingToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await _rabbitMQConsumer.Subscribe<ReportMessage>("report-queue", async (message) =>
        {
            using var scope = _serviceScopeFactory.CreateScope();
            var reportRepository = scope.ServiceProvider.GetRequiredService<IReportRepository>();

            await ProcessReportAsync(message, reportRepository);
        });
    }

    /// <summary>
    /// Processes a report message by generating the report content based on the report type and updating its status.
    /// </summary>
    /// <param name="message">The <see cref="ReportMessage"/> containing the report details (e.g., ReportId, EventId, ReportType).</param>
    /// <param name="reportRepository">The repository used to access and update report entities in the database.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the report with the specified <see cref="ReportMessage.ReportId"/> is not found or if the report type is unsupported.
    /// </exception>
    /// <exception cref="Exception">Thrown if an error occurs during report processing, which triggers a status update to Failed.</exception>
    private async Task ProcessReportAsync(ReportMessage message, IReportRepository reportRepository)
    {
        try
        {
            var report = await reportRepository.GetAsync(r => r.Id == message.ReportId)
                ?? throw new InvalidOperationException($"Report with ID {message.ReportId} not found.");

            if (report.ReportStatus != ReportStatus.Pending)
            {
                _loggerServiceBase.LogInformation($"Report {message.ReportId} is not in Pending status. Current status: {report.ReportStatus}. Skipping processing.");
                return;
            }

            string result;
            switch (message.ReportType)
            {
                case ReportType.TicketSales:
                    var summary = await reportRepository.GetSummaryByEventIdAsync(message.EventId);
                    result = JsonSerializer.Serialize(summary);
                    break;

                case ReportType.Attendance:
                    _loggerServiceBase.LogInformation("Attendance report type is not yet implemented.");
                    return;

                default:
                    throw new InvalidOperationException($"Unsupported report type: {message.ReportType}");
            }

            report.Result = result;
            report.ReportStatus = ReportStatus.Completed;
            report.CompletedAt = DateTimeOffset.UtcNow;

            await reportRepository.UpdateAsync(report);
            _loggerServiceBase.LogInformation($"Report {message.ReportId} processed successfully. Status updated to Completed.");
        }
        catch (Exception ex)
        {
            _loggerServiceBase.LogError($"Failed to process report {message.ReportId}.", ex);

            try
            {
                var report = await reportRepository.GetAsync(r => r.Id == message.ReportId);
                if (report != null)
                {
                    report.ReportStatus = ReportStatus.Failed;
                    report.CompletedAt = DateTimeOffset.UtcNow;
                    await reportRepository.UpdateAsync(report);
                    _loggerServiceBase.LogInformation($"Report {message.ReportId} status updated to Failed.");
                }
            }
            catch (Exception updateEx)
            {
                _loggerServiceBase.LogError($"Failed to update report {message.ReportId} status to Failed.", updateEx);
            }

            throw;
        }
    }
}