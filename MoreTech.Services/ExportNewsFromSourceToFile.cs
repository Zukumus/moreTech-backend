using System.Collections;
using System.Globalization;
using System.IO.Compression;
using CsvHelper;
using CsvHelper.Configuration;
using MoreTech.Configuration;
using MoreTech.Service.Contracts.Abstract;
using NewsCatcher.Repositories.Contracts.Abstract;
using NewsCatcher.Repositories.Contracts.Models;

namespace MoreTech.Services;

public class ExportNewsFromSourceToFile : IExportNewsFromSourceToFile
{
    private readonly IGetNewsByKeyRepository getNewsByKeyRepository;
    private readonly IFileNameConfiguration fileNameConfiguration;

    public ExportNewsFromSourceToFile(IGetNewsByKeyRepository getNewsByKeyRepository, IFileNameConfiguration fileNameConfiguration)
    {
        this.getNewsByKeyRepository = getNewsByKeyRepository;
        this.fileNameConfiguration = fileNameConfiguration;
    }
    public async Task ExportToFile(string key, CancellationToken token)
    {
        var news = await getNewsByKeyRepository.GetNewsByKeyWord(key, token);
        await WriteToFile(news, token);
    }

    private async Task WriteToFile(IEnumerable<NewsRepositoryModel> news, CancellationToken token)
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,
        };
        await using var stream = File.Open(fileNameConfiguration.FileWithNewsName, FileMode.Append);
        await using var writer = new StreamWriter(stream);
        await using var csv = new CsvWriter(writer, config);
        await csv.WriteRecordsAsync((IEnumerable)news, token);
    }
}