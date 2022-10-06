using Microsoft.Extensions.Configuration;
using MoreTech.Configuration;

namespace Newscatcher.Client.Configuration;

public class NewsCatcherClientConfiguration : INewsCatcherClientConfiguration
{
    private readonly IConfiguration configuration;

    public NewsCatcherClientConfiguration(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public Uri NewsCatcherBaseAddress => new(configuration["REST_ZIF_OM_NEWSCATCHER_URL"]);

    public string ApiKey => configuration["NEWSCATCHER_API_KEY"];
}