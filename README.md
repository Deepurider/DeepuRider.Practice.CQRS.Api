# DeepuRider.Practice.CQRS

Short guide to the custom, minimal CQRS + mediator implementation used in this solution.

Targets: .NET 10

Projects
- DeepuRider.Practice.CQRS.Core  — core mediator and abstractions (IRequest, IRequestHandler, IApplicationMediator).
- DeepuRider.Practice.CQRS.Handlers — concrete request/handler implementations and DI registration helpers.
- DeepuRider.Practice.CQRS.Api — ASP.NET Web API surface (controllers) that dispatch requests through the mediator.

Key points
- This mediator implementation is intentionally small and resolves handlers from the DI container.
- Important: this implementation is without reflection. Handlers are registered explicitly in
  DeepuRider.Practice.CQRS.Handlers.IExtensionServiceCollection.AddHandlers().
  (There is commented example code showing how to switch to assembly scanning if you prefer reflection-based registration.)

How it works (overview)
- Define a request: implement IRequest<TResponse> (a marker that pairs a request type with a response type).
- Implement a handler: IRequestHandler<TRequest, TResponse> with HandleAsync(TRequest, CancellationToken).
- Register your handler in the Handlers project (AddHandlers) so it can be resolved by DI.
- Use the application mediator (IApplicationMediator) from controllers or services and call SendAsync(request).

Registration (Program.cs)

Add the handlers/DI registration at application startup. Example (already present in the sample Program.cs):

	builder.Services.AddHandlers();

This method registers handlers and the mediator implementation (ServiceProviderMediator) into the service collection.

Controller usage example

Controllers inherit BaseController which exposes a Mediator property resolved from request services. Example:

	public class UserController : BaseController
	{
		[HttpPost]
		public async Task<IActionResult> Post(CreateUserCommand command, CancellationToken cancellationToken)
		{
			var result = await Mediator.SendAsync<CreateUserCommand, bool>(command, cancellationToken);
			return Ok(result);
		}
	}

Adding a new request + handler
1. Create a request type: public class MyRequest : IRequest<MyResponse> { ... }
2. Create a handler: public class Handler : IRequestHandler<MyRequest, MyResponse> { public Task<MyResponse> HandleAsync(MyRequest r, CancellationToken ct) { ... } }
3. Register the handler in DeepuRider.Practice.CQRS.Handlers.IExtensionServiceCollection.AddHandlers():
	   services.AddScoped<IRequestHandler<MyRequest, MyResponse>, MyRequest.Handler>();

Notes and extension ideas
- The current design uses explicit registrations (no reflection). If you want automatic discovery, review the commented
  services.Scan(...) snippet in DeepuRider.Practice.CQRS.Handlers.IExtensionServiceCollection.cs and enable assembly scanning.
- The mediator implementation (ServiceProviderMediator) simply resolves IRequestHandler<TRequest,TResponse> from DI and invokes HandleAsync.
  Keep handlers registered with an appropriate lifetime (scoped is typical for web APIs).

Build & run
- dotnet build
- dotnet run (from the Api project directory) or run via Visual Studio.

License
- This workspace is for practice and internal use — adapt as needed.
