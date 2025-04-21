using Core.Application.Rules;
using EventManagement.Application.Features.Attendees.Constants;
using EventManagement.Application.Features.Attendees.Rules;
using EventManagement.Application.Services.Repositories;
using EventManagement.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.Attendees.Commands.CheckIn;

/// <summary>
/// Represents a command to mark an attendee as checked-in for an event.
/// </summary>
public class CheckInAttendeeCommand : IRequest<CheckInAttendeeResponse>
{
    /// <summary>
    /// Gets or sets the unique identifier of the attendee.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Handles the <see cref="CheckInAttendeeCommand"/> to mark an attendee as checked-in for an event.
    /// </summary>
    public class CheckInAttendeeCommandHandler : IRequestHandler<CheckInAttendeeCommand, CheckInAttendeeResponse>
    {
        private readonly IAttendeeRepository _attendeeRepository;
        private readonly AttendeeBusinessRules _attendeeBusinessRules;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateAttendeeCommandHandler"/> class.
        /// </summary>
        /// <param name="attendeeRepository">The repository to manage attendee entities.</param>
        /// <param name="attendeeBusinessRules">The business rules for validating attendee-specific constraints.</param>
        public CheckInAttendeeCommandHandler(IAttendeeRepository attendeeRepository, AttendeeBusinessRules attendeeBusinessRules)
        {
            _attendeeRepository = attendeeRepository;
            _attendeeBusinessRules = attendeeBusinessRules;
        }

        /// <summary>
        /// Processes the command to mark the specified attendee as checked-in.
        /// </summary>
        /// <param name="request">The command containing the attendee ID.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>A task representing the asynchronous operation, with the result of the check-in operation.</returns>
        /// <exception cref="NotFoundException">Thrown when no attendee is found with the specified ID.</exception>
        /// <exception cref="BusinessException">Thrown when attendee is already checked in.</exception>
        public async Task<CheckInAttendeeResponse> Handle(CheckInAttendeeCommand request, CancellationToken cancellationToken)
        {
            await RuleRunner.RunAsync(
                async () => await _attendeeBusinessRules.CheckAttendeeExistsByIdAsync(request.Id)
            );

            Attendee? attendee = await _attendeeRepository.GetAsync(
                predicate: a => a.Id == request.Id,
                cancellationToken: cancellationToken
            );

            await RuleRunner.RunAsync(
                async () => await _attendeeBusinessRules.EnsureAttendeeNotCheckedIn(attendee!)
            );

            attendee!.IsCheckedIn = true;

            await _attendeeRepository.UpdateAsync(attendee);

            CheckInAttendeeResponse response = new() { 
                IsCheckedIn = true,
                Message = AttendeesMessages.AttendeeCheckedIn
            };

            return response;
        }
    }
}