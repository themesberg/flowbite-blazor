using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Components;

namespace Flowbite.Components;

public partial class TimelineItem : FlowbiteComponentBase
{
    private static readonly IReadOnlyDictionary<TimelineColor, TimelineColorStyle> ColorStyles = new Dictionary<TimelineColor, TimelineColorStyle>
    {
        { TimelineColor.Primary, new("bg-primary-200 dark:bg-primary-900", "ring-white dark:ring-gray-900", "text-primary-600 dark:text-primary-400", "bg-primary-200 dark:bg-primary-700") },
        { TimelineColor.Green, new("bg-green-200 dark:bg-green-900", "ring-white dark:ring-gray-900", "text-green-600 dark:text-green-400", "bg-green-200 dark:bg-green-700") },
        { TimelineColor.Orange, new("bg-orange-200 dark:bg-orange-900", "ring-white dark:ring-gray-900", "text-orange-600 dark:text-orange-400", "bg-orange-200 dark:bg-orange-700") },
        { TimelineColor.Red, new("bg-red-200 dark:bg-red-900", "ring-white dark:ring-gray-900", "text-red-600 dark:text-red-400", "bg-red-200 dark:bg-red-700") },
        { TimelineColor.Blue, new("bg-blue-200 dark:bg-blue-900", "ring-white dark:ring-gray-900", "text-blue-600 dark:text-blue-400", "bg-blue-200 dark:bg-blue-700") },
        { TimelineColor.Purple, new("bg-purple-200 dark:bg-purple-900", "ring-white dark:ring-gray-900", "text-purple-600 dark:text-purple-400", "bg-purple-200 dark:bg-purple-700") },
        { TimelineColor.Gray, new("bg-gray-200 dark:bg-gray-700", "ring-white dark:ring-gray-900", "text-gray-600 dark:text-gray-400", "bg-gray-200 dark:bg-gray-700") },
    };

    [CascadingParameter]
    public TimelineOrder Order { get; set; } = TimelineOrder.Default;

    /// <summary>
    /// Optional content rendered inside the timeline item.
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Optional content rendered in place of the default orientation indicator.
    /// </summary>
    [Parameter]
    public RenderFragment? OrientationContent { get; set; }

    /// <summary>
    /// Optional heading displayed above the item content.
    /// </summary>
    [Parameter]
    public string? Title { get; set; }

    /// <summary>
    /// Raw date string used for the datetime attribute and display.
    /// Supports natural language (e.g. "February 2022") as well as ISO strings.
    /// </summary>
    [Parameter]
    public string? Date { get; set; }

    /// <summary>
    /// Determines how the parsed date is displayed when a valid date is provided.
    /// </summary>
    [Parameter]
    public TimelineDateFormat DateFormat { get; set; } = TimelineDateFormat.MonthYear;

    /// <summary>
    /// Color variant used for the indicator and connector (where applicable).
    /// </summary>
    [Parameter]
    public TimelineColor Color { get; set; } = TimelineColor.Primary;

    /// <summary>
    /// When true, hides the connector line for vertical layouts.
    /// </summary>
    [Parameter]
    public bool IsLast { get; set; }

    /// <summary>
    /// Additional classes applied to the root list item element.
    /// </summary>
    [Parameter]
    public string? ListItemClass { get; set; }

    /// <summary>
    /// Additional classes applied to the indicator container.
    /// </summary>
    [Parameter]
    public string? IndicatorClass { get; set; }

    /// <summary>
    /// Additional classes applied to the indicator icon.
    /// </summary>
    [Parameter]
    public string? SvgClass { get; set; }

    /// <summary>
    /// Additional classes applied to the rendered time elements.
    /// </summary>
    [Parameter]
    public string? TimeClass { get; set; }

    /// <summary>
    /// Additional classes applied to the title element.
    /// </summary>
    [Parameter]
    public string? TitleClass { get; set; }

    /// <summary>
    /// Additional classes applied to the connector element for vertical layouts.
    /// </summary>
    [Parameter]
    public string? ConnectorClass { get; set; }

    /// <summary>
    /// Optional prefix rendered before the formatted date.
    /// </summary>
    [Parameter]
    public string? DatePrefix { get; set; }

