using Newscatcher.Client.Contracts.Models;

namespace Newscatcher.Client.Contracts.Abstract;

/// <summary>
/// Клиент к сервису агрегации новостей
/// </summary>
public interface INewCatcherClient
{
    /// <summary>
    /// Получить новости по ключевому слову
    /// </summary>
    Task<IReadOnlyCollection<NewsModel>> GetNewsByKeyWord(string keyWord, CancellationToken token);
    
    /// <summary>
    /// Получить новости по ключевому слову и диапазону дат
    /// </summary>
    Task<IReadOnlyCollection<NewsModel>> GetNewsByKeyWordAndDateRange(string keyWord, DateTimeOffset startDate, DateTimeOffset endDate, CancellationToken token);
}