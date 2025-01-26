using SPerfomance.Application.Abstractions;
using SPerfomance.Application.PasswordRecoveryContext.Commands.RegisterPasswordRecovery;
using SPerfomance.Application.PasswordRecoveryContext.Commands.ResolvePasswordRecovery;
using SPerfomance.Application.PasswordRecoveryContext.Models;
using SPerfomance.Application.PasswordRecoveryContext.RepositoryAbstraction;
using SPerfomance.Application.Users.Commands.ChangeEmail;
using SPerfomance.Application.Users.Commands.ChangePassword;
using SPerfomance.Application.Users.Commands.RegisterAsTeacher;
using SPerfomance.Application.Users.Commands.UpdateUser;
using SPerfomance.DataAccess.Repositories;
using SPerfomance.Domain.Models.Teachers;
using SPerfomance.Domain.Models.Users;
using SPerfomance.PasswordRecovery.DataAccess.Repository;

namespace SPerfomance.Api.Features.Users.Configurations;

public static class UsersConfiguration
{
    public static IServiceCollection ConfigureUsers(this IServiceCollection services)
    {
        services = services.ConfigureRepository().ConfigureCommands();
        return services;
    }

    private static IServiceCollection ConfigureCommands(this IServiceCollection services)
    {
        services = services
            .AddTransient<ICommandHandler<ChangeEmailCommand, User>, ChangeEmailCommandHandler>()
            .AddTransient<
                ICommandHandler<ChangePasswordCommand, User>,
                ChangePasswordCommandHandler
            >()
            .AddTransient<
                ICommandHandler<RegisterPasswordRecoveryCommand, PasswordRecoveryTicket>,
                RegisterPasswordRecoveryCommandHandler
            >()
            .AddTransient<
                ICommandHandler<ResolvePasswordRecoveryCommand, PasswordRecoveryTicket>,
                ResolvePasswordRecoveryCommandHandler
            >()
            .AddTransient<
                ICommandHandler<RegisterAsTeacherRequest, Teacher>,
                RegisterAsTeacherCommandHandler
            >()
            .AddTransient<ICommandHandler<UpdateUserCommand, User>, UpdateUserCommandHandler>();
        return services;
    }

    private static IServiceCollection ConfigureRepository(this IServiceCollection services)
    {
        services = services
            .AddScoped<IUsersRepository, UsersRepository>()
            .AddScoped<IPasswordRecoveryTicketsRepository, RecoveryTicketsRepository>();
        return services;
    }
}
