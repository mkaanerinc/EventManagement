using AutoMapper;
using Core.Application.Rules;
using EventManagement.Application.Features.Attendees.Rules;
using EventManagement.Application.Services.Repositories;
using EventManagement.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.Attendees.Commands.Update;

/// <summary>
/// Command to update an attendee.
/// </summary>
public class UpdateAttendeeCommand : IRequest<UpdatedAttendeeResponse>
{
    /// <summary>
    /// Gets or sets the unique identifier of the created attendee.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the ID of the ticket associated with the attendee.
    /// </summary>
    public Guid TicketId { get; set; }

    /// <summary>
    /// Gets or sets the full name of the attendee.
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// Gets or sets the email address of the attendee.
    /// </summary>
    public required string Email { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the ticket was purchased.
    /// </summary>
    public DateTimeOffset PurchasedAt { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the attendee has checked in to the event.
    /// </summary>
    public bool IsCheckedIn { get; set; }

    /// <summary>
    /// Handles the <see cref="UpdateAttendeeCommand"/> to update an attendee and return the result.
    /// </summary>
    public class UpdateAttendeeCommandHandler : IRequestHandler<UpdateAttendeeCommand, UpdatedAttendeeResponse>
    {
        private readonly IAttendeeRepository _attendeeRepository;
        private readonly IMapper _mapper;
        private readonly AttendeeBusinessRules _attendeeBusinessRules;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateAttendeeCommandHandler"/> class.
        /// </summary>
        /// <param name="attendeeRepository">The repository to manage attendee entities.</param>
        /// <param name="mapper">The mapper instance to convert between models.</param>
        /// <param name="attendeeBusinessRules">The business rules for validating attendee-specific constraints.</param>
        public UpdateAttendeeCommandHandler(IAttendeeRepository attendeeRepository, IMapper mapper, AttendeeBusinessRules attendeeBusinessRules)
        {
            _attendeeRepository = attendeeRepository;
            _mapper = mapper;
            _attendeeBusinessRules = attendeeBusinessRules;
        }

        /// <summary>
        /// Handles the update of an attendee.
        /// </summary>
        /// <param name="request">The update attendee command containing attendee details.</param>
        /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
        /// <returns>The response object containing details of the updated attendee.</returns>
        /// <exception cref="NotFoundException">Thrown when no attendee is found with the specified attendee ID.</exception>
        /// <exception cref="BusinessException">Thrown when no attendee is found with the specified ticket ID.</exception>
        /// <exception cref="BusinessException">Thrown when the ticket does not exist or has no available quantity.</exception>
        public async Task<UpdatedAttendeeResponse> Handle(UpdateAttendeeCommand request, CancellationToken cancellationToken)
        {
            await RuleRunner.RunAsync(
                async () => await _attendeeBusinessRules.CheckAttendeeExistsByIdAsync(request.Id),
                async () => await _attendeeBusinessRules.EnsureTicketExists(request.TicketId),
                async () => await _attendeeBusinessRules.EnsureTicketHasAvailableQuantity(request.TicketId)
            );

            Attendee? updatedAttendee = await _attendeeRepository.GetAsync
                (predicate: a => a.Id == request.Id,
                cancellationToken: cancellationToken
                );

            updatedAttendee = _mapper.Map(request, updatedAttendee);

            await _attendeeRepository.UpdateAsync(updatedAttendee!);

            UpdatedAttendeeResponse response = _mapper.Map<UpdatedAttendeeResponse>(updatedAttendee);

            return response;
        }
    }
}