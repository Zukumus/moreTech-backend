using System.Globalization;
using CsvHelper;
using MoreTech.Configuration;
using MoreTech.Data.Repository.Contracts;
using MoreTech.DomainModels;
using NewsCatcher.Repositories.Contracts.Abstract;

namespace MoreTech.Api.BackgroundServices;

/// <summary>
/// Сервис для экспорта новостей из источника в цикле с учетом лимита запросов к источнику
/// </summary>
public class ExportNewsFromSourceByDelayService : BackgroundService
{
    private readonly IServiceScopeFactory serviceScopeFactory;
    private readonly IFileNameConfiguration fileNameConfiguration;
    private readonly IGetNewsByKeyRepository getNewsByKeyRepository;
    private readonly ILogger<ExportNewsFromSourceByDelayService> logger;

    public ExportNewsFromSourceByDelayService(IServiceScopeFactory serviceScopeFactory,
        IFileNameConfiguration fileNameConfiguration,
        IGetNewsByKeyRepository getNewsByKeyRepository,
        ILogger<ExportNewsFromSourceByDelayService> logger)
    {
        this.serviceScopeFactory = serviceScopeFactory;
        this.fileNameConfiguration = fileNameConfiguration;
        this.getNewsByKeyRepository = getNewsByKeyRepository;
        this.logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
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
                    break;
                }
                var keyWord = queue.Peek();
                logger.LogInformation("Parsing key word start");
                var startDate = DateTime.Parse(keyWord.StartDate);
                var endDate = DateTime.Parse(keyWord.EndDate);
                var news = await getNewsByKeyRepository.GetNewsByKeyWordAndDateRange(keyWord.KeyWord, startDate, endDate, stoppingToken);
                using var scope = serviceScopeFactory.CreateScope();
                var createNewsRepository = scope.ServiceProvider.GetRequiredService<ICreateNewsRepository>();
                if (news.Any())
                {
                    queue.Dequeue();
                    var newsToCreate = news.Select(i => new MoreTech.Data.Repository.Contracts.NewsRepositoryModel
                    {
                        Role = keyWord.UserRole,
                        KeyWord = keyWord.KeyWord,
                        PublishDate = i.PublishDate,
                        SourceUrl = i.SourceUrl,
                        Summary = i.Summary,
                        Title = i.Title,
                    });
                    await createNewsRepository.CreateNews(newsToCreate, stoppingToken);
                    logger.LogInformation("Parsing key word finished");
                }
                await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
            }
        }
        catch (Exception e)
        {
            logger.LogInformation("Error: {exception}", e);
        }
    }
}