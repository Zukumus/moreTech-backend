using Microsoft.Extensions.DependencyInjection;

namespace MoreTech.MapperProfiles;

public static class RegistrationExtension
{
    public static IServiceCollection AddMapperProfiles(this IServiceCollection services)
    {
        return services.AddAutoMapper(expression =>
        {
            expression.AllowNullCollections = true;
        }, typeof(RegistrationExtension).Assembly);
    }
}