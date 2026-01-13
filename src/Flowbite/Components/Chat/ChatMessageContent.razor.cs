using Flowbite.Utilities;
using Microsoft.AspNetCore.Components;

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

    [CascadingParameter] private ChatMessageRole Role { get; set; }

    private string GetContentClasses()
    {
        var variantClasses = Variant switch
        {
            ChatMessageVariant.Flat => Role switch
            {
                ChatMessageRole.User => "rounded-2xl bg-gray-100 px-4 py-2 text-white dark:bg-gray-700 dark:text-white",
                ChatMessageRole.Assistant => "text-gray-600 dark:text-gray-300",
                ChatMessageRole.System => "rounded-md bg-amber-100 px-4 py-2 text-amber-900 dark:bg-amber-900/30 dark:text-amber-200",
                _ => string.Empty
            },
            _ => Role switch
            {
                ChatMessageRole.User => "rounded-2xl bg-primary-600 px-3 py-2 text-white shadow-md",
                ChatMessageRole.Assistant => "rounded-2xl border border-gray-200 bg-white px-4 py-3 text-gray-900 shadow-md dark:border-gray-700 dark:bg-gray-800 dark:text-gray-100",
                ChatMessageRole.System => "rounded-2xl bg-amber-100 px-4 py-3 text-amber-900 shadow-md dark:bg-amber-900/40 dark:text-amber-100",
                _ => "rounded-2xl bg-gray-100 px-4 py-3"
            }
        };

        return MergeClasses(
            ElementClass.Empty()
                .Add("flex flex-col gap-2 transition motion-reduce:transition-none")
                .Add(variantClasses)
                .Add(Class));
    }
}
