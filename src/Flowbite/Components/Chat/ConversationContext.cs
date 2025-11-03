using System;
using Microsoft.AspNetCore.Components;

namespace Flowbite.Components.Chat;

/// <summary>
/// Provides shared state between conversation related components.
/// </summary>
internal sealed class ConversationContext
{
    private ElementReference _contentElement;

    /// <summary>
    /// Indicates whether the content element reference has been set.
    /// </summary>
    public bool HasContentElement => !string.IsNullOrEmpty(_contentElement.Id);

    /// <summary>
    /// Gets the registered content element.
    /// </summary>
    public ElementReference ContentElement => _contentElement;

    /// <summary>
    /// Indicates whether the conversation is currently scrolled to the bottom.
    /// </summary>
    public bool IsAtBottom { get; private set; } = true;

    /// <summary>
    /// Event raised when the content element is registered.
    /// </summary>
    public event System.Action<ElementReference>? ContentRegistered;

    /// <summary>
    /// Event raised when the scroll position toggles between bottom and non-bottom states.
    /// </summary>
    public event System.Action<bool>? AtBottomChanged;

    /// <summary>
    /// Registers the content element that should be scrolled.
    /// </summary>
    /// <param name="element">The element reference for the scrollable content.</param>
    public void RegisterContent(ElementReference element)
    {
        _contentElement = element;
        ContentRegistered?.Invoke(element);
    }

    /// <summary>
    /// Delegate used by child components to request scrolling to the bottom.
    /// </summary>
    public Func<Task>? ScrollToBottom { get; set; }

    /// <summary>
    /// Updates the stored scroll position and notifies listeners when it changes.
    /// </summary>
    /// <param name="isAtBottom">True when the scroll area is at the bottom.</param>
    public void UpdateIsAtBottom(bool isAtBottom)
    {
        if (IsAtBottom == isAtBottom)
        {
            return;
        }

        IsAtBottom = isAtBottom;
        AtBottomChanged?.Invoke(isAtBottom);
    }
}
