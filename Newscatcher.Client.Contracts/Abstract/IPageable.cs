using System.Text.Json.Serialization;

namespace Newscatcher.Client.Contracts.Abstract;

/// <summary>
/// Пагинация
/// </summary>
public interface IPageable
{
    /// <summary>
    /// Всего страниц
    /// </summary>
    public int TotalPages { get; set; }
}