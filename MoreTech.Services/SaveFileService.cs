using MoreTech.Service.Contracts.Abstract;

namespace MoreTech.Services;

internal class SaveFileService : ISaveFileService
{
    public Task SaveFile(Stream stream, string filePath)
    {
        using var fileStream = new FileStream(filePath, FileMode.Create);
        stream.Seek(0, SeekOrigin.Begin);
        stream.CopyTo(fileStream);
        return Task.CompletedTask;
    }
}