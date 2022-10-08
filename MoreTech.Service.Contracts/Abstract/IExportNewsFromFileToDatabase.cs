namespace MoreTech.Service.Contracts.Abstract;

/// <summary>
/// Экспорт новостей из файла в бд
/// </summary>
public interface IExportNewsFromFileToDatabase
{
    Task ExportToDb(string role, Stream fileStream, CancellationToken token);
}