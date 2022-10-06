namespace MoreTech.Configuration;

/// <summary>
/// Настройка клиента к сервису агрегации новостей
/// </summary>
public interface INewsCatcherClientConfiguration
{
    /// <summary>
    /// Базовый url до сервиса агрегации новостей
    /// </summary>
    public Uri NewsCatcherBaseAddress { get; }

    /// <summary>
    /// Токен авторизации
    /// </summary>
    public string ApiKey { get; }
}