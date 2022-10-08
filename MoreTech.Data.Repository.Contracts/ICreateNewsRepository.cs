namespace MoreTech.Data.Repository.Contracts;

/// <summary>
/// Репозиторий для созданий новостей
/// </summary>
public interface ICreateNewsRepository
{
    /// <summary>
    /// Добавить список новостей
    /// </summary>
    Task CreateNews(IEnumerable<NewsRepositoryModel> NewsModels, CancellationToken token);
}