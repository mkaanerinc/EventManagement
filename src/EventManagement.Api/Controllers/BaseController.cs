using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EventManagement.Api.Controllers
{
    /// <summary>
    /// Base controller class providing access to the MediatR mediator for derived controllers.
    /// </summary>
    /// <remarks>
    /// This class is designed to allow controllers to send requests using MediatR without directly 
    /// having to instantiate or inject the mediator service.
    /// </remarks>
    public class BaseController : ControllerBase
    {
        private IMediator? _mediator;

        /// <summary>
        /// Gets the MediatR mediator to send requests.
        /// </summary>
        protected IMediator? Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
    }
}
