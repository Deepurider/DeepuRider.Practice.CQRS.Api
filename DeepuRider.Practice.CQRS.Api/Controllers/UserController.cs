using DeepuRider.Practice.CQRS.Handlers.Users;
using Microsoft.AspNetCore.Mvc;

namespace DeepuRider.Practice.CQRS.Api.Controllers
{
    [Route("api/v1/[controller]")]
    public class UserController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateUserCommand command, CancellationToken cancellationToken)
        {
            var result = await Mediator.SendAsync<CreateUserCommand, bool>(command, cancellationToken);
            return Ok(result);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] UpdateUserCommand command, CancellationToken cancellationToken)
        {
            if (id != command.Id) return BadRequest("Id in route and body must match.");

            var result = await Mediator.SendAsync<UpdateUserCommand, bool>(command, cancellationToken);
            return Ok(result);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var command = new DeleteUserCommand { Id = id };
            var result = await Mediator.SendAsync<DeleteUserCommand, bool>(command, cancellationToken);
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetUserQuery { Id = id };
            var user = await Mediator.SendAsync<GetUserQuery, UserDto?>(query, cancellationToken);
            if (user is null) return NotFound();
            return Ok(user);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var users = await Mediator.SendAsync<GetUsersQuery, IEnumerable<UserDto>>(new GetUsersQuery(), cancellationToken);
            return Ok(users);
        }
    }
}
