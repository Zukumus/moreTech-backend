using MoreTech.Configuration;
using Newscatcher.Client.Contracts.Abstract;
using Newscatcher.Client.Contracts.Models;

namespace Newscatcher.Client;

internal class NewsCatcherClient : BaseClient, INewCatcherClient
{
    private const int DefaultPageSize = 100;

    public NewsCatcherClient(INewsCatcherClientConfiguration configuration, HttpClient client) : base(configuration.NewsCatcherBaseAddress, client, configuration.ApiKey)
    {
    }

    public async Task<IReadOnlyCollection<NewsModel>> GetNewsByKeyWord(string keyWord, CancellationToken token) =>
        (await Get<GetNewsByKeyResponseModel>($"v1/search_free?q={keyWord}&lang=ru&page={1}&page_size={100}", token)).News;

    public async Task<IReadOnlyCollection<NewsModel>> GetNewsByKeyWordAndDateRange(string keyWord, DateTimeOffset startDate, DateTimeOffset endDate,
        CancellationToken token) => 
        await GetByPagination<GetNewsByKeyResponseModel>(
                $"v1/search_enterprise?q={keyWord}&from={startDate.UtcDateTime}&to={endDate.UtcDateTime}", DefaultPageSize, token)
            .SelectMany(i => i.News.ToAsyncEnumerable())
            .ToListAsync(token);
}