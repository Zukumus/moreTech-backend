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
}