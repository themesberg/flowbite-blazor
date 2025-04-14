using Flowbite.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;

namespace Flowbite.Services;

/// <summary>
/// Service for displaying toast notifications.
/// </summary>
public class ToastService : IToastService
{
    private readonly IJSRuntime _jsRuntime;
    private readonly Dictionary<string, ToastMessage> _activeToasts = new();
    
    /// <summary>
    /// Event raised when a toast is added.
    /// </summary>
    public event Action<ToastMessage>? OnToastAdded;
    
    /// <summary>
    /// Event raised when a toast is removed.
    /// </summary>
    public event Action<string>? OnToastRemoved;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="ToastService"/> class.
    /// </summary>
    /// <param name="jsRuntime">The JavaScript runtime.</param>
    public ToastService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }
    
    /// <summary>
    /// Shows a toast notification.
    /// </summary>
    /// <param name="message">The message to display.</param>
    /// <param name="type">The type of toast.</param>
    /// <param name="duration">The duration in milliseconds (0 for no auto-hide).</param>
    /// <returns>The ID of the toast.</returns>
    public string Show(string message, ToastType type = ToastType.Default, int duration = 3000)
    {
        var toast = new ToastMessage
        {
            Message = message,
            Type = type,
            Duration = duration
        };
        
        _activeToasts[toast.Id] = toast;
        OnToastAdded?.Invoke(toast);
        
        return toast.Id;
    }
    
    /// <summary>
    /// Shows a toast notification with a title.
    /// </summary>
    /// <param name="title">The title of the toast.</param>
    /// <param name="message">The message to display.</param>
    /// <param name="type">The type of toast.</param>
    /// <param name="duration">The duration in milliseconds (0 for no auto-hide).</param>
    /// <returns>The ID of the toast.</returns>
    public string Show(string title, string message, ToastType type = ToastType.Default, int duration = 3000)
    {
        var toast = new ToastMessage
        {
            Title = title,
            Message = message,
            Type = type,
            Duration = duration
        };
        
        _activeToasts[toast.Id] = toast;
        OnToastAdded?.Invoke(toast);
        
        return toast.Id;
    }
    
    /// <summary>
    /// Shows a success toast notification.
    /// </summary>
    /// <param name="message">The message to display.</param>
    /// <param name="duration">The duration in milliseconds (0 for no auto-hide).</param>
    /// <returns>The ID of the toast.</returns>
    public string ShowSuccess(string message, int duration = 3000)
    {
        return Show(message, ToastType.Success, duration);
    }
    
    /// <summary>
    /// Shows an error toast notification.
    /// </summary>
    /// <param name="message">The message to display.</param>
    /// <param name="duration">The duration in milliseconds (0 for no auto-hide).</param>
    /// <returns>The ID of the toast.</returns>
    public string ShowError(string message, int duration = 3000)
    {
        return Show(message, ToastType.Error, duration);
    }
    
    /// <summary>
    /// Shows a warning toast notification.
    /// </summary>
    /// <param name="message">The message to display.</param>
    /// <param name="duration">The duration in milliseconds (0 for no auto-hide).</param>
    /// <returns>The ID of the toast.</returns>
    public string ShowWarning(string message, int duration = 3000)
    {
        return Show(message, ToastType.Warning, duration);
    }
    
    /// <summary>
    /// Shows an info toast notification.
    /// </summary>
    /// <param name="message">The message to display.</param>
    /// <param name="duration">The duration in milliseconds (0 for no auto-hide).</param>
    /// <returns>The ID of the toast.</returns>
    public string ShowInfo(string message, int duration = 3000)
    {
        return Show(message, ToastType.Info, duration);
    }
    
    /// <summary>
    /// Shows a toast notification with custom content.
    /// </summary>
    /// <param name="content">The custom content to display.</param>
    /// <param name="options">Options for configuring the toast.</param>
    /// <returns>The ID of the toast.</returns>
    public string ShowCustom(RenderFragment content, ToastOptions? options = null)
    {
        options ??= new ToastOptions();
        
        var toast = new ToastMessage
        {
            CustomContent = content,
            Type = options.Type,
            Duration = options.Duration,
            Class = options.Class,
            DataTestId = options.DataTestId
        };
        
        _activeToasts[toast.Id] = toast;
        OnToastAdded?.Invoke(toast);
        
        return toast.Id;
    }
    
    /// <summary>
    /// Removes a toast notification by ID.
    /// </summary>
    /// <param name="toastId">The ID of the toast to remove.</param>
    public void Remove(string toastId)
    {
        if (_activeToasts.ContainsKey(toastId))
        {
            _activeToasts.Remove(toastId);
            OnToastRemoved?.Invoke(toastId);
        }
    }
    
    /// <summary>
    /// Removes all toast notifications.
    /// </summary>
    public void Clear()
    {
        var toastIds = new List<string>(_activeToasts.Keys);
        
        foreach (var id in toastIds)
        {
            Remove(id);
        }
    }
}
