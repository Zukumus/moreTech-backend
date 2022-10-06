using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using MoreTech.Api.Configuration;
using MoreTech.Api.Helpers;
using MoreTech.Configuration;
using MoreTech.MapperProfiles;
using MoreTech.Services;
using Newscatcher.Client.DI;
using NewsCatcher.Repositories;

namespace MoreTech.Api;

public class Startup
{
    private readonly IWebHostEnvironment environment;
    private readonly IConfiguration configuration;

    /// <summary>
    /// Конструктор
    /// </summary>
    public Startup(IConfiguration configuration, IWebHostEnvironment environment)
    {
        this.environment = environment;
        this.configuration = configuration;
    }

    /// <summary>
    /// This method gets called by the runtime. Use this method to add services to the container.
    /// </summary>
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddNewsCatcherRepository();
        services.AddMapperProfiles();
        services.AddServices();
        services.AddNewsCatcherClients();
        
        services.AddScoped<IFileNameConfiguration, FileNameConfiguration>();
        
        services.AddMvc()
            .AddControllersAsServices()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase, allowIntegerValues: false));
            });
        
        services.AddCustomSwagger(configuration, false, null, Assembly.GetExecutingAssembly());
    }
    
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseCors(builder => { builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod(); });
        app.UseDeveloperExceptionPage();
        app.UseCustomSwagger(configuration, "MoreTech.Api");
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}