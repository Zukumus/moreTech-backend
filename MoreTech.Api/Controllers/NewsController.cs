using Microsoft.AspNetCore.Mvc;
using MoreTech.Configuration;
using MoreTech.Data.Repository.Contracts;
using MoreTech.Service.Contracts.Abstract;
using NewsRepositoryModel = NewsCatcher.Repositories.Contracts.Models.NewsRepositoryModel;

namespace MoreTech.Api.Controllers;

[ApiController]
[Route("user-news")]
public class NewsController : ControllerBase
{
    private readonly IGetNewsRepository getNewsRepository;
    private readonly IExportNewsFromSourceToDataBase exportNewsFromSourceToDataBase;
    private readonly ISaveFileService saveFileService;
    private readonly IFileNameConfiguration fileNameConfiguration;

    public NewsController(IGetNewsRepository getNewsRepository,
        IExportNewsFromSourceToDataBase exportNewsFromSourceToDataBase, ISaveFileService saveFileService, IFileNameConfiguration fileNameConfiguration)
    {
        this.getNewsRepository = getNewsRepository;
        this.exportNewsFromSourceToDataBase = exportNewsFromSourceToDataBase;
        this.saveFileService = saveFileService;
        this.fileNameConfiguration = fileNameConfiguration;
    }

    /// <summary>
    /// Добавить файл с ключевыми словами для поиска в иссточнике новостсей в фоновом режиме. Данные будут сохранены в БД
    /// </summary>
    [HttpPut]
    [Route("file-with-key-words")]
    public async Task<ActionResult> UploadFileWithKeyWords(IFormFile file)
    {
        await saveFileService.SaveFile(file.OpenReadStream(), fileNameConfiguration.FileWithKeyWordsName);
        return NoContent();
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
    public async Task<ActionResult<IReadOnlyCollection<NewsRepositoryModel>>> GetNewsFroRole(string role,
        string keyWord)
    {
        var news = await getNewsRepository.GetTopNewsByKeyWord(role, keyWord, CancellationToken.None);
        return Ok(news);
    }
}