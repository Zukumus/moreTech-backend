using Microsoft.EntityFrameworkCore;
using MoreTech.Data.Repository.Contracts;

namespace MoreTech.Data.Repository.Services;

public class GetNewsRepository : IGetNewsRepository
{
    private readonly DataContext context;

    public GetNewsRepository(DataContext context)
    {
        this.context = context;
    }

    public async Task<IReadOnlyCollection<NewsRepositoryModel>> GetTopNewsByKeyWord(string userRole, string keyWord,
        CancellationToken token)
    {
        var key = !string.IsNullOrEmpty(keyWord) ? keyWord.ToLowerInvariant() : string.Empty;
        var result = context.NewsFromSource.AsNoTracking()
            .Where(i => (i.Role.Contains(userRole) && string.IsNullOrEmpty(key)) || (!string.IsNullOrEmpty(key) && i.Title.Contains(key))).AsNoTracking().ToList()
            .OrderByDescending(i => i.PublishDate).DistinctBy(i => i.PublishDate).Take(5).ToList();
        
        return await Task.FromResult(result.Select(i => new NewsRepositoryModel
        {
            Role = i.Role,
            Title = i.Title,
            KeyWord = i.KeyWord,
            PublishDate = i.PublishDate,
            SourceUrl = i.SourceUrl,
            Summary = i.Summary,
        }).ToList().AsReadOnly());
    }
}