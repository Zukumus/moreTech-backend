namespace MoreTech.Service.Contracts.Abstract;

/// <summary>
/// Сервис для экпорта новости из источника новостей в БД
/// </summary>
public interface IExportNewsFromSourceToDataBase
{
    /// <summary>
    /// Экспорт
    /// </summary>
    Task Export(string role, string keyWord, CancellationToken token);
}