using SPerfomance.Application.Abstractions;
using SPerfomance.Application.EducationDirections.Commands.CreateEducationDirection;
using SPerfomance.Application.EducationDirections.Commands.RemoveEducationDirection;
using SPerfomance.Application.EducationDirections.Commands.UpdateEducationDirection;
using SPerfomance.Application.EducationDirections.Queries.GetEducationDirection;
using SPerfomance.DataAccess.Repositories;
using SPerfomance.Domain.Models.EducationDirections;
using SPerfomance.Domain.Models.EducationDirections.Abstractions;

namespace SPerfomance.Api.Features.EducationDirections.Configuration;

public static class ConfigureEducationDirections
{
    public static IServiceCollection ConfigureEducationDirectionsComponents(
        this IServiceCollection services
    )
    {
        services = services.ConfigureRepository().ConfigureCommands().ConfigureQueries();
        return services;
    }

    private static IServiceCollection ConfigureRepository(this IServiceCollection services)
    {
        services.AddScoped<IEducationDirectionRepository, EducationDirectionRepository>();
        return services;
    }

    private static IServiceCollection ConfigureCommands(this IServiceCollection services)
    {
        services.AddTransient<
            ICommandHandler<CreateEducationDirectionCommand, EducationDirection>,
            CreateEducationDirectionCommandHandler
        >();
        services.AddTransient<
            ICommandHandler<RemoveEducationDirectionCommand, EducationDirection>,
            RemoveEducationDirectionCommandHandler
        >();
        services.AddTransient<
            ICommandHandler<UpdateEducationDirectionCommand, EducationDirection>,
            UpdateEducationDirectionCommandHandler
        >();
        return services;
    }

    private static IServiceCollection ConfigureQueries(this IServiceCollection services)
    {
        services.AddTransient<
            IQueryHandler<GetEducationDirectionQuery, EducationDirection>,
            GetEducationDirectionQueryHandler
        >();
        return services;
    }
}
