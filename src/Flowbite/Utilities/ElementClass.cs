namespace Flowbite.Utilities;

/// <summary>
/// Fluent builder for constructing CSS class strings with conditional logic.
/// </summary>
/// <example>
/// <code>
/// // Basic usage
/// var classes = ElementClass.Empty()
///     .Add("px-4 py-2 rounded font-medium")
///     .Add("opacity-50 cursor-not-allowed", when: isDisabled)
///     .Add(userClass);
/// 
/// // Implicit string conversion
/// string cssClasses = ElementClass.Empty().Add("flex").Add("gap-4");
/// </code>
/// </example>
public record struct ElementClass
{
    private string? _buffer;

    /// <summary>
    /// Creates a new empty ElementClass instance.
    /// </summary>
    /// <returns>A new empty ElementClass.</returns>
    public static ElementClass Empty() => new();

    /// <summary>
    /// Adds CSS class(es) to the builder.
    /// </summary>
    /// <param name="value">The CSS class(es) to add. Null or whitespace values are ignored.</param>
    /// <returns>The updated ElementClass instance.</returns>
    public ElementClass Add(string? value)
    {
        if (!string.IsNullOrWhiteSpace(value))
            _buffer = string.IsNullOrEmpty(_buffer) ? value : $"{_buffer} {value}";
        return this;
    }

    /// <summary>
    /// Conditionally adds CSS class(es) to the builder.
    /// </summary>
    /// <param name="value">The CSS class(es) to add. Null or whitespace values are ignored.</param>
    /// <param name="when">If true, the classes are added; if false, they are ignored.</param>
    /// <returns>The updated ElementClass instance.</returns>
    public ElementClass Add(string? value, bool when) => when ? Add(value) : this;

    /// <summary>
    /// Returns the combined CSS class string.
    /// </summary>
    /// <returns>The combined CSS classes, or an empty string if no classes were added.</returns>
    public override string ToString() => _buffer ?? string.Empty;

    /// <summary>
    /// Implicitly converts an ElementClass to a string.
    /// </summary>
    /// <param name="el">The ElementClass instance to convert.</param>
    public static implicit operator string(ElementClass el) => el.ToString();
}
