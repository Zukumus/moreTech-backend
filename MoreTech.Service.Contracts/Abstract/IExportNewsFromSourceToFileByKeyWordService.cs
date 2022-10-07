namespace MoreTech.Service.Contracts.Abstract;

/// <summary>
/// Сервис для экспорта новостей в файл из источника новостей по ключевому слову
/// </summary>
public interface IExportNewsFromSourceToFileByKeyWordService
{
    /// <summary>
    /// Экспорт новостей в файл из источника по ключевому слову
    /// </summary>
    Task ExportToFileByKeyWord(string key, CancellationToken token);
}