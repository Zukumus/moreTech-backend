using Microsoft.Extensions.DependencyInjection;
using MoreTech.Configuration;
using Newscatcher.Client.Configuration;
using Newscatcher.Client.Contracts.Abstract;

namespace Newscatcher.Client.DI;

public static class RegistrationExtension
{
    public static IServiceCollection AddNewsCatcherClients(this IServiceCollection services)
    {
        services.AddTransient<INewsCatcherClientConfiguration, NewsCatcherClientConfiguration>();
        services.AddHttpClient<INewCatcherClient, NewsCatcherClient>();
        return services;
    }
}