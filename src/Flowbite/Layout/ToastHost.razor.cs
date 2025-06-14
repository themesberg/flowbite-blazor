using Flowbite.Components;
using Flowbite.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flowbite.Layout;

/// <summary>
/// Container component responsible for displaying toast notifications.
/// It listens to the IToastService and renders Toast components accordingly.
/// </summary>
public partial class ToastHost : IDisposable
{
    [Inject] private IToastService ToastService { get; set; } = default!;

    /// <summary>
    /// Specifies the position where toasts should appear on the screen.
    /// Defaults to TopRight.
    /// </summary>
    [Parameter] public ToastPosition Position { get; set; } = ToastPosition.TopRight;

    /// <summary>
    /// The unique identifier for this toast host. Toasts can be targeted to a specific host.
    /// If null, this host will be the default and will display any toasts without a specific HostId.
    /// </summary>
    [Parameter] public string? HostId { get; set; }

    private readonly List<ToastMessage> _activeToasts = new();
    private string PositionClasses => GetPositionClasses();

    protected override void OnInitialized()
    {
        ToastService.OnToastAdded += HandleToastAdded;
        ToastService.OnToastRemoved += HandleToastRemoved;
    }

    private void HandleToastAdded(ToastMessage toast)
    {
        // Only add the toast if it's targeted to this host
        if (toast.HostId == HostId)
        {
            InvokeAsync(() =>
            {
                _activeToasts.Add(toast);
                StateHasChanged();
            });
        }
    }

    private void HandleToastRemoved(string toastId)
    {
        InvokeAsync(() =>
        {
            var toastToRemove = _activeToasts.FirstOrDefault(t => t.Id == toastId);
            if (toastToRemove != null)
            {
                _activeToasts.Remove(toastToRemove);
                StateHasChanged();
            }
        });
    }

    private async Task HandleToastClose(string toastId)
    {
        // The Toast component handles the dismissal animation.
        // Once the animation is complete, it calls this handler via OnClose.
        // We then tell the service to remove it, which triggers HandleToastRemoved.
        await InvokeAsync(() => ToastService.Remove(toastId));
    }

    private string GetPositionClasses()
    {
        return Position switch
        {
            ToastPosition.TopLeft => "fixed top-5 left-5 z-[80] space-y-4",
            ToastPosition.TopCenter => "fixed top-5 left-1/2 -translate-x-1/2 z-[80] space-y-4",
            ToastPosition.TopRight => "fixed top-5 right-5 z-[80] space-y-4",
            ToastPosition.BottomLeft => "fixed bottom-5 left-5 z-[80] space-y-4",
            ToastPosition.BottomCenter => "fixed bottom-5 left-1/2 -translate-x-1/2 z-[80] space-y-4",
            ToastPosition.BottomRight => "fixed bottom-5 right-5 z-[80] space-y-4",
            _ => "fixed top-5 right-5 z-[80] space-y-4" // Default to TopRight
        };
    }

    public void Dispose()
    {
        ToastService.OnToastAdded -= HandleToastAdded;
        ToastService.OnToastRemoved -= HandleToastRemoved;
        GC.SuppressFinalize(this);
    }
}
