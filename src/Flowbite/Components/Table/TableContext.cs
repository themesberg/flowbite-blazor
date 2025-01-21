using Microsoft.AspNetCore.Components;

namespace Flowbite.Components.Table;

/// <summary>
/// Represents the context for a Table component, sharing state across all table subcomponents.
/// </summary>
public class TableContext
{
    /// <summary>
    /// Gets whether the table has striped rows.
    /// </summary>
    public bool Striped { get; }

    /// <summary>
    /// Gets whether the table rows are hoverable.
    /// </summary>
    public bool Hoverable { get; }

    /// <summary>
    /// Gets whether the table has borders.
    /// </summary>
    public bool Bordered { get; }

    /// <summary>
    /// Gets whether the table is responsive.
    /// </summary>
    public bool Responsive { get; }

    /// <summary>
    /// Gets the base CSS classes for the table.
    /// </summary>
    public string BaseClasses => "w-full text-left text-sm text-gray-500 dark:text-gray-400";

    /// <summary>
    /// Gets the wrapper CSS classes for the table.
    /// </summary>
    public string WrapperClasses => CombineClasses(
        "relative",
        Responsive ? "overflow-x-auto" : string.Empty,
        "rounded-lg"
    );

    /// <summary>
    /// Gets the shadow CSS classes for the table.
    /// </summary>
    public string ShadowClasses => "relative w-full";

    /// <summary>
    /// Initializes a new instance of the TableContext class.
    /// </summary>
    public TableContext(bool striped, bool hoverable, bool bordered, bool responsive)
    {
        Striped = striped;
        Hoverable = hoverable;
        Bordered = bordered;
        Responsive = responsive;
    }

    private string CombineClasses(params string[] classes)
    {
        return string.Join(" ", classes.Where(c => !string.IsNullOrWhiteSpace(c)));
    }
}