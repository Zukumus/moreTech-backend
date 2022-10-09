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
        var roleLowerCase = userRole.ToLowerInvariant();
        var query = context.NewsFromSource.AsNoTracking().Where(i => (i.Role.Contains(roleLowerCase)));
        if (!string.IsNullOrEmpty(keyWord))
        {
            var keyLower = keyWord.ToLowerInvariant();
            query = query.Where(i => i.Title.Contains(keyLower) || i.KeyWord.Contains(keyLower)).AsNoTracking();
        }
        var result = query.ToList().OrderByDescending(i => i.PublishDate).DistinctBy(i => i.PublishDate).Take(5).ToList();
        
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