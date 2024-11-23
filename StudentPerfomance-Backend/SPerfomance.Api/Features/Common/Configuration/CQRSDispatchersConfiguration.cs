using SPerfomance.Application.Abstractions;
using SPerfomance.Application.Common;

namespace SPerfomance.Api.Features.Common.Configuration;

public static class CqrsDispatchersConfiguration
{
    public static IServiceCollection ConfigureCqrsDispatchers(this IServiceCollection services)
    {
        services.AddScoped<ICommandDispatcher, CommandDispatcher>();
        services.AddScoped<IQueryDispatcher, QueryDispatcher>();
        return services;
    }
}
