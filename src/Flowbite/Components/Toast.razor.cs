using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Flowbite.Components;

/// <summary>
/// Represents a single toast notification component.
/// </summary>
public partial class Toast : ComponentBase, IDisposable
{
    [Inject] private IJSRuntime JSRuntime { get; set; } = default!;

    /// <summary>
    /// The ToastMessage object containing the data for this toast.
    /// </summary>
    [Parameter, EditorRequired] public ToastMessage ToastMessage { get; set; } = default!;

    /// <summary>
    /// Callback invoked when the toast's close button is clicked or the duration expires.
    /// The parent component (ToastHost) should handle the actual removal.
    /// </summary>
    [Parameter] public EventCallback OnClose { get; set; }

    /// <summary>
    /// Specifies whether the toast includes a close button. Defaults to true.
    /// This might be overridden by ToastOptions if custom content is used.
    /// </summary>
    [Parameter] public bool CloseButton { get; set; } = true;

    private ElementReference _toastElement;
    private System.Timers.Timer? _timer; // Explicitly qualify Timer
    private bool _isVisible = true; // Controls visibility for fade-out animation
    private Type? _iconComponentType;
    private string? _iconColorClass;
    private string? _textColorClass;
    private string? _bgColorClass;
    private string? _borderColorClass;
    private string? _closeButtonClass;

    protected override void OnInitialized()
    {
        SetAppearanceClasses(ToastMessage.Type);
        if (ToastMessage.Duration > 0)
        {
            StartTimer(ToastMessage.Duration);
        }
    }

    private void SetAppearanceClasses(ToastType type)
    {
        // Base classes
        _textColorClass = "text-gray-500 dark:text-gray-400";
        _bgColorClass = "bg-white dark:bg-gray-800";
        _borderColorClass = "border-gray-200 dark:border-gray-700"; // Default border, might be overridden
        _closeButtonClass = "ms-auto -mx-1.5 -my-1.5 bg-white text-gray-400 hover:text-gray-900 rounded-lg focus:ring-2 focus:ring-gray-300 p-1.5 hover:bg-gray-100 inline-flex items-center justify-center h-8 w-8 dark:text-gray-500 dark:hover:text-white dark:bg-gray-800 dark:hover:bg-gray-700";

        switch (type)
        {
            case ToastType.Info:
                _iconComponentType = typeof(Icons.InfoCircleIcon);
                _iconColorClass = "text-blue-500 dark:text-blue-400";
                // Use default text/bg/border/close
                break;
            case ToastType.Success:
                _iconComponentType = typeof(Icons.CheckCircleIcon);
                _iconColorClass = "text-green-500 dark:text-green-400";
                _textColorClass = "text-green-500 dark:text-green-400";
                _bgColorClass = "bg-green-100 dark:bg-green-800";
                _borderColorClass = "border-green-200 dark:border-green-700";
                _closeButtonClass = "ms-auto -mx-1.5 -my-1.5 bg-green-100 text-green-500 hover:text-green-900 rounded-lg focus:ring-2 focus:ring-green-400 p-1.5 hover:bg-green-200 inline-flex items-center justify-center h-8 w-8 dark:text-green-400 dark:hover:text-white dark:bg-green-800 dark:hover:bg-green-700";
                break;
            case ToastType.Warning:
                _iconComponentType = typeof(Icons.ExclamationTriangleIcon);
                _iconColorClass = "text-yellow-500 dark:text-yellow-400";
                _textColorClass = "text-yellow-500 dark:text-yellow-400";
                _bgColorClass = "bg-yellow-100 dark:bg-yellow-800";
                _borderColorClass = "border-yellow-200 dark:border-yellow-700";
                _closeButtonClass = "ms-auto -mx-1.5 -my-1.5 bg-yellow-100 text-yellow-500 hover:text-yellow-900 rounded-lg focus:ring-2 focus:ring-yellow-400 p-1.5 hover:bg-yellow-200 inline-flex items-center justify-center h-8 w-8 dark:text-yellow-400 dark:hover:text-white dark:bg-yellow-800 dark:hover:bg-yellow-700";
                break;
            case ToastType.Error:
                _iconComponentType = typeof(Icons.CloseCircleSolidIcon);
                _iconColorClass = "text-red-500 dark:text-red-400";
                _textColorClass = "text-red-500 dark:text-red-400";
                _bgColorClass = "bg-red-100 dark:bg-red-800";
                _borderColorClass = "border-red-200 dark:border-red-700";
                _closeButtonClass = "ms-auto -mx-1.5 -my-1.5 bg-red-100 text-red-500 hover:text-red-900 rounded-lg focus:ring-2 focus:ring-red-400 p-1.5 hover:bg-red-200 inline-flex items-center justify-center h-8 w-8 dark:text-red-400 dark:hover:text-white dark:bg-red-800 dark:hover:bg-red-700";
                break;
            case ToastType.Default:
            default:
                _iconComponentType = null; // No icon for default
                _iconColorClass = null;
                _textColorClass = "text-gray-900 dark:text-white";
                _bgColorClass = "bg-gray-100 dark:bg-gray-700";
                _borderColorClass = "border-gray-200 dark:border-gray-600";
                _closeButtonClass = "ms-auto -mx-1.5 -my-1.5 bg-gray-100 text-gray-500 hover:text-gray-900 rounded-lg focus:ring-2 focus:ring-gray-300 p-1.5 hover:bg-gray-200 inline-flex items-center justify-center h-8 w-8 dark:text-gray-400 dark:hover:text-white dark:bg-gray-700 dark:hover:bg-gray-600";
                break;
        }
    }

