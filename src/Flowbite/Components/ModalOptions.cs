using System;

namespace Flowbite.Components;

/// <summary>
/// Represents options for configuring a modal dialog.
/// </summary>
public class ModalOptions
{
    /// <summary>
    /// Gets or sets the title of the modal.
    /// </summary>
    public string? Title { get; set; }
    
    /// <summary>
    /// Gets or sets the size of the modal.
    /// </summary>
    public ModalSize Size { get; set; } = ModalSize.Default;
    
    /// <summary>
    /// Gets or sets the position of the modal.
    /// </summary>
    public ModalPosition Position { get; set; } = ModalPosition.Center;
    
    /// <summary>
    /// Gets or sets whether the modal can be dismissed by clicking outside or pressing Escape.
    /// </summary>
    public bool Dismissible { get; set; } = true;
    
    /// <summary>
    /// Gets or sets whether the modal has a close button in the header.
    /// </summary>
    public bool CloseButton { get; set; } = true;
    
    /// <summary>
    /// Gets or sets whether the modal has a backdrop overlay.
    /// </summary>
    public bool Backdrop { get; set; } = true;
    
    /// <summary>
    /// Gets or sets the CSS class to apply to the modal.
    /// </summary>
    public string? Class { get; set; }
    
    /// <summary>
    /// Gets or sets the CSS class to apply to the modal backdrop.
    /// </summary>
    public string? BackdropClass { get; set; }
    
    /// <summary>
    /// Gets or sets the CSS class to apply to the modal content.
    /// </summary>
    public string? ContentClass { get; set; }
    
    /// <summary>
    /// Gets or sets the CSS class to apply to the modal header.
    /// </summary>
    public string? HeaderClass { get; set; }
    
    /// <summary>
    /// Gets or sets the CSS class to apply to the modal footer.
    /// </summary>
    public string? FooterClass { get; set; }
    
    /// <summary>
    /// Gets or sets the data-testid attribute for testing.
    /// </summary>
    public string? DataTestId { get; set; }
}

/// <summary>
/// Represents parameters for a modal component.
/// </summary>
public class ModalParameters
{
    private readonly Dictionary<string, object> _parameters = new();
    
    /// <summary>
    /// Adds a parameter to the modal.
    /// </summary>
    /// <param name="name">The name of the parameter.</param>
    /// <param name="value">The value of the parameter.</param>
    /// <returns>The modal parameters instance for chaining.</returns>
    public ModalParameters Add(string name, object value)
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
/// Represents the result of a modal dialog.
/// </summary>
/// <typeparam name="TResult">The type of the result data.</typeparam>
public class ModalResult<TResult>
{
    /// <summary>
    /// Gets or sets whether the modal was confirmed.
    /// </summary>
    public bool Confirmed { get; set; }
    
    /// <summary>
    /// Gets or sets the result data.
    /// </summary>
    public TResult? Data { get; set; }
    
    /// <summary>
    /// Creates a new instance of the <see cref="ModalResult{TResult}"/> class with the specified result data.
    /// </summary>
    /// <param name="data">The result data.</param>
    /// <returns>A new modal result instance.</returns>
    public static ModalResult<TResult> Ok(TResult data) => new() { Confirmed = true, Data = data };
    
    /// <summary>
    /// Creates a new instance of the <see cref="ModalResult{TResult}"/> class with no result data.
    /// </summary>
    /// <returns>A new modal result instance.</returns>
    public static ModalResult<TResult> Cancel() => new() { Confirmed = false };
}
