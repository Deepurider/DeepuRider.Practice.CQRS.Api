using DeepuRider.Practice.CQRS.Core;
using Microsoft.AspNetCore.Mvc;

namespace DeepuRider.Practice.CQRS.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BaseController : ControllerBase
    {
        /// <summary>
        /// Provides access to the application's mediator for dispatching requests (commands/queries)
        /// from controllers. Resolved from the request services scope.
        /// </summary>
        public IApplicationMediator Mediator => HttpContext.RequestServices.GetRequiredService<IApplicationMediator>();
    }
}
