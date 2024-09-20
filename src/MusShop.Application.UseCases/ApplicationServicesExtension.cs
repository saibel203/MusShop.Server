using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MusShop.Application.UseCases.Commons.Behaviors;

namespace MusShop.Application.UseCases;

public static class ApplicationServicesExtension
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Add Mediator pattern
        services.AddMediatR(config =>
            config.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));

        // Add Mediator behaviors
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingPipelineBehavior<,>));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
        
        // Add FluentValidators
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
}