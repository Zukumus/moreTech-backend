using Microsoft.AspNetCore.Mvc;
using MoreTech.Data.Repository.Contracts;
using MoreTech.Service.Contracts.Abstract;
using NewsRepositoryModel = NewsCatcher.Repositories.Contracts.Models.NewsRepositoryModel;

namespace MoreTech.Api.Controllers;

[ApiController]
[Route("administration")]
public class NewsController : ControllerBase
{
    private readonly IGetNewsRepository getNewsRepository;
    private readonly IExportNewsFromSourceToDataBase exportNewsFromSourceToDataBase;

    public NewsController(IGetNewsRepository getNewsRepository, IExportNewsFromSourceToDataBase exportNewsFromSourceToDataBase)
    {
        this.getNewsRepository = getNewsRepository;
        this.exportNewsFromSourceToDataBase = exportNewsFromSourceToDataBase;
    }
    
    /// <summary>
    /// Поиск новостей в источнике данных и сохранение в локальное хранилище
    /// </summary>
    [HttpPut]
    [Route("news")]
    public async Task<ActionResult> SearchNews([FromQuery] string keyWord, [FromQuery] string role)
    {
        await exportNewsFromSourceToDataBase.Export(role, keyWord, HttpContext.RequestAborted);
        return NoContent();
    }
    
    /// <summary>
    /// Получить новость по ключевому слову
    /// </summary>
    [HttpGet]
    [Route("news")]
    public async Task<ActionResult<IReadOnlyCollection<NewsRepositoryModel>>> GetNewsFroRole(string role, string keyWord)
    {
        var news = await getNewsRepository.GetTopNewsByKeyWord(role, keyWord, CancellationToken.None);
        return Ok(news);
    }
}