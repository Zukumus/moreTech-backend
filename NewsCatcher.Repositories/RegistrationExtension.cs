using Microsoft.Extensions.DependencyInjection;
using NewsCatcher.Repositories.Contracts.Abstract;

namespace NewsCatcher.Repositories;

public static class RegistrationExtension
{
    public static IServiceCollection AddNewsCatcherRepository(this IServiceCollection services)
    {
        services.AddTransient<IGetNewsByKeyRepository, GetNewsByKeyRepository>();
        return services;
    }
}