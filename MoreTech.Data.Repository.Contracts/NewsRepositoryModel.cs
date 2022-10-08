namespace MoreTech.Data.Repository.Contracts;

/// <summary>
/// Модель новости для репозитория
/// </summary>
public class NewsRepositoryModel
{
    /// <summary>
    /// Роль
    /// </summary>
    public string Role { get; set; }
    
    /// <summary>
    /// Ключевое слово по которому был произведен поиск
    /// </summary>
    public string KeyWord { get; set; }

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