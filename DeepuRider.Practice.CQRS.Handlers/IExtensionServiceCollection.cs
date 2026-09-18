using DeepuRider.Practice.CQRS.Core;
using DeepuRider.Practice.CQRS.Handlers.Users;
using Microsoft.Extensions.DependencyInjection;

namespace DeepuRider.Practice.CQRS.Handlers
{
    public static class IExtensionServiceCollection
    {
        public static void AddHandlers(this IServiceCollection services)
        {
            // Register all user-related handlers explicitly (no reflection)
            services.AddScoped<IRequestHandler<CreateUserCommand, bool>, CreateUserCommand.Handler>();
            services.AddScoped<IRequestHandler<UpdateUserCommand, bool>, UpdateUserCommand.Handler>();
            services.AddScoped<IRequestHandler<DeleteUserCommand, bool>, DeleteUserCommand.Handler>();
            services.AddScoped<IRequestHandler<GetUserQuery, Users.UserDto?>, GetUserQuery.Handler>();
            services.AddScoped<IRequestHandler<GetUsersQuery, IEnumerable<Users.UserDto>>, GetUsersQuery.Handler>();

            services.AddDependencies();
        }
        private static void AddDependencies(this IServiceCollection services)
        {
            services.AddCore();
        }
    }
}


// Auto-register all MediatR request handlers from the assembly
// where CreateUserCommand is defined.
//
// This scans the assembly for classes implementing
// IRequestHandler<,> and registers them as their
// implemented interfaces with a scoped lifetime.

// services.Scan(scan => scan
//     // Scan the assembly containing CreateUserCommand.
//     .FromAssemblyOf<CreateUserCommand>()
//
//     // Find all classes implementing IRequestHandler<,>.
//     .AddClasses(classes =>
//         classes.AssignableTo(typeof(IRequestHandler<,>)))
//
//     // Register classes using their implemented interfaces.
//     .AsImplementedInterfaces()
//
//     // Register services with a scoped lifetime.
//     .WithScopedLifetime());
