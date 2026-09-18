using DeepuRider.Practice.CQRS.Core;

namespace DeepuRider.Practice.CQRS.Handlers.Users
{
    /// <summary>
    /// Command used to update an existing user.
    /// </summary>
    public class UpdateUserCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }

        public class Handler : IRequestHandler<UpdateUserCommand, bool>
        {
            public async Task<bool> HandleAsync(UpdateUserCommand request, CancellationToken ct)
            {
                // TODO: perform update logic (persistence) here
                return true;
            }
        }
    }
}
