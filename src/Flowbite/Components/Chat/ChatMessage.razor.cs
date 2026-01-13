using Flowbite.Utilities;
using Microsoft.AspNetCore.Components;

namespace Flowbite.Components.Chat;

/// <summary>
/// Layout container for a single conversation message.
/// </summary>
public partial class ChatMessage : Flowbite.Base.FlowbiteComponentBase
{
    private string BaseClasses => "max-w-4xl flex w-full items-end gap-3 py-4 px-1";

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
    /// Optional avatar markup rendered alongside the message.
    /// </summary>
    [Parameter]
    public RenderFragment? Avatar { get; set; }

    /// <summary>
    /// Placeholder initials for an automatically generated avatar.
    /// </summary>
    [Parameter]
    public string? AvatarInitials { get; set; }

    private string GetContainerClasses()
    {
        var variantClasses = From switch
        {
            ChatMessageRole.User => "is-user flex-row-reverse justify-end text-right",
            ChatMessageRole.Assistant => "is-assistant justify-start text-left",
            ChatMessageRole.System => "is-system justify-center text-center",
            _ => string.Empty,
        };

        return MergeClasses(ElementClass.Empty()
            .Add(BaseClasses)
            .Add(variantClasses)
            .Add(Class));
    }

    private string GetSubcontainerClasses()
    {
        var variantClasses = From switch
        {
            ChatMessageRole.User => "justify-end items-start",
            ChatMessageRole.Assistant => "justify-start items-start",
            ChatMessageRole.System => "justify-start items-start",
            _ => string.Empty,
        };

        return $"flex w-full gap-2 md:gap-3 {variantClasses}";
    }

    private bool ShouldRenderAvatar =>
        Avatar is not null || !string.IsNullOrWhiteSpace(AvatarInitials);

    private RenderFragment RenderAvatar() => builder =>
    {
        if (Avatar is not null)
        {
            builder.AddContent(0, Avatar);
            return;
        }

        builder.OpenComponent(1, typeof(Flowbite.Components.Avatar));
        builder.AddAttribute(2, "PlaceholderInitials", AvatarInitials);
        builder.AddAttribute(3, "Rounded", true);
        builder.AddAttribute(4, "Bordered", true);
        builder.AddAttribute(5, "Size", Flowbite.Components.Avatar.AvatarSize.Small);
        builder.AddAttribute(6, "Color", Flowbite.Components.Avatar.AvatarColor.Gray);
        builder.CloseComponent();
    };
}