    /// <summary>
    /// Additional attributes to be applied to the root list item.
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }

    private string ItemClasses => CombineClasses(GetItemBaseClasses(), ListItemClass);
    private string IndicatorClasses => JoinClasses(GetIndicatorBaseClasses(), IndicatorClass, GetColorIndicatorClasses());
    private string IndicatorIconClasses => JoinClasses("w-4 h-4", SvgClass, GetColorIconClasses());
    private string TimeClasses => JoinClasses(GetTimeBaseClasses(), TimeClass);
    private string TitleClasses => JoinClasses(GetTitleBaseClasses(), TitleClass);
    private string ConnectorClasses => JoinClasses(GetConnectorBaseClasses(), ConnectorClass, GetConnectorColorClasses());
    private bool ShouldRenderConnector => !IsLast && (Order == TimelineOrder.Vertical || Order == TimelineOrder.Activity);
    private string DisplayDate => BuildDisplayDate();

    private string GetItemBaseClasses() => Order switch
    {
        TimelineOrder.Default => "mb-10 ms-4",
        TimelineOrder.Vertical => "relative mb-10 ms-6",
        TimelineOrder.Horizontal => "relative mb-6 sm:mb-0",
        TimelineOrder.Activity => "relative mb-10 ms-6",
        _ => "mb-10 ms-4"
    };

    private string GetIndicatorBaseClasses() => Order switch
    {
        TimelineOrder.Default => "absolute w-3 h-3 bg-gray-200 rounded-full mt-1.5 -left-1.5 border border-white dark:border-gray-900 dark:bg-gray-700",
        TimelineOrder.Vertical => "flex absolute -left-4 top-1.5 justify-center items-center w-6 h-6 rounded-full ring-8",
        TimelineOrder.Horizontal => "flex items-center",
        TimelineOrder.Activity => "flex absolute -left-4 top-1.5 justify-center items-center w-6 h-6 rounded-full ring-8",
        _ => "absolute w-3 h-3 bg-gray-200 rounded-full mt-1.5 -left-1.5 border border-white dark:border-gray-900 dark:bg-gray-700"
    };

    private string GetTimeBaseClasses() => Order switch
    {
        TimelineOrder.Vertical => "mb-1 pl-4 text-sm font-normal leading-none text-gray-400 dark:text-gray-500",
        TimelineOrder.Horizontal => "mb-1 text-sm font-normal leading-none text-gray-400 dark:text-gray-500",
        TimelineOrder.Activity => "mb-1 text-sm font-normal leading-none text-gray-400 dark:text-gray-500",
        _ => "mb-1 text-sm font-normal leading-none text-gray-400 dark:text-gray-500"
    };

    private string GetTitleBaseClasses() => Order switch
    {
        TimelineOrder.Vertical => "flex ml-4 items-center mb-1 text-lg font-semibold text-gray-900 dark:text-white",
        TimelineOrder.Horizontal => "text-lg font-semibold text-gray-900 dark:text-white",
        TimelineOrder.Activity => "text-lg font-semibold text-gray-900 dark:text-white",
        _ => "text-lg font-semibold text-gray-900 dark:text-white"
    };

    private string GetConnectorBaseClasses() => Order switch
    {
        TimelineOrder.Vertical => "absolute top-7 -left-1.5 w-px h-full bg-gray-200 dark:bg-gray-700",
        TimelineOrder.Activity => "absolute top-7 -left-4 w-px h-full bg-gray-200 dark:bg-gray-700",
        _ => string.Empty
    };

    private string GetColorIndicatorClasses()
    {
        if (!ColorStyles.TryGetValue(Color, out var style))
        {
            return string.Empty;
        }

        return Order switch
        {
            TimelineOrder.Vertical => $"{style.Dot} {style.Ring}",
            TimelineOrder.Horizontal => $"{style.Dot} {style.Ring}",
            TimelineOrder.Activity => $"{style.Dot} {style.Ring}",
            _ => string.Empty
        };
    }

    private string GetColorIconClasses()
    {
        if (!ColorStyles.TryGetValue(Color, out var style))
        {
            return string.Empty;
        }

        return Order switch
        {
            TimelineOrder.Vertical => style.Icon,
            TimelineOrder.Horizontal => style.Icon,
            TimelineOrder.Activity => style.Icon,
            _ => string.Empty
        };
    }

    private string GetConnectorColorClasses()
    {
        if (Order != TimelineOrder.Vertical)
        {
            return string.Empty;
        }

        return ColorStyles.TryGetValue(Color, out var style) ? style.VerticalConnector : string.Empty;
    }

    private string BuildDisplayDate()
    {
        if (string.IsNullOrWhiteSpace(Date))
        {
            return string.Empty;
        }

        var formatted = FormatDate(Date);
        var prefix = DatePrefix;

        if (string.IsNullOrWhiteSpace(prefix))
        {
            return formatted;
        }

        prefix = prefix.Trim();
        return $"{prefix} {formatted}".Trim();
    }

    private string FormatDate(string value)
    {
        if (!TryParseDate(value, out var parsed))
        {
            return value;
        }

        var culture = CultureInfo.CurrentCulture;

        return DateFormat switch
        {
            TimelineDateFormat.Year => parsed.ToString("yyyy", culture),
            TimelineDateFormat.MonthYear => parsed.ToString("MMMM yyyy", culture),
            TimelineDateFormat.FullDate => RemoveDayOfWeek(parsed.ToString("D", culture), culture),
            _ => parsed.ToString("MMMM yyyy", culture)
        };
    }

    private static bool TryParseDate(string value, out DateTimeOffset parsed)
    {
        if (DateTimeOffset.TryParse(value, CultureInfo.CurrentCulture, DateTimeStyles.AllowWhiteSpaces, out parsed))
        {
            return true;
        }

        if (DateTimeOffset.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces, out parsed))
        {
            return true;
        }

        return false;
    }

    private static string RemoveDayOfWeek(string value, CultureInfo culture)
    {
        foreach (var dayName in culture.DateTimeFormat.DayNames)
        {
            if (string.IsNullOrEmpty(dayName))
            {
                continue;
            }

            if (value.StartsWith(dayName, StringComparison.CurrentCultureIgnoreCase))
            {
                return value[dayName.Length..].TrimStart(',', ' ', '-');
            }
        }

        return value;
    }

    private static string JoinClasses(params string?[] classes)
    {
        return string.Join(" ", classes.Where(c => !string.IsNullOrWhiteSpace(c)));
    }

    private sealed record TimelineColorStyle(string Dot, string Ring, string Icon, string VerticalConnector);
}
