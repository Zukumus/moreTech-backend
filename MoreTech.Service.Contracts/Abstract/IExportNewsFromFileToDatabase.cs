namespace MoreTech.Service.Contracts.Abstract;

/// <summary>
/// Экспорт новостей из файла в бд
/// </summary>
public interface IExportNewsFromFileToDatabase
{
    Task ExportToDb(Stream fileStream, CancellationToken token);
}