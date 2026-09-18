using DeepuRider.Practice.CQRS.Core;

namespace DeepuRider.Practice.CQRS.Handlers.Users
{
    public record UserDto(Guid Id, string Username, string Email);

    /// <summary>
    /// Query to fetch a single user by id.
    /// </summary>
    public class GetUserQuery : IRequest<UserDto?>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<GetUserQuery, UserDto?>
        {
            public async Task<UserDto?> HandleAsync(GetUserQuery request, CancellationToken ct)
            {
                // TODO: fetch user from persistence. Return null if not found.
                return new UserDto(request.Id, "sample-user", "sample@example.com");
            }
        }
    }

    /// <summary>
    /// Query to fetch all users.
    /// </summary>
    public class GetUsersQuery : IRequest<IEnumerable<UserDto>>
    {
        public class Handler : IRequestHandler<GetUsersQuery, IEnumerable<UserDto>>
        {
            public async Task<IEnumerable<UserDto>> HandleAsync(GetUsersQuery request, CancellationToken ct)
            {
                // TODO: return real data from persistence
                return new[] { new UserDto(Guid.NewGuid(), "sample-user", "sample@example.com") };
            }
        }
    }
}
