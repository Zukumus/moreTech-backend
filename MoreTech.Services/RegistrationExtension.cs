using Microsoft.Extensions.DependencyInjection;
using MoreTech.Service.Contracts.Abstract;

namespace MoreTech.Services;

public static class RegistrationExtension
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IExportNewsFromSourceToFileByKeyWordService, ExportNewsFromSourceToFileByKeyWordService>();
        services.AddScoped<ISaveFileService, SaveFileService>();
        services.AddScoped<IExportNewsFromFileToDatabase, ExportNewsFromFileToDatabase>();
        services.AddScoped<IExportNewsFromSourceToDataBase, ExportNewsFromSourceToDataBase>();
        return services;
    }
}