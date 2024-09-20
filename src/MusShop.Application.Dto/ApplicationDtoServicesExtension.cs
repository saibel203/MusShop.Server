using Mapster;
using Microsoft.Extensions.DependencyInjection;
using MusShop.Application.MapsterConfigurations;

namespace MusShop.Application;

public static class ApplicationDtoServicesExtension
{
    public static IServiceCollection AddApplicationDtoService(this IServiceCollection services)
    {
        // Add Mapster
        services.AddMapster();

        // Add Mapster configurations
        BaseMapsterConfig.Configure();

        return services;
    }
}