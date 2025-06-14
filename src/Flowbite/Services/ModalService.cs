using Flowbite.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Flowbite.Services;

/// <summary>
/// Service for displaying modal dialogs.
/// </summary>
public class ModalService : IModalService
{
    private readonly IJSRuntime _jsRuntime;
    private readonly List<ModalInstance> _modals = new();
    private TaskCompletionSource<object?>? _activeModalTcs;
    
    /// <summary>
    /// Event raised when a modal instance is added.
    /// </summary>
    public event Action<ModalInstance>? OnModalInstanceAdded;
    
    /// <summary>
    /// Event raised when a modal close is requested.
    /// </summary>
    public event Action<string>? OnModalCloseRequested;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="ModalService"/> class.
    /// </summary>
    /// <param name="jsRuntime">The JavaScript runtime.</param>
    public ModalService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }
    
    /// <summary>
    /// Shows a modal dialog with the specified content.
    /// </summary>
    /// <typeparam name="TResult">The type of result returned by the modal.</typeparam>
    /// <param name="title">The title of the modal.</param>
    /// <param name="content">The content of the modal.</param>
    /// <param name="options">Options for configuring the modal.</param>
    /// <returns>A task that resolves with the modal result.</returns>
    public async Task<ModalResult<TResult>> ShowAsync<TResult>(string title, RenderFragment content, ModalOptions? options = null)
    {
        options ??= new ModalOptions();
        options.Title = title;
        
        var modalInstance = new ModalInstance
        {
            Title = title,
            Content = content,
            Options = options,
            TaskCompletionSource = new TaskCompletionSource<object?>()
        };
        
        _activeModalTcs = modalInstance.TaskCompletionSource;
        
        _modals.Add(modalInstance);
        OnModalInstanceAdded?.Invoke(modalInstance);
        
        var result = await modalInstance.TaskCompletionSource.Task;
        
        if (result is TResult typedResult)
        {
            return ModalResult<TResult>.Ok(typedResult);
        }
        
        return ModalResult<TResult>.Cancel();
    }
    
    /// <summary>
    /// Shows a modal dialog using the specified component type.
    /// </summary>
    /// <typeparam name="TComponent">The type of component to show.</typeparam>
    /// <typeparam name="TResult">The type of result returned by the modal.</typeparam>
    /// <param name="parameters">Parameters to pass to the component.</param>
    /// <param name="options">Options for configuring the modal.</param>
    /// <returns>A task that resolves with the modal result.</returns>
    public Task<ModalResult<TResult>> ShowAsync<TComponent, TResult>(ModalParameters? parameters = null, ModalOptions? options = null)
        where TComponent : ComponentBase
    {
        parameters ??= new ModalParameters();
        options ??= new ModalOptions();
        
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
        
        return ShowAsync<TResult>(options.Title ?? typeof(TComponent).Name, content, options);
    }
    
    /// <summary>
    /// Closes the currently open modal with the specified result.
    /// </summary>
    /// <param name="result">The result to return.</param>
    public void Close(object? result = null)
    {
        if (_modals.Count > 0)
        {
            var modalId = _modals[^1].Id;
            OnModalCloseRequested?.Invoke(modalId);
            _activeModalTcs?.TrySetResult(result);
        }
    }
}
