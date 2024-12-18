using SPerfomance.Application.Abstractions;
using SPerfomance.Application.StudentGroups.Commands.AddStudentCommand;
using SPerfomance.Application.StudentGroups.Commands.AttachEducationPlan;
using SPerfomance.Application.StudentGroups.Commands.ChangeGroupName;
using SPerfomance.Application.StudentGroups.Commands.CreateStudentGroup;
using SPerfomance.Application.StudentGroups.Commands.DeattachEducationPlan;
using SPerfomance.Application.StudentGroups.Commands.MergeWithGroup;
using SPerfomance.Application.StudentGroups.Commands.MoveStudentToOtherGroup;
using SPerfomance.Application.StudentGroups.Commands.RemoveStudent;
using SPerfomance.Application.StudentGroups.Commands.RemoveStudentGroup;
using SPerfomance.Application.StudentGroups.Commands.UpdateStudent;
using SPerfomance.Application.StudentGroups.Queries.GetStudentFromGroup;
using SPerfomance.Application.StudentGroups.Queries.GetStudentGroupByName;
using SPerfomance.DataAccess.Repositories;
using SPerfomance.Domain.Models.StudentGroups;
using SPerfomance.Domain.Models.StudentGroups.Abstractions;
using SPerfomance.Domain.Models.Students;
using SPerfomance.Domain.Models.Students.Abstractions;

namespace SPerfomance.Api.Features.StudentGroups.Configurations;

public static class StudentGroupsConfiguration
{
    public static IServiceCollection ConfigureStudentGroups(this IServiceCollection services)
    {
        services = services.ConfigureRepositories().ConfigureCommands().ConfigureQueries();
        return services;
    }

    private static IServiceCollection ConfigureRepositories(this IServiceCollection services)
    {
        services = services
            .AddScoped<IStudentGroupsRepository, StudentGroupsRepository>()
            .AddScoped<IStudentsRepository, StudentsRepository>();
        return services;
    }

    private static IServiceCollection ConfigureCommands(this IServiceCollection services)
    {
        services = services
            .AddTransient<ICommandHandler<AddStudentCommand, Student>, AddStudentCommandHandler>()
            .AddTransient<
                ICommandHandler<AttachEducationPlanCommand, StudentGroup>,
                AttachEducationPlanCommandHandler
            >()
            .AddTransient<
                ICommandHandler<ChangeGroupNameCommand, StudentGroup>,
                ChangeGroupNameCommandHandler
            >()
            .AddTransient<
                ICommandHandler<CreateStudentGroupCommand, StudentGroup>,
                CreateStudentGroupCommandHandler
            >()
            .AddTransient<
                ICommandHandler<DeattachEducationPlanCommand, StudentGroup>,
                DeattachEducationPlanCommandHandler
            >()
            .AddTransient<
                ICommandHandler<MergeWithGroupCommand, StudentGroup>,
                MergeWithGroupCommandHandler
            >()
            .AddTransient<
                ICommandHandler<RemoveStudentCommand, Student>,
                RemoveStudentCommandHandler
            >()
            .AddTransient<
                ICommandHandler<RemoveStudentGroupCommand, StudentGroup>,
                RemoveStudentGroupCommandHandler
            >()
            .AddTransient<
                ICommandHandler<UpdateStudentCommand, Student>,
                UpdateStudentCommandHandler
            >()
            .AddTransient<
                ICommandHandler<MoveStudentToOtherGroupCommand, Student>,
                MoveStudentToOtherGroupCommandHandler
            >();
        return services;
    }

    private static IServiceCollection ConfigureQueries(this IServiceCollection services)
    {
        services = services
            .AddTransient<
                IQueryHandler<GetStudentFromGroupQuery, Student>,
                GetStudentFromGroupQueryHandler
            >()
            .AddTransient<
                IQueryHandler<GetStudentGroupQuery, StudentGroup>,
                GetStudentGroupQueryHandler
            >();
        return services;
    }
}
