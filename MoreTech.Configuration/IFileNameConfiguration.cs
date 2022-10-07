namespace MoreTech.Configuration;

/// <summary>
/// Настройка названия файла
/// </summary>
public interface IFileNameConfiguration
{
    /// <summary>
    /// Название файла с новостями
    /// </summary>
    public string FileWithNewsName { get; }
    
    /// <summary>
    /// Название файла с ключевыми словами
    /// </summary>
    public string FileWithKeyWordsName { get; }
}