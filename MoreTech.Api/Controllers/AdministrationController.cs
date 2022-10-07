using Microsoft.AspNetCore.Mvc;
using MoreTech.Configuration;
using MoreTech.Service.Contracts.Abstract;

namespace MoreTech.Api.Controllers;

[ApiController]
[Route("administration")]
public class AdministrationController : ControllerBase
{
    private readonly IExportNewsFromSourceToFileByKeyWordService exportNewsFromSourceToFileByKeyWordService;
    private readonly IFileNameConfiguration fileNameConfiguration;
    private readonly ISaveFileService saveFileService;

    public AdministrationController(
        IExportNewsFromSourceToFileByKeyWordService exportNewsFromSourceToFileByKeyWordService,
        IFileNameConfiguration fileNameConfiguration,
        ISaveFileService saveFileService)
    {
        this.exportNewsFromSourceToFileByKeyWordService = exportNewsFromSourceToFileByKeyWordService;
        this.fileNameConfiguration = fileNameConfiguration;
        this.saveFileService = saveFileService;
    }

    /// <summary>
    /// Экспорт новостей в файл из источника новостей. Если файла был удален, будет создан новый
    /// </summary>
    [HttpPut]
    [Route("parser-to-file/news")]
    public async Task<ActionResult> ParseNewsToFile([FromQuery] string keyWord)
    {
        await exportNewsFromSourceToFileByKeyWordService.ExportToFileByKeyWord(keyWord, HttpContext.RequestAborted);
        return NoContent();
    }

    /// <summary>
    /// Скачать файл с новостями
    /// </summary>
    [HttpGet]
    [Route("file-with-news")]
    public async Task<FileContentResult> DownloadFileWithNews()
    {
        if (!System.IO.File.Exists(fileNameConfiguration.FileWithNewsName))
        {
            throw new Exception("Файл не найден");
        }

        var file = await System.IO.File.ReadAllBytesAsync(fileNameConfiguration.FileWithNewsName);
        return new FileContentResult(file, "text/csv")
        {
            FileDownloadName = fileNameConfiguration.FileWithNewsName
        };
    }

    /// <summary>
    /// Удалить файл с новостями
    /// </summary>
    [HttpDelete]
    [Route("file-with-news")]
    public async Task<ActionResult> DeleteFileWithNews()
    {
        if (!System.IO.File.Exists(fileNameConfiguration.FileWithNewsName))
        {
            return NotFound();
        }

        System.IO.File.Delete(fileNameConfiguration.FileWithNewsName);
        return NoContent();
    }

    /// <summary>
    /// Добавить файл с ключевми словами для поиска в иссточнике новостсей
    /// </summary>
    [HttpPut]
    [Route("file-with-key-words")]
    public async Task<ActionResult> UploadFileWithKeyWords(IFormFile file)
    {
        await saveFileService.SaveFile(file.OpenReadStream(), fileNameConfiguration.FileWithKeyWordsName);
        return NoContent();
    }
}