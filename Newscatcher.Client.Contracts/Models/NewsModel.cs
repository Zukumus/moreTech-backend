using System.Text.Json.Serialization;

namespace Newscatcher.Client.Contracts.Models;

/// <summary>
/// Модель новости
/// </summary>
public class NewsModel
{
    /// <summary>
    /// Заголовок
    /// </summary>
    public string Title { get; set; }
    
    /// <summary>
    /// Резюме
    /// </summary>
    public string Summary { get; set; }

    /// <summary>
    /// Дата публикации в источнике
    /// </summary>
    [JsonPropertyName("published_date")]
    public string PublishDate { get; set; }
    
    /// <summary>
    /// Дата публикации в источнике
    /// </summary>
    [JsonPropertyName("link")]
    public string SourceUrl { get; set; }
}