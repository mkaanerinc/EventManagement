using AutoMapper;
using Core.Application.Rules;
using EventManagement.Application.Features.Tickets.Rules;
using EventManagement.Application.Services.Repositories;
using EventManagement.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.Tickets.Commands.Delete;

/// <summary>
/// Command to delete a ticket by its ID.
/// </summary>
public class DeleteTicketCommand : IRequest<DeletedTicketResponse>
{
    /// <summary>
    /// Gets or sets the ID of the ticket to be deleted.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Handles the <see cref="DeleteTicketCommand"/> request.
    /// </summary>
    public class DeleteTicketCommandHandler : IRequestHandler<DeleteTicketCommand, DeletedTicketResponse>
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IMapper _mapper;
        private readonly TicketBusinessRules _ticketBusinessRules;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteTicketCommandHandler"/> class.
        /// </summary>
        /// <param name="ticketRepository">The repository to manage ticket entities.</param>
        /// <param name="mapper">The mapper instance to convert between models.</param>
        /// <param name="ticketBusinessRules">The business rules for validating ticket-specific constraints.</param>
        public DeleteTicketCommandHandler(ITicketRepository ticketRepository, IMapper mapper, TicketBusinessRules ticketBusinessRules)
        {
            _ticketRepository = ticketRepository;
            _mapper = mapper;
            _ticketBusinessRules = ticketBusinessRules;
        }

        /// <summary>
        /// Handles the delete operation for a ticket.
        /// </summary>
        /// <param name="request">The delete ticket command containing the ticket ID.</param>
        /// <param name="cancellationToken">A cancellation token for the asynchronous operation.</param>
        /// <returns>A <see cref="DeletedTicketResponse"/> representing the deleted ticket.</returns>
        /// <exception cref="NotFoundException">Thrown when no ticket is found with the specified ID.</exception>

        public async Task<DeletedTicketResponse> Handle(DeleteTicketCommand request, CancellationToken cancellationToken)
        {
            await RuleRunner.RunAsync(
                async () =>  await _ticketBusinessRules.CheckTicketExistsByIdAsync(request.Id)
            );

            Ticket? deletedTicket = await _ticketRepository.GetAsync
                (predicate: t => t.Id == request.Id,
                cancellationToken: cancellationToken
                );

            // Use this overload of Map to update the existing entity instance (deletedTicket)
            // instead of creating a new one. This way, EF Core keeps tracking the original entity,
            // and only the necessary fields from 'request' are mapped onto it.
            // This approach is preferred for update operations.
            deletedTicket = _mapper.Map(request, deletedTicket);

            await _ticketRepository.DeleteAsync(deletedTicket!);

            DeletedTicketResponse response = _mapper.Map<DeletedTicketResponse>(deletedTicket);

            return response;
        }
    }
}
