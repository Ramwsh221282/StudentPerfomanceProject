using SPerfomance.DataAccess.Repositories;

namespace SPerfomance.Api.Features.Users.Configurations;

public static class UsersConfiguration
{
    public static IServiceCollection ConfigureUsers(this IServiceCollection services)
    {
        services = services.ConfigureRepository();
        return services;
    }

    private static IServiceCollection ConfigureRepository(this IServiceCollection services)
    {
        services = services.AddScoped<IUsersRepository, UsersRepository>();
        return services;
    }
}
