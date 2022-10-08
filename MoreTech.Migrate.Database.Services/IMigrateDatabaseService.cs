namespace MoreTech.Migrate.Database.Services;

/// <summary>
/// Сервис для накатывания миграций
/// </summary>
public interface IMigrateDatabaseService
{
    /// <summary>
    /// Применить миграции на БД
    /// </summary>
    Task Migrate();

    /// <summary>
    /// Пересоздать БД
    /// </summary>
    Task Recreate();
}
