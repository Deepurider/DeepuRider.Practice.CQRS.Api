using DeepuRider.Practice.CQRS.Core;

namespace DeepuRider.Practice.CQRS.Handlers.Users
{
    /// <summary>
    /// Command to delete a user by identifier.
    /// </summary>
    public class DeleteUserCommand : IRequest<bool>
    {
        public Guid Id { get; set; }

        public class Handler : IRequestHandler<DeleteUserCommand, bool>
        {
            public async Task<bool> HandleAsync(DeleteUserCommand request, CancellationToken ct)
            {
                // TODO: perform delete logic (persistence) here
                return true;
            }
        }
    }
}
