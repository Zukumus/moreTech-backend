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
}