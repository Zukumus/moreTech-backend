using Microsoft.AspNetCore.Mvc;
using MoreTech.Data.Repository.Contracts;
using NewsRepositoryModel = NewsCatcher.Repositories.Contracts.Models.NewsRepositoryModel;

namespace MoreTech.Api.Controllers;

[ApiController]
[Route("administration")]
public class NewsController : ControllerBase
{
    private readonly IGetNewsRepository getNewsRepository;

    public NewsController(IGetNewsRepository getNewsRepository)
    {
        this.getNewsRepository = getNewsRepository;
    }
    
    /// <summary>
    /// Получить новость по ключевому слову
    /// </summary>
    [HttpGet]
    [Route("news-by-key")]
    public async Task<ActionResult<IReadOnlyCollection<NewsRepositoryModel>>> GetNewsFroRole(string keyWord)
    {
        var news = await getNewsRepository.GetTopNewsByKeyWord(keyWord, CancellationToken.None);
        return Ok(news);
    }
}