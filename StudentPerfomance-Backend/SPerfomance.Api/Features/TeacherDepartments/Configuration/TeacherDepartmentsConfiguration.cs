using SPerfomance.Application.Abstractions;
using SPerfomance.Application.Departments.Commands.ChangeTeachersDepartmentName;
using SPerfomance.Application.Departments.Commands.CreateTeachersDepartment;
using SPerfomance.Application.Departments.Commands.FireTeacher;
using SPerfomance.Application.Departments.Commands.RegisterTeacher;
using SPerfomance.Application.Departments.Commands.RemoveTeachersDepartment;
using SPerfomance.Application.Departments.Commands.UpdateTeacher;
using SPerfomance.Application.Departments.Queries.GetDepartmentByName;
using SPerfomance.Application.Departments.Queries.GetTeacherFromDepartment;
using SPerfomance.DataAccess.Repositories;
using SPerfomance.Domain.Models.TeacherDepartments;
using SPerfomance.Domain.Models.TeacherDepartments.Abstractions;
using SPerfomance.Domain.Models.Teachers;
using SPerfomance.Domain.Models.Teachers.Abstractions;

namespace SPerfomance.Api.Features.TeacherDepartments.Configuration;

public static class TeacherDepartmentsConfiguration
{
    public static IServiceCollection ConfigureTeacherDepartments(this IServiceCollection services)
    {
        services = services.ConfigureRepository().ConfigureCommands().ConfigureQueries();
        return services;
    }

    private static IServiceCollection ConfigureRepository(this IServiceCollection services)
    {
        services = services
            .AddScoped<ITeacherDepartmentsRepository, TeacherDepartmentsRepository>()
            .AddScoped<ITeachersRepository, TeachersRepository>();
        return services;
    }

    private static IServiceCollection ConfigureCommands(this IServiceCollection services)
    {
        services = services
            .AddTransient<
                ICommandHandler<CreateTeachersDepartmentCommand, TeachersDepartments>,
                CreateTeachersDepartmentCommandHandler
            >()
            .AddTransient<
                ICommandHandler<ChangeTeachersDepartmentNameCommand, TeachersDepartments>,
                ChangeTeachersDepartmentNameCommandHandler
            >()
            .AddTransient<
                ICommandHandler<RemoveTeachersDepartmentCommand, TeachersDepartments>,
                RemoveTeachersDepartmentCommandHandler
            >()
            .AddTransient<ICommandHandler<FireTeacherCommand, Teacher>, FireTeacherCommandHandler>()
            .AddTransient<
                ICommandHandler<RegisterTeacherCommand, Teacher>,
                RegisterTeacherCommandHandler
            >()
            .AddTransient<
                ICommandHandler<UpdateTeacherCommand, Teacher>,
                UpdateTeacherCommandHandler
            >();
        return services;
    }

    private static IServiceCollection ConfigureQueries(this IServiceCollection services)
    {
        services = services
            .AddTransient<
                IQueryHandler<GetDepartmentByNameQuery, TeachersDepartments>,
                GetDepartmentByNameQueryHandler
            >()
            .AddTransient<
                IQueryHandler<GetTeacherFromDepartmentQuery, Teacher>,
                GetTeacherFromDepartmentQueryHandler
            >();
        return services;
    }
}
