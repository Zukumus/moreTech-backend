using Microsoft.Extensions.DependencyInjection;

namespace MoreTech.Migrate.Database.Services;

public static class RegistrationExtension
{
    /// <summary>
    /// Зарегистрировать сервисы в ServiceCollection
    /// </summary>
    public static IServiceCollection AddMigrationServices(this IServiceCollection services)
    {
        services.AddTransient<IMigrateDatabaseService, MigrateDatabaseService>();
        return services;
    }
}
