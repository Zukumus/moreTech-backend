namespace NewsCatcher.Repositories.Contracts.Models;

/// <summary>
/// Модель новости
/// </summary>
public class NewsRepositoryModel
{
    /// <summary>
    /// Заголовок
    /// </summary>
    public string Title { get; init; }
    
    /// <summary>
    /// Резюме
    /// </summary>
    public string Summary { get; init; }

    /// <summary>
    /// Дата публикации в источнике
    /// </summary>
    public DateTime PublishDate { get; init; }

    /// <summary>
    /// url источника
    /// </summary>
    public string SourceUrl { get; set; }
}