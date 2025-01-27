using SPerfomance.Api.Features.PerfomanceContext.Contracts;
using SPerfomance.Application.Abstractions;
using SPerfomance.Application.PerfomanceContext.AssignmentSessions.Abstractions;
using SPerfomance.Application.PerfomanceContext.AssignmentSessions.Commands.CloseAssignmentSession;
using SPerfomance.Application.PerfomanceContext.AssignmentSessions.Commands.Create;
using SPerfomance.Application.PerfomanceContext.AssignmentSessions.Commands.MakeAssignment;
using SPerfomance.Application.PerfomanceContext.AssignmentSessions.DTO;
using SPerfomance.Application.PerfomanceContext.AssignmentSessions.Queries.GetInfo;
using SPerfomance.ControlWeekDocuments.Documents;
using SPerfomance.DataAccess.Repositories;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSession;
using SPerfomance.Domain.Models.PerfomanceContext.Models.AssignmentSession.Abstractions;
using SPerfomance.Domain.Models.PerfomanceContext.Models.StudentAssignments;
using SPerfomance.Statistics.DataAccess.Repositories;

namespace SPerfomance.Api.Features.PerfomanceContext.Configuration;

public static class PerfomanceContextConfiguration
{
    public static IServiceCollection ConfigurePerfomanceContext(this IServiceCollection services)
    {
        services = services.ConfigureRepositories().ConfigureCommands().ConfigureQueries();
        return services;
    }

    private static IServiceCollection ConfigureRepositories(this IServiceCollection services)
    {
        services = services
            .AddScoped<IAssignmentSessionsRepository, AssignmentSessionsRepository>()
            .AddScoped<IStudentAssignmentsRepository, StudentAssignmentsRepository>()
            .AddScoped<IControlWeekReportRepository, ControlWeekRepository>()
            .AddScoped<IControlWeekGroupDocument, GroupControlWeekDocument>();
        return services;
    }

    private static IServiceCollection ConfigureCommands(this IServiceCollection services)
    {
        services = services
            .AddTransient<
                ICommandHandler<CloseAssignmentSessionCommand, AssignmentSession>,
                CloseAssignmentSessionCommandHandler
            >()
            .AddTransient<
                ICommandHandler<CreateAssignmentSessionCommand, AssignmentSession>,
                CreateAssignmentSessionCommandHandler
            >()
            .AddTransient<
                ICommandHandler<MakeAssignmentCommand, StudentAssignment>,
                MakeAssignmentCommandHandler
            >();
        return services;
    }

    private static IServiceCollection ConfigureQueries(this IServiceCollection services)
    {
        services = services.AddTransient<
            IQueryHandler<GetAssignmentSessionInfoQuery, AssignmentSessionInfoDTO>,
            GetAssignmentSessionInfoQueryHandler
        >();
        return services;
    }
}
