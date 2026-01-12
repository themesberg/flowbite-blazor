using Microsoft.Extensions.DependencyInjection;
using TailwindMerge.Extensions;

namespace Flowbite.Services;

/// <summary>
/// Extension methods for setting up Flowbite services in an IServiceCollection.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds Flowbite services to the specified IServiceCollection.
    /// </summary>
    public static IServiceCollection AddFlowbite(this IServiceCollection services)
    {
        if (services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        services.AddTailwindMerge();
        services.AddSingleton<FlowbiteVersionService>();
        services.AddFlowbiteOffCanvasServices();

        return services;
    }
    
    /// <summary>
    /// Adds Flowbite off-canvas component services to the specified IServiceCollection.
    /// </summary>
    public static IServiceCollection AddFlowbiteOffCanvasServices(this IServiceCollection services)
    {
        services.AddScoped<IModalService, ModalService>();
        services.AddScoped<IDrawerService, DrawerService>();
        services.AddScoped<IToastService, ToastService>();
        
        return services;
    }
    
    /// <summary>
    /// Adds Flowbite modal service to the specified IServiceCollection.
    /// </summary>
    public static IServiceCollection AddFlowbiteModalService(this IServiceCollection services)
    {
        services.AddScoped<IModalService, ModalService>();
        return services;
    }
    
    /// <summary>
    /// Adds Flowbite drawer service to the specified IServiceCollection.
    /// </summary>
    public static IServiceCollection AddFlowbiteDrawerService(this IServiceCollection services)
    {
        services.AddScoped<IDrawerService, DrawerService>();
        return services;
    }
    
    /// <summary>
    /// Adds Flowbite toast service to the specified IServiceCollection.
    /// </summary>
    public static IServiceCollection AddFlowbiteToastService(this IServiceCollection services)
    {
        services.AddScoped<IToastService, ToastService>();
        return services;
    }
}
