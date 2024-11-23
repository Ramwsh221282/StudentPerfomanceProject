using SPerfomance.Application.Services.Mailing;

namespace SPerfomance.Api.Features.Users.Configurations;

public static class ConfigureMailing
{
    public static IServiceCollection ConfigureMailru(this IServiceCollection services)
    {
        services = services.AddSingleton<IMailingService, MailingService>();
        return services;
    }
}
