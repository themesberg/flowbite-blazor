using Flowbite.Components;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace Flowbite.Services;

/// <summary>
/// Service for displaying modal dialogs.
/// </summary>
public interface IModalService
{
    /// <summary>
    /// Event raised when a modal instance is added.
    /// </summary>
    event Action<ModalInstance> OnModalInstanceAdded;
    
    /// <summary>
    /// Event raised when a modal close is requested.
    /// </summary>
    event Action<string> OnModalCloseRequested;
    
    /// <summary>
    /// Shows a modal dialog with the specified content.
    /// </summary>
    /// <typeparam name="TResult">The type of result returned by the modal.</typeparam>
    /// <param name="title">The title of the modal.</param>
    /// <param name="content">The content of the modal.</param>
    /// <param name="options">Options for configuring the modal.</param>
    /// <returns>A task that resolves with the modal result.</returns>
    Task<ModalResult<TResult>> ShowAsync<TResult>(string title, RenderFragment content, ModalOptions? options = null);
    
    /// <summary>
    /// Shows a modal dialog using the specified component type.
    /// </summary>
    /// <typeparam name="TComponent">The type of component to show.</typeparam>
    /// <typeparam name="TResult">The type of result returned by the modal.</typeparam>
    /// <param name="parameters">Parameters to pass to the component.</param>
    /// <param name="options">Options for configuring the modal.</param>
    /// <returns>A task that resolves with the modal result.</returns>
    Task<ModalResult<TResult>> ShowAsync<TComponent, TResult>(ModalParameters? parameters = null, ModalOptions? options = null) 
        where TComponent : ComponentBase;
    
    /// <summary>
    /// Closes the currently open modal with the specified result.
    /// </summary>
    /// <param name="result">The result to return.</param>
    void Close(object? result = null);
}

/// <summary>
/// Represents a modal instance.
/// </summary>
public class ModalInstance
{
    /// <summary>
    /// Gets the unique identifier for the modal.
    /// </summary>
    public string Id { get; } = Guid.NewGuid().ToString();
    
    /// <summary>
    /// Gets the title of the modal.
    /// </summary>
    public string? Title { get; init; }
    
    /// <summary>
    /// Gets the content of the modal.
    /// </summary>
    public RenderFragment Content { get; init; } = _ => { };
    
    /// <summary>
    /// Gets the options for the modal.
    /// </summary>
    public ModalOptions Options { get; init; } = new();
    
    /// <summary>
    /// Gets or sets the task completion source for the modal result.
    /// </summary>
    public TaskCompletionSource<object?> TaskCompletionSource { get; init; } = new();
}
