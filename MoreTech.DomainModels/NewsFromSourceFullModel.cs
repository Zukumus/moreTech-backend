namespace MoreTech.DomainModels;

/// <summary>
/// Полная модель новости из источника
/// </summary>
public class NewsFromSourceFullModel
{
    /// <summary>
    /// Ключевое слово для поиска новости
    /// </summary>
    public string KeyWord { get; init; }

    /// <summary>
    /// Заголовок
    /// </summary>
    public string Title { get; set; }
    
    /// <summary>
    /// Резюме
    /// </summary>
    public string Summary { get; set; }

    /// <summary>
    /// url источника
    /// </summary>
    public string SourceUrl { get; set; }

    /// <summary>
    /// Дата публикации в источнике
    /// </summary>
    public DateTime PublishDate { get; set; }
}