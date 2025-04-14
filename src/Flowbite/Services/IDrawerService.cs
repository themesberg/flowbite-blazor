using Flowbite.Components;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace Flowbite.Services;

/// <summary>
/// Service for displaying drawer components.
/// </summary>
public interface IDrawerService
{
    /// <summary>
    /// Event raised when a drawer instance is added.
    /// </summary>
    event Action<DrawerInstance> OnDrawerInstanceAdded;
    
    /// <summary>
    /// Event raised when a drawer close is requested.
    /// </summary>
    event Action<string> OnDrawerCloseRequested;
    
    /// <summary>
    /// Shows a drawer with the specified content.
    /// </summary>
    /// <param name="content">The content of the drawer.</param>
    /// <param name="position">The position of the drawer.</param>
    /// <param name="options">Options for configuring the drawer.</param>
    /// <returns>A task that completes when the drawer is closed.</returns>
    Task<DrawerResult> ShowAsync(RenderFragment content, DrawerPosition position = DrawerPosition.Left, DrawerOptions? options = null);
    
    /// <summary>
    /// Shows a drawer using the specified component type.
    /// </summary>
    /// <typeparam name="TComponent">The type of component to show.</typeparam>
    /// <param name="parameters">Parameters to pass to the component.</param>
    /// <param name="position">The position of the drawer.</param>
    /// <param name="options">Options for configuring the drawer.</param>
    /// <returns>A task that completes when the drawer is closed.</returns>
    Task<DrawerResult> ShowAsync<TComponent>(DrawerParameters? parameters = null, DrawerPosition position = DrawerPosition.Left, DrawerOptions? options = null) 
        where TComponent : ComponentBase;
    
    /// <summary>
    /// Closes the currently open drawer with the specified result.
    /// </summary>
    /// <param name="result">Optional result data.</param>
    void Close(object? result = null);
}

/// <summary>
/// Represents a drawer instance.
/// </summary>
public class DrawerInstance
{
    /// <summary>
    /// Gets the unique identifier for the drawer.
    /// </summary>
    public string Id { get; } = Guid.NewGuid().ToString();
    
    /// <summary>
    /// Gets the content of the drawer.
    /// </summary>
    public RenderFragment Content { get; init; } = _ => { };
    
    /// <summary>
    /// Gets the position of the drawer.
    /// </summary>
    public DrawerPosition Position { get; init; } = DrawerPosition.Left;
    
    /// <summary>
    /// Gets the options for the drawer.
    /// </summary>
    public DrawerOptions Options { get; init; } = new();
    
    /// <summary>
    /// Gets or sets the task completion source for the drawer result.
    /// </summary>
    public TaskCompletionSource<object?> TaskCompletionSource { get; init; } = new();
}
