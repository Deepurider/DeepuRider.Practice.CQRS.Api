using Microsoft.Extensions.DependencyInjection;

namespace DeepuRider.Practice.CQRS.Core;

/// <summary>
/// Represents a request (command or query) that expects a response of type <typeparamref name="TResponse"/>.
/// Implementations are simple marker types used to route requests to their handlers.
/// </summary>
/// <typeparam name="TResponse">The type of response returned by the request.</typeparam>
public interface IRequest<TResponse> { }

/// <summary>
/// Handles a request of type <typeparamref name="TRequest"/> and produces a <typeparamref name="TResponse"/>.
/// </summary>
/// <typeparam name="TRequest">The request type.</typeparam>
/// <typeparam name="TResponse">The response type.</typeparam>
public interface IRequestHandler<in TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    /// <summary>
    /// Handle the provided request and return a response asynchronously.
    /// </summary>
    /// <param name="request">The request instance to handle.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>The response.</returns>
    Task<TResponse> HandleAsync(TRequest request, CancellationToken ct);
}

/// <summary>
/// Application-level mediator abstraction used to dispatch <see cref="IRequest{TResponse}"/> instances
/// to their corresponding <see cref="IRequestHandler{TRequest,TResponse}"/> implementations.
/// </summary>
public interface IApplicationMediator
{
    /// <summary>
    /// Send a request to its handler and get the response.
    /// The generic constraint ensures the compiler understands the relationship between the request and response types.
    /// </summary>
    /// <typeparam name="TRequest">The concrete request type implementing <see cref="IRequest{TResponse}"/>.</typeparam>
    /// <typeparam name="TResponse">The response type produced by the handler.</typeparam>
    /// <param name="request">The request instance to dispatch.</param>
    /// <param name="ct">Optional cancellation token.</param>
    /// <returns>The handler's response.</returns>
    Task<TResponse> SendAsync<TRequest, TResponse>(TRequest request, CancellationToken ct = default)
        where TRequest : IRequest<TResponse>;
}

/// <summary>
/// Simple mediator implementation that resolves request handlers from the application's
/// <see cref="IServiceProvider"/> (DI container) and invokes them.
/// This implementation is intentionally small and depends on handlers being registered in DI.
/// </summary>
public class ServiceProviderMediator : IApplicationMediator
{
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// Create a new instance using the provided service provider
    /// (usually the application's root/service scope provider).
    /// </summary>
    public ServiceProviderMediator(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

    /// <inheritdoc />
    public Task<TResponse> SendAsync<TRequest, TResponse>(TRequest request, CancellationToken ct = default)
        where TRequest : IRequest<TResponse>
    {
        // Resolve the handler for the given request/response pair from DI and invoke it.
        var handler = _serviceProvider.GetRequiredService<IRequestHandler<TRequest, TResponse>>();
        return handler.HandleAsync(request, ct);
    }
}
