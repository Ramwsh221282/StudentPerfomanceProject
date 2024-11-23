using SPerfomance.Application.Services.Authentication.Abstractions;

namespace SPerfomance.Api.Features.Users.Configurations;

public static class AuthConfiguration
{
    public static IServiceCollection ConfigureAuth(this IServiceCollection services)
    {
        services = services
            .AddSingleton<IPasswordHasher, PasswordHasher>()
            .AddSingleton<IPasswordGenerator, PasswordGenerator>()
            .AddSingleton<IJwtTokenService, JwtTokenService>();
        return services;
    }
}
