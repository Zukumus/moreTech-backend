using MoreTech.Configuration;
using Newscatcher.Client.Contracts.Abstract;
using Newscatcher.Client.Contracts.Models;

namespace Newscatcher.Client;

public class NewsCatcherClient : BaseClient, INewCatcherClient
{
    private const int DefaultPageSize = 100;

    public NewsCatcherClient(INewsCatcherClientConfiguration configuration, HttpClient client) : base(configuration.NewsCatcherBaseAddress, client, configuration.ApiKey)
    {
    }

    public async Task<IReadOnlyCollection<NewsModel>> GetNewsByKeyWord(string keyWord, CancellationToken token) =>
        await GetByPagination<GetNewsByKeyResponseModel>(
                $"v1/search_free?q={keyWord}", DefaultPageSize, token)
            .SelectMany(i => i.News.ToAsyncEnumerable())
            .ToListAsync(token);
}