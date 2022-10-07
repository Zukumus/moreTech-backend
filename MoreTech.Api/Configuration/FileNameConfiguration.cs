using MoreTech.Configuration;

namespace MoreTech.Api.Configuration;

public class FileNameConfiguration : IFileNameConfiguration
{
    private readonly IConfiguration configuration;

    public FileNameConfiguration(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public string FileWithNewsName => configuration["FILE_WITH_NEWS_NAME"];
    public string FileWithKeyWordsName => configuration["FILE_WITH_KEY_WORDS_NAME"];
}