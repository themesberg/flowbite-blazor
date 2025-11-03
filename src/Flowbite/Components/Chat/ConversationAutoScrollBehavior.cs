namespace Flowbite.Components.Chat;

/// <summary>
/// Controls how the conversation view handles automatic scrolling.
/// </summary>
public enum ConversationAutoScrollBehavior
{
    /// <summary>
    /// Always scroll to the latest message after render.
    /// </summary>
    Always = 0,

    /// <summary>
    /// Only scroll when the user is already at the bottom of the conversation.
    /// </summary>
    StickToBottom = 1
}
