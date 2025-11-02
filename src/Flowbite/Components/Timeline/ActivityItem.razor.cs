using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Components;

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

    /// <summary>
    /// Additional attributes applied to each generated list item.
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }

    private string ListItemClasses => CombineClasses("mb-10 ms-6", ListItemClass);
    private string AvatarWrapperClasses => JoinClasses("flex absolute -start-3 justify-center items-center w-6 h-6 bg-blue-200 rounded-full ring-8 ring-white dark:ring-gray-900 dark:bg-blue-900", AvatarWrapperClass);
    private string AvatarClasses => JoinClasses("rounded-full shadow-lg", AvatarClass);
    private string CardClasses => JoinClasses("p-4 bg-white rounded-lg border border-gray-200 shadow-xs dark:bg-gray-700 dark:border-gray-600", CardClass);
    private string HeaderClasses => JoinClasses("justify-between items-center mb-3 sm:flex", HeaderClass);
    private string TimeClasses => JoinClasses("mb-1 text-xs font-normal text-gray-400 sm:order-last sm:mb-0", TimeClass);
    private string TitleClasses => JoinClasses("text-sm font-normal text-gray-500 dark:text-gray-300", TitleClass);
    private string TextClasses => JoinClasses("p-3 text-xs italic font-normal text-gray-500 bg-gray-50 rounded-lg border border-gray-200 dark:bg-gray-600 dark:border-gray-500 dark:text-gray-300", TextClass);

    private static string JoinClasses(params string?[] classes)
    {
        return string.Join(" ", classes.Where(c => !string.IsNullOrWhiteSpace(c)));
    }
}
