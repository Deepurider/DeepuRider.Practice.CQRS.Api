using Microsoft.Extensions.DependencyInjection;

namespace DeepuRider.Practice.CQRS.Core
{
    public static class IExtensionServiceCollection
    {
        public static void AddCore(this IServiceCollection services)
        {
            // Register the application mediator implementation used to dispatch requests to handlers.
            services.AddScoped<IApplicationMediator, ServiceProviderMediator>();
        }
    }
}
