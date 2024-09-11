namespace MusShop.Api;

public static class BaseServicesExtension
{
    public static IServiceCollection AddBaseServices(this IServiceCollection services)
    {
        services.AddControllers()
            .AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly);
        
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }
}