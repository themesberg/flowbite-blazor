using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using System.Linq;

namespace Flowbite.Components;

/// <summary>
/// TextInput component for forms and user input.
/// </summary>
public partial class TextInput<TValue> : IDisposable
{
    private const string BaseWrapperClasses = "relative flex";
    private const string BaseFieldClasses = "relative w-full";
    private const string BaseInputClasses = "block w-full border disabled:cursor-not-allowed disabled:opacity-50 bg-gray-50 border-gray-300 text-gray-900 focus:border-primary-500 focus:ring-primary-500 dark:border-gray-600 dark:bg-gray-700 dark:text-white dark:placeholder-gray-400 dark:focus:border-primary-500 dark:focus:ring-primary-500";
    private const string BaseAddonClasses = "inline-flex items-center border border-gray-300 bg-gray-200 px-3 text-sm text-gray-900 dark:border-gray-600 dark:bg-gray-600 dark:text-gray-400";
    private const string BaseIconClasses = "pointer-events-none absolute inset-y-0 left-0 flex items-center pl-3";
    private const string BaseRightIconClasses = "pointer-events-none absolute inset-y-0 right-0 flex items-center pr-3";
    private const string BaseHelperTextClasses = "mt-1 text-sm";

    /// <summary>
    /// Gets or sets the value of the input.
    /// </summary>
    [Parameter] public TValue? Value { get; set; }

    /// <summary>
    /// Event callback for when the input value changes.
    /// </summary>
    [Parameter] public EventCallback<TValue> ValueChanged { get; set; }

    /// <summary>
    /// Gets or sets the color variant of the input.
    /// </summary>
    [Parameter] public TextInputColor Color { get; set; } = TextInputColor.Gray;


    [CascadingParameter] private EditContext? CurrentEditContext { get; set; }

    /// <summary>
    /// Gets or sets the size variant of the input.
    /// </summary>
    [Parameter] public TextInputSize Size { get; set; } = TextInputSize.Medium;

    /// <summary>
    /// Gets or sets the type of the input (e.g., "text", "password", "email").
    /// </summary>
    [Parameter] public string Type { get; set; } = "text";

    /// <summary>
    /// Gets or sets the placeholder text.
    /// </summary>
    [Parameter] public string? Placeholder { get; set; }

    /// <summary>
    /// Gets or sets whether the input is required.
    /// </summary>
    [Parameter] public bool Required { get; set; }

    /// <summary>
    /// Gets or sets whether the input is disabled.
    /// </summary>
    [Parameter] public bool Disabled { get; set; }

    /// <summary>
    /// Gets or sets whether the input has a shadow effect.
    /// </summary>
    [Parameter] public bool Shadow { get; set; }

    /// <summary>
    /// Gets or sets the helper text displayed below the input.
    /// </summary>
    [Parameter] public string? HelperText { get; set; }

    /// <summary>
    /// Gets or sets the icon displayed on the left side of the input.
    /// </summary>
    [Parameter] public IconBase? Icon { get; set; }

    /// <summary>
    /// Gets or sets the icon displayed on the right side of the input.
    /// </summary>
    [Parameter] public IconBase? RightIcon { get; set; }

    /// <summary>
    /// Gets or sets the text to display before the input.
    /// </summary>
    [Parameter] public string? AddonLeft { get; set; }

    /// <summary>
    /// Gets or sets the text to display after the input.
    /// </summary>
    [Parameter] public string? AddonRight { get; set; }

    /// <summary>
    /// Additional attributes that will be applied to the input element.
    /// </summary>
    [Parameter(CaptureUnmatchedValues = true)]
    public Dictionary<string, object>? AdditionalAttributes { get; set; }

    private string GetWrapperClasses() => BaseWrapperClasses;

    private string GetFieldClasses() => BaseFieldClasses;

    private string GetAddonClasses(bool isLeft = true)
    {
        var classes = new List<string> { BaseAddonClasses };
        
        if (isLeft)
        {
            classes.Add("rounded-l-md border-r-0");
        }
        else
        {
            classes.Add("rounded-r-md border-l-0");
        }

        return string.Join(" ", classes);
    }

    private string GetIconClasses() => BaseIconClasses;

    private string GetRightIconClasses() => BaseRightIconClasses;

    protected override void OnInitialized()
    {
        if (CurrentEditContext != null)
        {
            CurrentEditContext.OnValidationStateChanged += ValidationStateChanged;
        }
    }

    private void ValidationStateChanged(object? sender, ValidationStateChangedEventArgs e)
    {
        // Nothing to do for now.
    }

    public void Dispose()
    {
        if (CurrentEditContext != null)
        {
            CurrentEditContext.OnValidationStateChanged -= ValidationStateChanged;
        }
    }

