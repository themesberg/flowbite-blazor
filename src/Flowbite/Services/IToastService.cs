using Flowbite.Components;
using Microsoft.AspNetCore.Components;
using System;

namespace Flowbite.Services;

/// <summary>
/// Service for displaying toast notifications.
/// </summary>
public interface IToastService
{
    /// <summary>
    /// Event raised when a toast is added.
    /// </summary>
    event Action<ToastMessage> OnToastAdded;
    
    /// <summary>
    /// Event raised when a toast is removed.
    /// </summary>
    event Action<string> OnToastRemoved;
    
    /// <summary>
    /// Shows a toast notification.
    /// </summary>
    /// <param name="message">The message to display.</param>
    /// <param name="type">The type of toast.</param>
    /// <param name="duration">The duration in milliseconds (0 for no auto-hide).</param>
    /// <returns>The ID of the toast.</returns>
    string Show(string message, ToastType type = ToastType.Default, int duration = 3000);
    
    /// <summary>
    /// Shows a toast notification with a title.
    /// </summary>
    /// <param name="title">The title of the toast.</param>
    /// <param name="message">The message to display.</param>
    /// <param name="type">The type of toast.</param>
    /// <param name="duration">The duration in milliseconds (0 for no auto-hide).</param>
    /// <returns>The ID of the toast.</returns>
    string Show(string title, string message, ToastType type = ToastType.Default, int duration = 3000);
    
    /// <summary>
    /// Shows a success toast notification.
    /// </summary>
    /// <param name="message">The message to display.</param>
    /// <param name="duration">The duration in milliseconds (0 for no auto-hide).</param>
    /// <returns>The ID of the toast.</returns>
    string ShowSuccess(string message, int duration = 3000);
    
    /// <summary>
    /// Shows an error toast notification.
    /// </summary>
    /// <param name="message">The message to display.</param>
    /// <param name="duration">The duration in milliseconds (0 for no auto-hide).</param>
    /// <returns>The ID of the toast.</returns>
    string ShowError(string message, int duration = 3000);
    
    /// <summary>
    /// Shows a warning toast notification.
    /// </summary>
    /// <param name="message">The message to display.</param>
    /// <param name="duration">The duration in milliseconds (0 for no auto-hide).</param>
    /// <returns>The ID of the toast.</returns>
    string ShowWarning(string message, int duration = 3000);
    
    /// <summary>
    /// Shows an info toast notification.
    /// </summary>
    /// <param name="message">The message to display.</param>
    /// <param name="duration">The duration in milliseconds (0 for no auto-hide).</param>
    /// <returns>The ID of the toast.</returns>
    string ShowInfo(string message, int duration = 3000);
    
    /// <summary>
    /// Shows a toast notification with custom content.
    /// </summary>
    /// <param name="content">The custom content to display.</param>
    /// <param name="options">Options for configuring the toast.</param>
    /// <returns>The ID of the toast.</returns>
    string ShowCustom(RenderFragment content, ToastOptions? options = null);
    
    /// <summary>
    /// Removes a toast notification by ID.
    /// </summary>
    /// <param name="toastId">The ID of the toast to remove.</param>
    void Remove(string toastId);
    
    /// <summary>
    /// Removes all toast notifications.
    /// </summary>
    void Clear();
}
