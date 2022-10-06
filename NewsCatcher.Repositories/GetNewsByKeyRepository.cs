using AutoMapper;
using Newscatcher.Client.Contracts.Abstract;
using NewsCatcher.Repositories.Contracts.Abstract;
using NewsCatcher.Repositories.Contracts.Models;

namespace NewsCatcher.Repositories;

public class GetNewsByKeyRepository : IGetNewsByKeyRepository
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
}