    private string GetInputClasses()
    {
        var classes = new List<string> { BaseInputClasses };

        // Add size classes
        var sizeClasses = Size switch
        {
            TextInputSize.Small => "p-2 text-sm",
            TextInputSize.Large => "sm:text-md p-4",
            _ => "p-2.5 text-sm" // Medium (default)
        };
        classes.Add(sizeClasses);

        // Add color classes
        var colorClasses = Color switch
        {
            TextInputColor.Success => "border-green-500 bg-green-50 text-green-900 placeholder-green-700 focus:border-green-500 focus:ring-green-500 dark:border-green-400 dark:bg-green-100 dark:focus:border-green-500 dark:focus:ring-green-500",
            TextInputColor.Failure => "border-red-500 bg-red-50 text-red-900 placeholder-red-700 focus:border-red-500 focus:ring-red-500 dark:border-red-400 dark:bg-red-100 dark:focus:border-red-500 dark:focus:ring-red-500",
            TextInputColor.Warning => "border-yellow-500 bg-yellow-50 text-yellow-900 placeholder-yellow-700 focus:border-yellow-500 focus:ring-yellow-500 dark:border-yellow-400 dark:bg-yellow-100 dark:focus:border-yellow-500 dark:focus:ring-yellow-500",
            TextInputColor.Info => "border-cyan-500 bg-cyan-50 text-cyan-900 placeholder-cyan-700 focus:border-cyan-500 focus:ring-cyan-500 dark:border-cyan-400 dark:bg-cyan-100 dark:focus:border-cyan-500 dark:focus:ring-cyan-500",
            _ => string.Empty // Gray (default) uses base classes
        };
        if (!string.IsNullOrEmpty(colorClasses))
        {
            classes.Add(colorClasses);
        }

        // Add icon padding
        if (Icon != null)
        {
            classes.Add("pl-10");
        }
        if (RightIcon != null)
        {
            classes.Add("pr-10");
        }

        // Add addon classes
        if (!string.IsNullOrEmpty(AddonLeft) && !string.IsNullOrEmpty(AddonRight))
        {
            // No rounded corners when both addons are present
            classes.Add("border-l-0 border-r-0");
        }
        else if (!string.IsNullOrEmpty(AddonLeft))
        {
            classes.Add("rounded-r-lg border-l-0");
        }
        else if (!string.IsNullOrEmpty(AddonRight))
        {
            classes.Add("rounded-l-lg border-r-0");
        }
        else
        {
            classes.Add("rounded-lg");
        }

        // Add shadow
        if (Shadow)
        {
            classes.Add("shadow-sm dark:shadow-sm-light");
        }

        return string.Join(" ", classes);
    }

    private string GetHelperTextClasses()
    {
        var classes = new List<string> { BaseHelperTextClasses };

        // Add color classes
        var colorClasses = Color switch
        {
            TextInputColor.Success => "text-green-600 dark:text-green-500",
            TextInputColor.Failure => "text-red-600 dark:text-red-500",
            TextInputColor.Warning => "text-yellow-600 dark:text-yellow-500",
            TextInputColor.Info => "text-cyan-600 dark:text-cyan-500",
            _ => "text-gray-500 dark:text-gray-400" // Gray (default)
        };
        classes.Add(colorClasses);

        return string.Join(" ", classes);
    }

    private string? CurrentValueAsString
    {
        get => Value?.ToString();
        set
        {
            var success = false;
            var parsedValue = default(TValue);

            if (typeof(TValue) == typeof(int))
            {
                success = int.TryParse(value, out var result);
                parsedValue = (TValue)(object)result;
            }
            else if (typeof(TValue) == typeof(decimal))
            {
                success = decimal.TryParse(value, out var result);
                parsedValue = (TValue)(object)result;
            }
            else if (typeof(TValue) == typeof(double))
            {
                success = double.TryParse(value, out var result);
                parsedValue = (TValue)(object)result;
            }
            else if (typeof(TValue) == typeof(float))
            {
                success = float.TryParse(value, out var result);
                parsedValue = (TValue)(object)result;
            }
            else if (typeof(TValue) == typeof(string))
            {
                success = true;
                parsedValue = (TValue)(object)(value ?? string.Empty);
            }

            if (success && !EqualityComparer<TValue>.Default.Equals(parsedValue, Value))
            {
                Value = parsedValue;
                _ = ValueChanged.InvokeAsync(Value);
            }
        }
    }

    private Task OnInputChanged(ChangeEventArgs e)
    {
        CurrentValueAsString = e.Value?.ToString();
        return Task.CompletedTask;
    }
}
