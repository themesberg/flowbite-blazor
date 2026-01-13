using Flowbite.Utilities;

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
    /// Gets the wrapper CSS classes for the table container.
    /// </summary>
    public string ContainerClasses => ElementClass.Empty()
        .Add("relative")
        .Add("overflow-x-auto", when: Responsive)
        .ToString();

    /// <summary>
    /// Gets the wrapper CSS classes for the table element.
    /// </summary>
    public string WrapperClasses => "relative";

    /// <summary>
    /// Gets the shadow CSS classes for the table.
    /// </summary>
    public string ShadowClasses => "absolute left-0 top-0 -z-10 h-full w-full rounded-lg bg-white drop-shadow-md dark:bg-gray-800";

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

}
