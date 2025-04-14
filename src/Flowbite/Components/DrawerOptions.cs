using System;

namespace Flowbite.Components;

/// <summary>
/// Represents options for configuring a drawer.
/// </summary>
public class DrawerOptions
{
    /// <summary>
    /// Gets or sets whether the drawer can be dismissed by clicking outside or pressing Escape.
    /// </summary>
    public bool Dismissible { get; set; } = true;
    
    /// <summary>
    /// Gets or sets whether the drawer has a close button.
    /// </summary>
    public bool CloseButton { get; set; } = true;
    
    /// <summary>
    /// Gets or sets whether the drawer has a backdrop overlay.
    /// </summary>
    public bool Backdrop { get; set; } = true;
    
    /// <summary>
    /// Gets or sets whether the drawer is displayed in edge mode (partially visible when closed).
    /// </summary>
    public bool Edge { get; set; } = false;
    
    /// <summary>
    /// Gets or sets the placement of the drawer.
    /// </summary>
    public DrawerPlacement Placement { get; set; } = DrawerPlacement.Fixed;
    
    /// <summary>
    /// Gets or sets the width of the drawer when positioned on the left or right.
    /// </summary>
    public string? Width { get; set; }
    
    /// <summary>
    /// Gets or sets the height of the drawer when positioned on the top or bottom.
    /// </summary>
    public string? Height { get; set; }
    
    /// <summary>
    /// Gets or sets the CSS class to apply to the drawer.
    /// </summary>
    public string? Class { get; set; }
    
    /// <summary>
    /// Gets or sets the CSS class to apply to the drawer backdrop.
    /// </summary>
    public string? BackdropClass { get; set; }
    
    /// <summary>
    /// Gets or sets the CSS class to apply to the drawer content.
    /// </summary>
    public string? ContentClass { get; set; }
    
    /// <summary>
    /// Gets or sets the data-testid attribute for testing.
    /// </summary>
    public string? DataTestId { get; set; }
}

/// <summary>
/// Represents parameters for a drawer component.
/// </summary>
public class DrawerParameters
{
    private readonly Dictionary<string, object> _parameters = new();
    
    /// <summary>
    /// Adds a parameter to the drawer.
    /// </summary>
    /// <param name="name">The name of the parameter.</param>
    /// <param name="value">The value of the parameter.</param>
    /// <returns>The drawer parameters instance for chaining.</returns>
    public DrawerParameters Add(string name, object value)
    {
        _parameters[name] = value;
        return this;
    }
    
    /// <summary>
    /// Gets a parameter value by name.
    /// </summary>
    /// <typeparam name="T">The type of the parameter value.</typeparam>
    /// <param name="name">The name of the parameter.</param>
    /// <returns>The parameter value.</returns>
    public T Get<T>(string name)
    {
        if (_parameters.TryGetValue(name, out var value))
        {
            return (T)value;
        }
        
        throw new KeyNotFoundException($"Parameter '{name}' not found.");
    }
    
    /// <summary>
    /// Tries to get a parameter value by name.
    /// </summary>
    /// <typeparam name="T">The type of the parameter value.</typeparam>
    /// <param name="name">The name of the parameter.</param>
    /// <param name="value">The parameter value if found, default otherwise.</param>
    /// <returns>True if the parameter was found, false otherwise.</returns>
    public bool TryGet<T>(string name, out T value)
    {
        if (_parameters.TryGetValue(name, out var objValue) && objValue is T typedValue)
        {
            value = typedValue;
            return true;
        }
        
        value = default!;
        return false;
    }
    
    /// <summary>
    /// Gets all parameters as a dictionary.
    /// </summary>
    /// <returns>A dictionary of parameter names and values.</returns>
    public IReadOnlyDictionary<string, object> GetAll() => _parameters;
}

/// <summary>
/// Represents the result of a drawer.
/// </summary>
public class DrawerResult
{
    /// <summary>
    /// Gets or sets whether the drawer was confirmed.
    /// </summary>
    public bool Confirmed { get; set; }
    
    /// <summary>
    /// Gets or sets the result data.
    /// </summary>
    public object? Data { get; set; }
    
    /// <summary>
    /// Creates a new instance of the <see cref="DrawerResult"/> class with the specified result data.
    /// </summary>
    /// <param name="data">The result data.</param>
    /// <returns>A new drawer result instance.</returns>
    public static DrawerResult Ok(object? data = null) => new() { Confirmed = true, Data = data };
    
    /// <summary>
    /// Creates a new instance of the <see cref="DrawerResult"/> class with no result data.
    /// </summary>
    /// <returns>A new drawer result instance.</returns>
    public static DrawerResult Cancel() => new() { Confirmed = false };
}
