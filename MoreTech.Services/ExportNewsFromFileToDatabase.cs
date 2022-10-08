using System.Globalization;
using CsvHelper;
using MoreTech.Data.Repository.Contracts;
using MoreTech.DomainModels;
using MoreTech.Service.Contracts.Abstract;

namespace MoreTech.Services;

public class ExportNewsFromFileToDatabase : IExportNewsFromFileToDatabase
{
    private readonly ICreateNewsRepository createNewsRepository;

    public ExportNewsFromFileToDatabase(ICreateNewsRepository createNewsRepository)
    {
        this.createNewsRepository = createNewsRepository;
    }
    
    public async Task ExportToDb(Stream fileStream, CancellationToken token)
    {
        using var reader = new StreamReader(fileStream);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        var newsFromSource = csv.GetRecords<NewsFromSourceFullModel>().ToList();
        var newsRepoModels = newsFromSource.Select(i => new NewsRepositoryModel
        {
            KeyWord = i.KeyWord,
            PublishDate = i.PublishDate,
            Summary = i.Summary,
            SourceUrl = i.SourceUrl,
            Title = i.Title,
        });
        await createNewsRepository.CreateNews(newsRepoModels, token);
    }
}