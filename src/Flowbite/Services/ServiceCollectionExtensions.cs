using Microsoft.Extensions.DependencyInjection;

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
        
        services.AddSingleton<FlowbiteVersionService>();
        
        return services;
    }
}
