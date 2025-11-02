using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Flowbite.Components;

public partial class GroupItem : FlowbiteComponentBase
{
    /// <summary>
    /// Grouped timeline entries rendered by the component.
    /// </summary>
    [Parameter]
    public IEnumerable<GroupTimelineItem>? Timelines { get; set; }

    /// <summary>
    /// Additional classes applied to the list item element.
    /// </summary>
    [Parameter]
    public string? ItemClass { get; set; }

    /// <summary>
    /// Additional classes applied to the anchor or div wrapper.
    /// </summary>
    [Parameter]
    public string? LinkClass { get; set; }

    /// <summary>
    /// Additional classes applied to the avatar image.
    /// </summary>
    [Parameter]
    public string? ImageClass { get; set; }

    /// <summary>
    /// Additional classes applied to the content container.
    /// </summary>
    [Parameter]
    public string? ContentClass { get; set; }

    /// <summary>
    /// Additional classes applied to the title element.
    /// </summary>
    [Parameter]
    public string? TitleClass { get; set; }

    /// <summary>
    /// Additional classes applied to the metadata container.
    /// </summary>
    [Parameter]
    public string? MetaClass { get; set; }

    /// <summary>
    /// Additional classes applied to the status icon.
    /// </summary>
    [Parameter]
    public string? IconClass { get; set; }

    /// <summary>
    /// Additional attributes applied to each <c>li</c> element.
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }

    private string ItemClasses => CombineClasses(string.Empty, ItemClass);
    private string LinkClasses => JoinClasses("block items-center p-3 sm:flex hover:bg-gray-100 dark:hover:bg-gray-700", LinkClass);
    private string ImageClasses => JoinClasses("me-3 mb-3 w-12 h-12 rounded-full sm:mb-0", ImageClass);
    private string ContentClasses => JoinClasses("text-gray-600 dark:text-gray-400", ContentClass);
    private string TitleClasses => JoinClasses("text-base font-normal", TitleClass);
    private string MetaClasses => JoinClasses("inline-flex items-center text-xs font-normal text-gray-500 dark:text-gray-400", MetaClass);
    private string IconClasses => JoinClasses("me-1 h-3 w-3", IconClass);

    private RenderFragment RenderContent(GroupTimelineItem item) => builder =>
    {
        var seq = 0;

        if (!string.IsNullOrWhiteSpace(item.ImageSource))
        {
            builder.OpenElement(seq++, "img");
            builder.AddAttribute(seq++, "class", ImageClasses);
            builder.AddAttribute(seq++, "src", item.ImageSource);
            builder.AddAttribute(seq++, "alt", item.ImageAlt);
            builder.CloseElement();
        }

        builder.OpenElement(seq++, "div");
        builder.AddAttribute(seq++, "class", ContentClasses);

        builder.OpenElement(seq++, "div");
        builder.AddAttribute(seq++, "class", TitleClasses);
        builder.AddContent(seq++, (MarkupString)(item.Title ?? string.Empty));
        builder.CloseElement();

        if (!string.IsNullOrWhiteSpace(item.Comment))
        {
            builder.OpenElement(seq++, "div");
            builder.AddAttribute(seq++, "class", "text-sm font-normal");
            builder.AddContent(seq++, item.Comment);
            builder.CloseElement();
        }

        builder.OpenElement(seq++, "span");
        builder.AddAttribute(seq++, "class", MetaClasses);
        RenderStatusIcon(builder, ref seq, item.IsPrivate);
        builder.AddContent(seq++, item.IsPrivate ? "Private" : "Public");
        builder.CloseElement();

        builder.CloseElement();
    };

    private void RenderStatusIcon(RenderTreeBuilder builder, ref int sequence, bool isPrivate)
    {
        builder.OpenElement(sequence++, "svg");
        builder.AddAttribute(sequence++, "class", IconClasses);
        builder.AddAttribute(sequence++, "fill", "currentColor");
        builder.AddAttribute(sequence++, "viewBox", "0 0 20 20");
        builder.AddAttribute(sequence++, "xmlns", "http://www.w3.org/2000/svg");

        if (isPrivate)
        {
            builder.OpenElement(sequence++, "path");
            builder.AddAttribute(sequence++, "fill-rule", "evenodd");
            builder.AddAttribute(sequence++, "d", "M3.707 2.293a1 1 0 00-1.414 1.414l14 14a1 1 0 001.414-1.414l-1.473-1.473A10.014 10.014 0 0019.542 10C18.268 5.943 14.478 3 10 3a9.958 9.958 0 00-4.512 1.074l-1.78-1.781zm4.261 4.26l1.514 1.515a2.003 2.003 0 012.45 2.45l1.514 1.514a4 4 0 00-5.478-5.478z");
            builder.AddAttribute(sequence++, "clip-rule", "evenodd");
            builder.CloseElement();

            builder.OpenElement(sequence++, "path");
            builder.AddAttribute(sequence++, "d", "M12.454 16.697L9.75 13.992a4 4 0 01-3.742-3.741L2.335 6.578A9.98 9.98 0 00.458 10c1.274 4.057 5.065 7 9.542 7 .847 0 1.669-.105 2.454-.303z");
            builder.CloseElement();
        }
        else
        {
            builder.OpenElement(sequence++, "path");
            builder.AddAttribute(sequence++, "fill-rule", "evenodd");
            builder.AddAttribute(sequence++, "d", "M4.083 9h1.946c.089-1.546.383-2.97.837-4.118A6.004 6.004 0 004.083 9zM10 2a8 8 0 100 16 8 8 0 000-16zm0 2c-.076 0-.232.032-.465.262-.238.234-.497.623-.737 1.182-.389.907-.673 2.142-.766 3.556h3.936c-.093-1.414-.377-2.649-.766-3.556-.24-.56-.5-.948-.737-1.182C10.232 4.032 10.076 4 10 4zm3.971 5c-.089-1.546-.383-2.97-.837-4.118A6.004 6.004 0 0115.917 9h-1.946zm-2.003 2H8.032c.093 1.414.377 2.649.766 3.556.24.56.5.948.737 1.182.233.23.389.262.465.262.076 0 .232-.032.465-.262.238-.234.498-.623.737-1.182.389-.907.673-2.142.766-3.556zm1.166 4.118c.454-1.147.748-2.572.837-4.118h1.946a6.004 6.004 0 01-2.783 4.118zm-6.268 0C6.412 13.97 6.118 12.546 6.03 11H4.083a6.004 6.004 0 002.783 4.118z");
            builder.AddAttribute(sequence++, "clip-rule", "evenodd");
            builder.CloseElement();
        }

        builder.CloseElement();
    }

    private static string JoinClasses(params string?[] classes)
    {
        return string.Join(" ", classes.Where(c => !string.IsNullOrWhiteSpace(c)));
    }
}
