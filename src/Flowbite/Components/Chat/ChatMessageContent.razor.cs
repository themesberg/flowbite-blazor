using Microsoft.AspNetCore.Components;
using System.Text;

namespace Flowbite.Components.Chat;

/// <summary>
/// Visual bubble for message content.
/// </summary>
public partial class ChatMessageContent : Flowbite.Base.FlowbiteComponentBase
{
    /// <summary>
    /// Determines the visual variant for the content bubble.
    /// </summary>
    [Parameter]
    public ChatMessageVariant Variant { get; set; } = ChatMessageVariant.Contained;

    /// <summary>
    /// Child content of the message bubble.
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Additional HTML attributes applied to the wrapper element.
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }

    [CascadingParameter] private ChatMessageRole Role { get; set; }

    private string GetContentClasses()
    {
        var builder = new StringBuilder("flex flex-col gap-2 text-sm transition");

        switch (Variant)
        {
            case ChatMessageVariant.Flat:
                builder.Append(Role switch
                {
                    ChatMessageRole.User => " rounded-2xl bg-gray-100 px-4 py-3 text-white shadow-sm dark:bg-gray-700 dark:text-white",
                    ChatMessageRole.Assistant => " rounded-2xl border border-gray-200 bg-gray-50 px-4 py-3 text-gray-900 shadow-sm dark:border-gray-700 dark:bg-gray-800/60 dark:text-gray-100",
                    ChatMessageRole.System => " rounded-md bg-amber-100 px-4 py-2 text-amber-900 shadow-sm dark:bg-amber-900/30 dark:text-amber-200",
                    _ => string.Empty
                });
                break;
            default:
                builder.Append(Role switch
                {
                    ChatMessageRole.User => " rounded-2xl bg-primary-600 px-4 py-3 text-white shadow-md dark:bg-primary-500",
                    ChatMessageRole.Assistant => " rounded-2xl border border-gray-200 bg-white px-4 py-3 text-gray-900 shadow-md dark:border-gray-700 dark:bg-gray-800 dark:text-gray-100",
                    ChatMessageRole.System => " rounded-2xl bg-amber-100 px-4 py-3 text-amber-900 shadow-md dark:bg-amber-900/40 dark:text-amber-100",
                    _ => " rounded-2xl bg-gray-100 px-4 py-3"
                });
                break;
        }

        return CombineClasses(builder.ToString());
    }
}
