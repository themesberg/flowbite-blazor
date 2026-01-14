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
        services.AddFlowbiteFloatingService();
        services.AddFlowbiteLazyServices();

        return services;
    }

    /// <summary>
    /// Adds Flowbite lazy-loaded JavaScript module services to the specified IServiceCollection.
    /// </summary>
    /// <remarks>
    /// These services use Lazy&lt;Task&lt;IJSObjectReference&gt;&gt; to load JavaScript modules
    /// on-demand, reducing initial page load time. Modules are only fetched when first used.
    /// </remarks>
    public static IServiceCollection AddFlowbiteLazyServices(this IServiceCollection services)
    {
        services.AddScoped<IClipboardService, ClipboardService>();
        services.AddScoped<IElementService, ElementService>();
        services.AddScoped<IFocusManagementService, FocusManagementService>();
        return services;
    }

    /// <summary>
    /// Adds the Flowbite clipboard service to the specified IServiceCollection.
    /// </summary>
    /// <remarks>
    /// The clipboard module is lazy-loaded on first use.
    /// </remarks>
    public static IServiceCollection AddFlowbiteClipboardService(this IServiceCollection services)
    {
        services.AddScoped<IClipboardService, ClipboardService>();
        return services;
    }

    /// <summary>
    /// Adds the Flowbite element service to the specified IServiceCollection.
    /// </summary>
    /// <remarks>
    /// The element module is lazy-loaded on first use.
    /// </remarks>
    public static IServiceCollection AddFlowbiteElementService(this IServiceCollection services)
    {
        services.AddScoped<IElementService, ElementService>();
        return services;
    }

    /// <summary>
    /// Adds the Flowbite focus management service to the specified IServiceCollection.
    /// </summary>
    /// <remarks>
    /// The focus management module is lazy-loaded on first use.
    /// </remarks>
    public static IServiceCollection AddFlowbiteFocusManagementService(this IServiceCollection services)
    {
        services.AddScoped<IFocusManagementService, FocusManagementService>();
        return services;
    }

    /// <summary>
    /// Adds Flowbite floating positioning service to the specified IServiceCollection.
    /// </summary>
    /// <remarks>
    /// The FloatingService provides viewport-aware positioning for dropdowns, tooltips, and popovers
    /// using @floating-ui/dom. It automatically handles flip and shift behavior when elements would
    /// otherwise overflow the viewport.
    /// </remarks>
    public static IServiceCollection AddFlowbiteFloatingService(this IServiceCollection services)
    {
        services.AddScoped<IFloatingService, FloatingService>();
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
