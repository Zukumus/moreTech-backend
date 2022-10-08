namespace MoreTech.Data.Repository.Contracts;

/// <summary>
/// Репозиторий для получения новостей
/// </summary>
public interface IGetNewsRepository
{
    /// <summary>
    /// Получить новости по ключевому слову
    /// </summary>
    Task<IReadOnlyCollection<NewsRepositoryModel>> GetTopNewsByKeyWord(string keyWord, CancellationToken token);
}