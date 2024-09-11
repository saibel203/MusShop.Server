using Mapster;
using Microsoft.Extensions.DependencyInjection;
using MusShop.Application.MapsterConfigurations;

namespace MusShop.Application;

public static class ApplicationMapsterServicesExtension
{
    public static IServiceCollection AddApplicationMapsterService(this IServiceCollection services)
    {
        // Add Mapster
        services.AddMapster();
        
        // Add Mapster configurations
        BaseMapsterConfig.Configure();
        
        return services;
    }
}