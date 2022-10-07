namespace MoreTech.DomainModels;

/// <summary>
/// Ключевое слово с датой для поиска в источнике новостей
/// </summary>
public class KeyWordWithDateModel
{
    /// <summary>
    /// Роль пользователя
    /// </summary>
    public string UserRole { get; init; }
    
    /// <summary>
    /// Ключевое слово для поиска новости
    /// </summary>
    public string KeyWord { get; init; }

    /// <summary>
    /// Дата начала
    /// </summary>
    public string StartDate { get; init; }
    
    /// <summary>
    /// Дата окончания
    /// </summary>
    public string EndDate { get; init; }
}