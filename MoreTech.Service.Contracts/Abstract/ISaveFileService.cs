namespace MoreTech.Service.Contracts.Abstract;

/// <summary>
/// Сервис для сохранения файла
/// </summary>
public interface ISaveFileService
{
    /// <summary>
    /// Сохранить файл
    /// </summary>
    Task SaveFile(Stream stream, string filePath);
}