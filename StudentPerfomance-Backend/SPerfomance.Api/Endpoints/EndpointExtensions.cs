using System.Reflection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace SPerfomance.Api.Endpoints;

public static class EndpointExtensions
{
    public static IServiceCollection AddEndpoints(this IServiceCollection services)
    {
        services.AddEndpoints(Assembly.GetExecutingAssembly());
        return services;
    }

    public static IServiceCollection AddEndpoints(
        this IServiceCollection services,
        Assembly assembly
    )
    {
        ServiceDescriptor[] descriptors = assembly
            .DefinedTypes.Where(t =>
                t is { IsAbstract: false, IsInterface: false }
                && t.IsAssignableTo(typeof(IEndpoint))
            )
            .Select(t => ServiceDescriptor.Transient(typeof(IEndpoint), t))
            .ToArray();

        services.TryAddEnumerable(descriptors);
        return services;
    }

    public static IApplicationBuilder MapEndpoints(
        this WebApplication app,
        RouteGroupBuilder? builder = null
    )
    {
        IEnumerable<IEndpoint> endpoints = app.Services.GetRequiredService<
            IEnumerable<IEndpoint>
        >();
        IEndpointRouteBuilder routeBuilder = builder is null ? app : builder;

        foreach (IEndpoint endpoint in endpoints)
        {
            endpoint.MapEndpoint(routeBuilder);
        }

        return app;
    }
}
