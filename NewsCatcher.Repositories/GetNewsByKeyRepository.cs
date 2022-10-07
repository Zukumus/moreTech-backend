using AutoMapper;
using Newscatcher.Client.Contracts.Abstract;
using NewsCatcher.Repositories.Contracts.Abstract;
using NewsCatcher.Repositories.Contracts.Models;

namespace NewsCatcher.Repositories;

internal class GetNewsByKeyRepository : IGetNewsByKeyRepository
{
    private readonly INewCatcherClient newsCatcherClient;
    private readonly IMapper mapper;

    public GetNewsByKeyRepository(INewCatcherClient newsCatcherClient, IMapper mapper)
    {
        this.newsCatcherClient = newsCatcherClient;
        this.mapper = mapper;
    }
    
    public async Task<IReadOnlyCollection<NewsRepositoryModel>> GetNewsByKeyWord(string key, CancellationToken token)
    {
        var news = await newsCatcherClient.GetNewsByKeyWord(key, token);
        return mapper.Map<IReadOnlyCollection<NewsRepositoryModel>>(news);
    }

    public async Task<IReadOnlyCollection<NewsRepositoryModel>> GetNewsByKeyWordAndDateRange(string key, DateTime starDate, DateTime endDate, CancellationToken token)
    {
        var news = await newsCatcherClient.GetNewsByKeyWordAndDateRange(key, starDate, endDate, token);
        return mapper.Map<IReadOnlyCollection<NewsRepositoryModel>>(news);
    }
}