using Microsoft.Extensions.DependencyInjection;
using MoreTech.Data.Repository.Contracts;

namespace MoreTech.Data.Repository.Services;

public static class RegistrationExtension
{
    public static IServiceCollection AddDataContextRepository(this IServiceCollection services)
    {
        services.AddScoped<ICreateNewsRepository, CreateNewsRepository>();
        services.AddScoped<IGetNewsRepository, GetNewsRepository>();
        return services;
    }
}