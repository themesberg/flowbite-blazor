using Microsoft.AspNetCore.Components;

namespace Flowbite.Components.Chat;

/// <summary>
/// Layout container for a single conversation message.
/// </summary>
public partial class ChatMessage : Flowbite.Base.FlowbiteComponentBase
{
    private string BaseClasses => "group flex w-full items-end gap-3 py-4";

    /// <summary>
    /// Specifies the author role for the message.
    /// </summary>
    [Parameter]
    public ChatMessageRole From { get; set; } = ChatMessageRole.User;

    /// <summary>
    /// Content to render inside the message container.
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Additional html attributes for the container element.
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }

    private string GetContainerClasses()
    {
        var variantClasses = From switch
        {
            ChatMessageRole.User => "is-user flex-row-reverse justify-start text-right",
            ChatMessageRole.Assistant => "is-assistant justify-start text-left",
            ChatMessageRole.System => "is-system justify-center text-center",
            _ => string.Empty,
        };

        return CombineClasses(BaseClasses, variantClasses);
    }
}
