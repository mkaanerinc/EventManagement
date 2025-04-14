using AutoMapper;
using EventManagement.Application.Services.Repositories;
using EventManagement.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.Events.Commands.Update;

/// <summary>
/// Command to update an event.
/// </summary>
public class UpdateEventCommand : IRequest<UpdatedEventResponse>
{
    /// <summary>
    /// Gets or sets the title of the event.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the title of the event.
    /// </summary>
    public required string Title { get; set; }

    /// <summary>
    /// Gets or sets the description of the event.
    /// </summary>
    public required string Description { get; set; }

    /// <summary>
    /// Gets or sets the location where the event will take place.
    /// </summary>
    public required string Location { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the event will take place.
    /// </summary>
    public DateTimeOffset EventAt { get; set; }

    /// <summary>
    /// Gets or sets the name of the event organizer.
    /// </summary>
    public required string OrganizerName { get; set; }

    /// <summary>
    /// Gets or sets the total capacity of the event.
    /// </summary>
    public int TotalCapacity { get; set; }

    /// <summary>
    /// Handles the <see cref="UpdateEventCommand"/> to update an event and return the result.
    /// </summary>
    public class UpdateEventCommandHandler : IRequestHandler<UpdateEventCommand, UpdatedEventResponse>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateEventCommandHandler"/> class.
        /// </summary>
        /// <param name="eventRepository">The repository to manage event entities.</param>
        /// <param name="mapper">The mapper instance to convert between models.</param>
        public UpdateEventCommandHandler(IEventRepository eventRepository, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles the update of an event.
        /// </summary>
        /// <param name="request">The update event command containing event details.</param>
        /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
        /// <returns>The response object containing details of the updated event.</returns>
        public async Task<UpdatedEventResponse> Handle(UpdateEventCommand request, CancellationToken cancellationToken)
        {
            Event? updatedEvent = await _eventRepository.GetAsync
                (predicate:e => e.Id == request.Id, 
                cancellationToken: cancellationToken
                );

            updatedEvent = _mapper.Map(request, updatedEvent);

            await _eventRepository.UpdateAsync(updatedEvent!);

            UpdatedEventResponse response = _mapper.Map<UpdatedEventResponse>(updatedEvent);

            return response;
        }
    }
}
