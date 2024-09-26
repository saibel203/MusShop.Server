using System.Reflection;
using Microsoft.OpenApi.Models;

namespace MusShop.Api;

public static class BaseServicesExtension
{
    public static IServiceCollection AddBaseServices(this IServiceCollection services)
    {
        services.AddControllers()
            .AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly);

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "MusShop API",
                Description = "This API is intended for the MusShop music store"
            });

            string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            string presentationXmlFile = "MusShop.Presentation.xml";
            string applicationDtoXmlFile = "MusShop.Application.Dto.xml";

            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFile));
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, presentationXmlFile));
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, applicationDtoXmlFile));
        });

        return services;
    }
}