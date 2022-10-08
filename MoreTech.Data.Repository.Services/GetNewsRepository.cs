using MoreTech.Data.Repository.Contracts;

namespace MoreTech.Data.Repository.Services;

public class GetNewsRepository : IGetNewsRepository
{
    private readonly DataContext context;

    public GetNewsRepository(DataContext context)
    {
        this.context = context;
    }
    public async Task<IReadOnlyCollection<NewsRepositoryModel>> GetTopNewsByKeyWord(string keyWord, CancellationToken token)
    {
        var key = keyWord.ToLowerInvariant();
        var result = context.NewsFromSource.Where(i => i.Title.Contains(key)).Take(3);
        return await Task.FromResult(result.Select(i => new NewsRepositoryModel
        {
            Title = i.Title,
            KeyWord = i.KeyWord,
            PublishDate = i.PublishDate,
            SourceUrl = i.SourceUrl,
            Summary = i.Summary,
        }).ToList().AsReadOnly());
    }
}