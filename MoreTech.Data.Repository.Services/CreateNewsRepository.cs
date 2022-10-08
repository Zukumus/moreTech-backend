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
        var newsFormSource = NewsModels.Select(i => new NewsModel
        {
            Id = Guid.NewGuid(),
            Role = i.Role,
            KeyWord = i.KeyWord,
            PublishDate = i.PublishDate,
            SourceUrl = i.SourceUrl,
            Title = i.Title,
        }).ToList();

        var list = new List<List<NewsModel>>(); 

        for (int i = 0; i < newsFormSource.Count(); i += 1000) 
        { 
            list.Add(newsFormSource.GetRange(i, Math.Min(1000, newsFormSource.Count() - i))); 
        }

        foreach (var x in list)
        {
            await context.NewsFromSource.AddRangeAsync(x, token);
            await context.SaveChangesAsync(token);
        }
    }
}