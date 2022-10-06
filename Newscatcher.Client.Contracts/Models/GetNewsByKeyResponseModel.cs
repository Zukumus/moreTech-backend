using System.Text.Json.Serialization;
using Newscatcher.Client.Contracts.Abstract;

namespace Newscatcher.Client.Contracts.Models;

/// <summary>
/// Модель ответа на запрос получения списка новостей по ключевому слову
/// </summary>
public class GetNewsByKeyResponseModel : IPageable
{
    /// <summary>
    /// Новости
    /// </summary>
    [JsonPropertyName("articles")]
    public IReadOnlyCollection<NewsModel> News { get; set; }

    /// <summary>
    /// <inheritdoc cref="IPageable.TotalPages"/>
    /// </summary>
    [JsonPropertyName("total_pages")]
    public int TotalPages { get; set; }
}