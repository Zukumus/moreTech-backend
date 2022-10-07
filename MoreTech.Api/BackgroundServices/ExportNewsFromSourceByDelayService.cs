using System.Collections;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using MoreTech.Configuration;
using MoreTech.DomainModels;
using NewsCatcher.Repositories.Contracts.Abstract;
using NewsCatcher.Repositories.Contracts.Models;

namespace MoreTech.Api.BackgroundServices;

/// <summary>
/// Сервис для экспорта новостей из источника в цикле с учетом лимита запросов к источнику
/// </summary>
public class ExportNewsFromSourceByDelayService : BackgroundService
{
    private readonly IFileNameConfiguration fileNameConfiguration;
    private readonly IGetNewsByKeyRepository getNewsByKeyRepository;
    private readonly ILogger<ExportNewsFromSourceByDelayService> logger;

    public ExportNewsFromSourceByDelayService(IFileNameConfiguration fileNameConfiguration,
        IGetNewsByKeyRepository getNewsByKeyRepository,
        ILogger<ExportNewsFromSourceByDelayService> logger)
    {
        this.fileNameConfiguration = fileNameConfiguration;
        this.getNewsByKeyRepository = getNewsByKeyRepository;
        this.logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var fileFound = false;
        do
        {
            if (!File.Exists(fileNameConfiguration.FileWithKeyWordsName))
            {
                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
                logger.LogInformation("File with key word not found");
                continue;
            }
            fileFound = true;
            logger.LogInformation("File with key word found");
        } while (!fileFound);


        using var reader = new StreamReader(fileNameConfiguration.FileWithKeyWordsName);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        var keyWords = csv.GetRecords<KeyWordWithDateModel>().ToList();
        var queue = new Queue<KeyWordWithDateModel>(keyWords);
        logger.LogInformation("Start parsing");
        
        while (true)
        {
            if (!queue.Any())
            {
                logger.LogInformation("KeyWord queue is empty. All parsing is finished!");
            }
            var keyWord = queue.Peek();
            logger.LogInformation("Parsing key word {Key} start", keyWord.KeyWord);
            var startDate = DateTime.Parse(keyWord.StartDate);
            var endDate = DateTime.Parse(keyWord.EndDate);
            var news = await getNewsByKeyRepository.GetNewsByKeyWordAndDateRange(keyWord.KeyWord, startDate, endDate, stoppingToken);
            if (news.Any())
            {
                queue.Dequeue();
                await WriteToFileWithNews(news, keyWord, stoppingToken);
                logger.LogInformation("Parsing key word {Key} finished", keyWord.KeyWord);
            }
            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
        }
    }
    
    private async Task WriteToFileWithNews(IEnumerable<NewsRepositoryModel> news, KeyWordWithDateModel keyWord, CancellationToken token)
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,
        };
        await using var stream = File.Open(fileNameConfiguration.FileWithNewsName, FileMode.Append);
        await using var writer = new StreamWriter(stream);
        await using var csv = new CsvWriter(writer, config);
        var newsFullModel = news.Select(i => new NewsFromSourceFullModel
        {
            KeyWord = keyWord.KeyWord, Summary = i.Summary, Title = i.Title, PublishDate = i.PublishDate,
            SourceUrl = i.SourceUrl,
        });
        await csv.WriteRecordsAsync((IEnumerable)newsFullModel, token);
    }
}