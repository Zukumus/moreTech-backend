namespace MoreTech.Service.Contracts.Abstract;

/// <summary>
/// Сервис для экспорта новостей в файл из источника новостей по ключевому слову
/// </summary>
public interface IExportNewsFromSourceToFile
{
    /// <summary>
    /// Экспорт новостей в файл из источника по ключевому слову
    /// </summary>
    Task ExportToFile(string key, CancellationToken token);
}