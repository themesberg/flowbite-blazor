using Flowbite.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Flowbite.Services;

/// <summary>
/// Service for displaying drawer components.
/// </summary>
public class DrawerService : IDrawerService
{
    private readonly IJSRuntime _jsRuntime;
    private readonly List<DrawerInstance> _drawers = new();
    private TaskCompletionSource<object?>? _activeDrawerTcs;
    
    /// <summary>
    /// Event raised when a drawer instance is added.
    /// </summary>
    public event Action<DrawerInstance>? OnDrawerInstanceAdded;
    
    /// <summary>
    /// Event raised when a drawer close is requested.
    /// </summary>
    public event Action<string>? OnDrawerCloseRequested;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="DrawerService"/> class.
    /// </summary>
    /// <param name="jsRuntime">The JavaScript runtime.</param>
    public DrawerService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }
    
    /// <summary>
    /// Shows a drawer with the specified content.
    /// </summary>
    /// <param name="content">The content of the drawer.</param>
    /// <param name="position">The position of the drawer.</param>
    /// <param name="options">Options for configuring the drawer.</param>
    /// <returns>A task that completes when the drawer is closed.</returns>
    public async Task<DrawerResult> ShowAsync(RenderFragment content, DrawerPosition position = DrawerPosition.Left, DrawerOptions? options = null)
    {
        options ??= new DrawerOptions();
        
        var drawerInstance = new DrawerInstance
        {
            Content = content,
            Position = position,
            Options = options,
            TaskCompletionSource = new TaskCompletionSource<object?>()
        };
        
        _activeDrawerTcs = drawerInstance.TaskCompletionSource;
        
        _drawers.Add(drawerInstance);
        OnDrawerInstanceAdded?.Invoke(drawerInstance);
        
        var result = await drawerInstance.TaskCompletionSource.Task;
        
        return result != null 
            ? DrawerResult.Ok(result) 
            : DrawerResult.Cancel();
    }
    
    /// <summary>
    /// Shows a drawer using the specified component type.
    /// </summary>
    /// <typeparam name="TComponent">The type of component to show.</typeparam>
    /// <param name="parameters">Parameters to pass to the component.</param>
    /// <param name="position">The position of the drawer.</param>
    /// <param name="options">Options for configuring the drawer.</param>
    /// <returns>A task that completes when the drawer is closed.</returns>
    public Task<DrawerResult> ShowAsync<TComponent>(DrawerParameters? parameters = null, DrawerPosition position = DrawerPosition.Left, DrawerOptions? options = null) 
        where TComponent : ComponentBase
    {
        parameters ??= new DrawerParameters();
        options ??= new DrawerOptions();
        
        var content = new RenderFragment(builder =>
        {
            var i = 0;
            builder.OpenComponent<TComponent>(i++);
            
            foreach (var parameter in parameters.GetAll())
            {
                builder.AddAttribute(i++, parameter.Key, parameter.Value);
            }
            
            builder.CloseComponent();
        });
        
        return ShowAsync(content, position, options);
    }
    
    /// <summary>
    /// Closes the currently open drawer with the specified result.
    /// </summary>
    /// <param name="result">Optional result data.</param>
    public void Close(object? result = null)
    {
        if (_drawers.Count > 0)
        {
            var drawerId = _drawers[^1].Id;
            OnDrawerCloseRequested?.Invoke(drawerId);
            _activeDrawerTcs?.TrySetResult(result);
        }
    }
}
