using MoreTech.Data.Models;
using MoreTech.Data.Repository.Contracts;

namespace MoreTech.Data.Repository.Services;

public class CreateNewsRepository : ICreateNewsRepository
{
    private readonly DataContext context;

    public CreateNewsRepository(DataContext context)
    {
        this.context = context;
    }
    public async Task CreateNews(IEnumerable<NewsRepositoryModel> NewsModels, CancellationToken token)
    {
        var news = NewsModels.Select(i => new NewsModel
        {
            Id = Guid.NewGuid(),
            KeyWord = i.KeyWord,
            PublishDate = i.PublishDate,
            SourceUrl = i.SourceUrl,
            Title = i.Title,
        });

        await context.NewsFromSource.AddRangeAsync(news, token);
        await context.SaveChangesAsync(token);
    }
}