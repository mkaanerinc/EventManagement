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

namespace EventManagement.Application.Features.Attendees.Commands.Delete;

/// <summary>
/// Command to delete an attendee by its ID.
/// </summary>
public class DeleteAttendeeCommand : IRequest<DeletedAttendeeResponse>
{
    /// <summary>
    /// Gets or sets the ID of the attendee to be deleted.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Handles the <see cref="DeleteAttendeeCommand"/> request.
    /// </summary>
    public class DeleteAttendeeCommandHandler : IRequestHandler<DeleteAttendeeCommand, DeletedAttendeeResponse>
    {
        private readonly IAttendeeRepository _attendeeRepository;
        private readonly IMapper _mapper;
        private readonly AttendeeBusinessRules _attendeeBusinessRules;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteAttendeeCommandHandler"/> class.
        /// </summary>
        /// <param name="attendeeRepository">The repository to manage attendee entities.</param>
        /// <param name="mapper">The mapper instance to convert between models.</param>
        /// <param name="attendeeBusinessRules">The business rules for validating attendee-specific constraints.</param>
        public DeleteAttendeeCommandHandler(IAttendeeRepository attendeeRepository, IMapper mapper, AttendeeBusinessRules attendeeBusinessRules)
        {
            _attendeeRepository = attendeeRepository;
            _mapper = mapper;
            _attendeeBusinessRules = attendeeBusinessRules;
        }

        /// <summary>
        /// Handles the delete operation for an attendee.
        /// </summary>
        /// <param name="request">The delete attendee command containing the attendee ID.</param>
        /// <param name="cancellationToken">A cancellation token for the asynchronous operation.</param>
        /// <returns>A <see cref="DeletedAttendeeResponse"/> representing the deleted attendee.</returns>
        /// <exception cref="NotFoundException">Thrown when no attendee is found with the specified attendee ID.</exception>
        public async Task<DeletedAttendeeResponse> Handle(DeleteAttendeeCommand request, CancellationToken cancellationToken)
        {
            await RuleRunner.RunAsync(
               async () => await _attendeeBusinessRules.CheckAttendeeExistsByIdAsync(request.Id)
           );

            Attendee? deletedAttendee = await _attendeeRepository.GetAsync
                (predicate: a => a.Id == request.Id,
                cancellationToken: cancellationToken
                );

            // Use this overload of Map to update the existing entity instance (deletedAttendee)
            // instead of creating a new one. This way, EF Core keeps tracking the original entity,
            // and only the necessary fields from 'request' are mapped onto it.
            // This approach is preferred for update operations.
            deletedAttendee = _mapper.Map(request, deletedAttendee);

            await _attendeeRepository.DeleteAsync(deletedAttendee!);

            DeletedAttendeeResponse response = _mapper.Map<DeletedAttendeeResponse>(deletedAttendee);

            return response;
        }
    }
}