using Microsoft.Extensions.DependencyInjection;
using MoreTech.Service.Contracts.Abstract;

namespace MoreTech.Services;

public static class RegistrationExtension
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IExportNewsFromSourceToFile, ExportNewsFromSourceToFile>();
        return services;
    }
}