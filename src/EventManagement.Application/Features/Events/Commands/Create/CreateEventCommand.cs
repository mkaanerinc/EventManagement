using AutoMapper;
using EventManagement.Application.Services.Repositories;
using EventManagement.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.Events.Commands.Create;

/// <summary>
/// Command to create a new event.
/// </summary>
public class CreateEventCommand : IRequest<CreatedEventResponse>
{
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
    /// Handles the <see cref="CreateEventCommand"/> to create a new event and return the result.
    /// </summary>
    public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, CreatedEventResponse>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateEventCommandHandler"/> class.
        /// </summary>
        /// <param name="eventRepository">The repository to manage event entities.</param>
        /// <param name="mapper">The mapper instance to convert between models.</param>
        public CreateEventCommandHandler(IEventRepository eventRepository, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles the creation of a new event.
        /// </summary>
        /// <param name="request">The create event command containing event details.</param>
        /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
        /// <returns>The response object containing details of the created event.</returns>
        public async Task<CreatedEventResponse> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            Event newEvent = _mapper.Map<Event>(request);
            newEvent.Id = Guid.NewGuid();

            await _eventRepository.AddAsync(newEvent);

            CreatedEventResponse response = _mapper.Map<CreatedEventResponse>(newEvent);

            return response;
        }
    }
}
