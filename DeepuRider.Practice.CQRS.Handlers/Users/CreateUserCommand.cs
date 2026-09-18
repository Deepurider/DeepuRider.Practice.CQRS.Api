using DeepuRider.Practice.CQRS.Core;

namespace DeepuRider.Practice.CQRS.Handlers.Users
{
    public class CreateUserCommand : IRequest<bool>
    {
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public class Handler : IRequestHandler<CreateUserCommand, bool>
        {
            public async Task<bool> HandleAsync(CreateUserCommand request, CancellationToken ct)
            {
                return true;
            }
        }
    }
}
