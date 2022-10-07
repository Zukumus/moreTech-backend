using NewsCatcher.Repositories.Contracts.Models;

namespace NewsCatcher.Repositories.Contracts.Abstract;

/// <summary>
/// Репозиторий для получения новости по ключевому слову
/// </summary>
public interface IGetNewsByKeyRepository
{
    /// <summary>
    /// Получить новости по ключевому слову
    /// </summary>
    Task<IReadOnlyCollection<NewsRepositoryModel>> GetNewsByKeyWord(string key, CancellationToken token);
    
    /// <summary>
    /// Получить новости по ключевому слову и диапазону дат
    /// </summary>
    Task<IReadOnlyCollection<NewsRepositoryModel>> GetNewsByKeyWordAndDateRange(string key, DateTime starDate, DateTime endDate, CancellationToken token);
}