using SPerfomance.Application.Abstractions;
using SPerfomance.Application.EducationPlans.Commands.ChangeEducationPlanYear;
using SPerfomance.Application.EducationPlans.Commands.CreateEducationPlan;
using SPerfomance.Application.EducationPlans.Commands.RemoveEducationPlan;
using SPerfomance.Application.EducationPlans.Queries.GetEducationPlan;
using SPerfomance.DataAccess.Repositories;
using SPerfomance.Domain.Models.EducationPlans;
using SPerfomance.Domain.Models.EducationPlans.Abstractions;

namespace SPerfomance.Api.Features.EducationPlans.Configuration;

public static class EducationPlansConfiguration
{
    public static IServiceCollection ConfigureEducationPlans(this IServiceCollection services)
    {
        services = services.ConfigureRepository().ConfigureCommands().ConfigureQueries();
        return services;
    }

    private static IServiceCollection ConfigureRepository(this IServiceCollection services)
    {
        services.AddScoped<IEducationPlansRepository, EducationPlansRepository>();
        return services;
    }

    private static IServiceCollection ConfigureCommands(this IServiceCollection services)
    {
        services.AddTransient<
            ICommandHandler<CreateEducationPlanCommand, EducationPlan>,
            CreateEducationPlanCommandHandler
        >();
        services.AddTransient<
            ICommandHandler<ChangeEducationPlanYearCommand, EducationPlan>,
            ChangeEducationPlanYearCommandHandler
        >();
        services.AddTransient<
            ICommandHandler<RemoveEducationPlanCommand, EducationPlan>,
            RemoveEducationPlanCommandHandler
        >();
        return services;
    }

    private static IServiceCollection ConfigureQueries(this IServiceCollection services)
    {
        services.AddTransient<
            IQueryHandler<GetEducationPlanQuery, EducationPlan>,
            GetEducationPlanQueryHandler
        >();
        return services;
    }
}
