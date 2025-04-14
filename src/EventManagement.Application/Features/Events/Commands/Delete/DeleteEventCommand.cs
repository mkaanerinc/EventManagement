using AutoMapper;
using EventManagement.Application.Services.Repositories;
using EventManagement.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.Events.Commands.Delete;

/// <summary>
/// Command to delete an event by its ID.
/// </summary>
public class DeleteEventCommand : IRequest<DeletedEventResponse>
{
    /// <summary>
    /// Gets or sets the ID of the event to be deleted.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Handles the <see cref="DeleteEventCommand"/> request.
    /// </summary>
    public class DeleteEventCommandHandler : IRequestHandler<DeleteEventCommand, DeletedEventResponse>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteEventCommandHandler"/> class.
        /// </summary>
        /// <param name="eventRepository">The event repository for data access.</param>
        /// <param name="mapper">The AutoMapper instance for object mapping.</param>
        public DeleteEventCommandHandler(IEventRepository eventRepository, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles the delete operation for an event.
        /// </summary>
        /// <param name="request">The delete event command containing the event ID.</param>
        /// <param name="cancellationToken">A cancellation token for the asynchronous operation.</param>
        /// <returns>A <see cref="DeletedEventResponse"/> representing the deleted event.</returns>
        public async Task<DeletedEventResponse> Handle(DeleteEventCommand request, CancellationToken cancellationToken)
        {
            Event? deletedEvent = await _eventRepository.GetAsync
                (predicate: e => e.Id == request.Id, 
                cancellationToken: cancellationToken
                );

            // Use this overload of Map to update the existing entity instance (deletedEvent)
            // instead of creating a new one. This way, EF Core keeps tracking the original entity,
            // and only the necessary fields from 'request' are mapped onto it.
            // This approach is preferred for update operations.
            deletedEvent = _mapper.Map(request,deletedEvent);

            await _eventRepository.DeleteAsync(deletedEvent!);

            DeletedEventResponse response = _mapper.Map<DeletedEventResponse>(deletedEvent);

            return response;
        }
    }
}
