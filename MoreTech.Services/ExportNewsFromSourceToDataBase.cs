using MoreTech.Data.Repository.Contracts;
using MoreTech.Service.Contracts.Abstract;
using NewsCatcher.Repositories.Contracts.Abstract;

namespace MoreTech.Services;

public class ExportNewsFromSourceToDataBase : IExportNewsFromSourceToDataBase
{
    private readonly ICreateNewsRepository createNewsRepository;
    private readonly IGetNewsByKeyRepository getNewsByKeyRepository;

    public ExportNewsFromSourceToDataBase(ICreateNewsRepository createNewsRepository,
        IGetNewsByKeyRepository getNewsByKeyRepository)
    {
        this.createNewsRepository = createNewsRepository;
        this.getNewsByKeyRepository = getNewsByKeyRepository;
    }
    
    public async Task Export(string role, string keyWord, CancellationToken token)
    {
        var news = await getNewsByKeyRepository.GetNewsByKeyWord(keyWord, token);
        var newsToExport = news.Select(i => new NewsRepositoryModel
        {
            KeyWord = keyWord,
            Role = role,
            PublishDate = i.PublishDate,
            SourceUrl = i.SourceUrl,
            Summary = i.Summary,
            Title = i.Title,
        });
        await createNewsRepository.CreateNews(newsToExport, token);
    }
}