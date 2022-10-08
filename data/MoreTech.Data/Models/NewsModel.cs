using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MoreTech.Data.Models;

/// <summary>
/// Модель новости
/// </summary>
public class NewsModel
{
    /// <summary>
    /// ИД
    /// </summary>
    public Guid Id { get; set; }

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

internal class NewsModelConfiguration : IEntityTypeConfiguration<NewsModel>
{
    public void Configure(EntityTypeBuilder<NewsModel> entity)
    {
        entity.HasKey(x => x.Id);
        entity.Property(x => x.Summary).IsRequired(false);
    }
}