using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using Flowbite.Utilities;

namespace Flowbite.Components;

public partial class ActivityItem : FlowbiteComponentBase
{
    /// <summary>
    /// Activity entries rendered by the component.
    /// </summary>
    [Parameter]
    public IEnumerable<ActivityTimelineItem>? Activities { get; set; }

    /// <summary>
    /// Additional classes applied to the list item wrapper.
    /// </summary>
    [Parameter]
    public string? ListItemClass { get; set; }

    /// <summary>
    /// Additional classes applied to the avatar wrapper element.
    /// </summary>
    [Parameter]
    public string? AvatarWrapperClass { get; set; }

    /// <summary>
    /// Additional classes applied directly to the avatar image.
    /// </summary>
    [Parameter]
    public string? AvatarClass { get; set; }

    /// <summary>
    /// Additional classes applied to the outer card container.
    /// </summary>
    [Parameter]
    public string? CardClass { get; set; }

    /// <summary>
    /// Additional classes applied to the header (time/title) container.
    /// </summary>
    [Parameter]
    public string? HeaderClass { get; set; }

    /// <summary>
    /// Additional classes applied to the rendered time element.
    /// </summary>
    [Parameter]
    public string? TimeClass { get; set; }

    /// <summary>
    /// Additional classes applied to the title container.
    /// </summary>
    [Parameter]
    public string? TitleClass { get; set; }

    /// <summary>
    /// Additional classes applied to the optional text container.
    /// </summary>
    [Parameter]
    public string? TextClass { get; set; }

    private string ListItemClasses => MergeClasses(
        ElementClass.Empty()
            .Add("mb-10 ms-6")
            .Add(ListItemClass));

    private string AvatarWrapperClasses => MergeClasses(
        ElementClass.Empty()
            .Add("flex absolute -start-3 justify-center items-center w-6 h-6 bg-blue-200 rounded-full ring-8 ring-white dark:ring-gray-900 dark:bg-blue-900")
            .Add(AvatarWrapperClass));

    private string AvatarClasses => MergeClasses(
        ElementClass.Empty()
            .Add("rounded-full shadow-lg")
            .Add(AvatarClass));

    private string CardClasses => MergeClasses(
        ElementClass.Empty()
            .Add("p-4 bg-white rounded-lg border border-gray-200 shadow-xs dark:bg-gray-700 dark:border-gray-600")
            .Add(CardClass));

    private string HeaderClasses => MergeClasses(
        ElementClass.Empty()
            .Add("justify-between items-center mb-3 sm:flex")
            .Add(HeaderClass));

    private string TimeClasses => MergeClasses(
        ElementClass.Empty()
            .Add("mb-1 text-xs font-normal text-gray-400 sm:order-last sm:mb-0")
            .Add(TimeClass));

    private string TitleClasses => MergeClasses(
        ElementClass.Empty()
            .Add("text-sm font-normal text-gray-500 dark:text-gray-300")
            .Add(TitleClass));

    private string TextClasses => MergeClasses(
        ElementClass.Empty()
            .Add("p-3 text-xs italic font-normal text-gray-500 bg-gray-50 rounded-lg border border-gray-200 dark:bg-gray-600 dark:border-gray-500 dark:text-gray-300")
            .Add(TextClass));
}
