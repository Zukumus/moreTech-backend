using Microsoft.Extensions.DependencyInjection;
using MoreTech.Data.Repository.Contracts;

namespace MoreTech.Data.Repository.Services;

public static class RegistrationExtension
{
    public static IServiceCollection AddDataContextRepository(this IServiceCollection services)
    {
        services.AddTransient<ICreateNewsRepository, CreateNewsRepository>();
        services.AddTransient<IGetNewsRepository, GetNewsRepository>();
        return services;
    }
}