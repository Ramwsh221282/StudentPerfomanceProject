using SPerfomance.Application.Abstractions;
using SPerfomance.Application.Semesters.Commands.AttachTeacherToDiscipline;
using SPerfomance.Application.Semesters.Commands.ChangeDisciplineName;
using SPerfomance.Application.Semesters.Commands.CreateDiscipline;
using SPerfomance.Application.Semesters.Commands.DeattachTeacherFromDiscipline;
using SPerfomance.Application.Semesters.Commands.RemoveDiscipline;
using SPerfomance.Application.Semesters.Queries.GetDisciplineFromSemester;
using SPerfomance.Application.Semesters.Queries.GetSemester;
using SPerfomance.DataAccess.Repositories;
using SPerfomance.Domain.Models.SemesterPlans;
using SPerfomance.Domain.Models.SemesterPlans.Abstractions;
using SPerfomance.Domain.Models.Semesters;

namespace SPerfomance.Api.Features.Semesters.Configuration;

public static class SemestersConfiguration
{
    public static IServiceCollection ConfigureSemesters(this IServiceCollection services)
    {
        services = services.ConfigureRepositories().ConfigureCommands().ConfigureQueries();
        return services;
    }

    private static IServiceCollection ConfigureRepositories(this IServiceCollection services)
    {
        services = services.AddScoped<ISemesterPlansRepository, SemesterPlansRepository>();
        return services;
    }

    private static IServiceCollection ConfigureCommands(this IServiceCollection services)
    {
        services = services
            .AddTransient<
                ICommandHandler<AttachTeacherToDisciplineCommand, SemesterPlan>,
                AttachTeacherToDisciplineCommandHandler
            >()
            .AddTransient<
                ICommandHandler<ChangeDisciplineNameCommand, SemesterPlan>,
                ChangeDisciplineNameCommandHandler
            >()
            .AddTransient<
                ICommandHandler<CreateDisciplineCommand, SemesterPlan>,
                CreateDisciplineCommandHandler
            >()
            .AddTransient<
                ICommandHandler<DeattachTeacherFromDisciplineCommand, SemesterPlan>,
                DeattachTeacherFromDisciplineCommandHandler
            >()
            .AddTransient<
                ICommandHandler<RemoveDisciplineCommand, SemesterPlan>,
                RemoveDisciplineCommandHandler
            >();
        return services;
    }

    private static IServiceCollection ConfigureQueries(this IServiceCollection services)
    {
        services = services
            .AddTransient<
                IQueryHandler<GetDisciplineFromSemesterQuery, SemesterPlan>,
                GetDisciplineFromSemesterQueryHandler
            >()
            .AddTransient<IQueryHandler<GetSemesterQuery, Semester>, GetSemesterQueryHandler>();
        return services;
    }
}