    private void StartTimer(int duration)
    {
        _timer = new System.Timers.Timer(duration); // Explicitly qualify Timer
        _timer.Elapsed += HandleTimerElapsed;
        _timer.AutoReset = false;
        _timer.Start();
    }

    private void HandleTimerElapsed(object? sender, ElapsedEventArgs e)
    {
        // Timer elapsed, initiate close process
        InvokeAsync(CloseToast);
    }

    private async Task CloseToast()
    {
        _timer?.Stop();
        _timer?.Dispose();
        _timer = null;

        _isVisible = false; // Trigger fade-out animation
        StateHasChanged();

        // Wait for animation to complete before notifying parent
        // Adjust delay based on CSS animation duration
        await Task.Delay(300); // Example: 300ms animation

        await OnClose.InvokeAsync(); // Notify ToastHost to remove
    }

    private string GetToastClasses()
    {
        // Add 'border' class and the specific border color class
        var sb = new StringBuilder("w-full max-w-xs p-4 rounded-lg shadow border "); 
        sb.Append(_borderColorClass); // Add border color class
        sb.Append(' ');
        sb.Append(_textColorClass);
        sb.Append(' ');
        sb.Append(_bgColorClass); 
        sb.Append(' ');
        // Add animation classes based on visibility
        sb.Append(_isVisible ? "toast-fade-in" : "toast-fade-out");
        if (!string.IsNullOrEmpty(ToastMessage.Class))
        {
            sb.Append(' ');
            sb.Append(ToastMessage.Class);
        }
        return sb.ToString();
    }

    private string GetIconContainerClasses()
    {
        var sb = new StringBuilder("inline-flex items-center justify-center flex-shrink-0 w-8 h-8 rounded-lg ");
        sb.Append(_iconColorClass?.Replace("text-", "bg-")?.Replace("400", "800")?.Replace("500", "100")); // Derive bg from text color
        sb.Append(' ');
        sb.Append(_iconColorClass); // Text color for the icon itself
        return sb.ToString();
    }

    public void Dispose()
    {
        _timer?.Stop();
        _timer?.Dispose();
        GC.SuppressFinalize(this);
    }
}
