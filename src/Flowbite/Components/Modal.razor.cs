using Flowbite.Utilities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Threading.Tasks;

namespace Flowbite.Components;

/// <summary>
/// Modal dialog component for displaying content that requires user interaction.
/// </summary>
public partial class Modal
{
    /// <summary>
    /// Gets or sets the title of the modal.
    /// </summary>
    [Parameter] public string? Title { get; set; }
    
    /// <summary>
    /// Gets or sets the content of the modal.
    /// </summary>
    [Parameter] public RenderFragment? ChildContent { get; set; }
    
    /// <summary>
    /// Gets or sets the footer content of the modal.
    /// </summary>
    [Parameter] public RenderFragment? FooterContent { get; set; }
    
    /// <summary>
    /// Gets or sets whether the modal is visible.
    /// </summary>
    [Parameter] public bool Show { get; set; }
    
    /// <summary>
    /// Gets or sets the callback for when the Show parameter changes.
    /// </summary>
    [Parameter] public EventCallback<bool> ShowChanged { get; set; }
    
    /// <summary>
    /// Gets or sets the callback for when the modal is closed.
    /// </summary>
    [Parameter] public EventCallback OnClose { get; set; }
    
    /// <summary>
    /// Gets or sets the size of the modal.
    /// </summary>
    [Parameter] public ModalSize Size { get; set; } = ModalSize.Default;
    
    /// <summary>
    /// Gets or sets the position of the modal.
    /// </summary>
    [Parameter] public ModalPosition Position { get; set; } = ModalPosition.Center;
    
    /// <summary>
    /// Gets or sets whether the modal can be dismissed by clicking outside or pressing Escape.
    /// </summary>
    [Parameter] public bool Dismissible { get; set; } = true;
    
    /// <summary>
    /// Gets or sets the CSS class to apply to the modal backdrop.
    /// </summary>
    [Parameter] public string? BackdropClass { get; set; }
    
    /// <summary>
    /// Gets or sets the CSS class to apply to the modal container.
    /// </summary>
    [Parameter] public string? ModalClass { get; set; }
    
    /// <summary>
    /// Gets or sets the data-testid attribute for testing.
    /// </summary>
    [Parameter] public string? DataTestId { get; set; }
    
    /// <summary>
    /// The modal context to share with child components.
    /// </summary>
    private ModalContext? _modalContext;
    
    /// <summary>
    /// Method invoked when the component is initialized.
    /// </summary>
    protected override void OnInitialized()
    {
        _modalContext = new ModalContext(Id, Dismissible, CloseAsync);
        base.OnInitialized();
    }
    
    /// <summary>
    /// Method invoked when the component parameters are set.
    /// </summary>
    protected override void OnParametersSet()
    {
        // Update the context if parameters change
        _modalContext = new ModalContext(Id, Dismissible, CloseAsync);
        base.OnParametersSet();
    }
    
    /// <summary>
    /// Method invoked when the component is initialized.
    /// </summary>
    protected override async Task OnParametersSetAsync()
    {
        if (Show != IsVisible)
        {
            if (Show)
                await ShowAsync();
            else
                await HideAsync();
        }
    }
    
    /// <summary>
    /// Handles the close button click event.
    /// </summary>
    private async Task HandleCloseClick()
    {
        await CloseAsync();
    }
    
    /// <summary>
    /// Handles the backdrop click event.
    /// </summary>
    private async Task HandleBackdropClick()
    {
        if (Dismissible)
        {
            await CloseAsync();
        }
    }
    
    /// <summary>
    /// Handles the escape key press event.
    /// </summary>
    /// <param name="args">The keyboard event arguments.</param>
    private async Task HandleEscapeKey(KeyboardEventArgs args)
    {
        if (args.Key == "Escape" && Dismissible)
        {
            await CloseAsync();
        }
    }
    
    /// <summary>
    /// Closes the modal.
    /// </summary>
    private async Task CloseAsync()
    {
        await ShowChanged.InvokeAsync(false);
        await OnClose.InvokeAsync();
    }
    
    /// <summary>
    /// Gets the CSS classes for the modal backdrop.
    /// </summary>
    /// <returns>The CSS classes for the modal backdrop.</returns>
    private string GetBackdropClasses()
    {
        return MergeClasses(
            ElementClass.Empty()
                // Base classes for backdrop with transition support
                .Add("p-16 fixed inset-0 z-50 h-screen md:inset-0 md:h-full flex transition-opacity duration-300 ease-in-out motion-reduce:transition-none")
                // Visibility and opacity based on IsVisible state
                .Add("bg-gray-900/50 dark:bg-gray-900/80 opacity-100", when: IsVisible)
                .Add("bg-transparent opacity-0 invisible pointer-events-none", when: !IsVisible)
                .Add(GetPositionClasses())
                .Add(BackdropClass)
        );
    }
    
    /// <summary>
    /// Gets the CSS classes for the modal container.
    /// </summary>
    /// <returns>The CSS classes for the modal container.</returns>
    private string GetModalClasses()
    {
        return MergeClasses(
            ElementClass.Empty()
                // Base classes with scale/opacity transition for the modal itself
                .Add("relative w-full flex max-h-[90dvh] flex-col rounded-lg bg-white shadow dark:bg-gray-700 transition-all duration-300 ease-in-out motion-reduce:transition-none")
                // Scale and opacity transform based on visibility
                .Add("scale-100 opacity-100", when: IsVisible)
                .Add("scale-95 opacity-0", when: !IsVisible)
                .Add(GetSizeClasses())
                .Add(ModalClass)
        );
    }
    
    /// <summary>
    /// Gets the CSS classes for the modal position.
    /// </summary>
    /// <returns>The CSS classes for the modal position.</returns>
    private string GetPositionClasses() => Position switch
    {
        ModalPosition.TopLeft => "items-start justify-start",
        ModalPosition.TopCenter => "items-start justify-center",
        ModalPosition.TopRight => "items-start justify-end",
        ModalPosition.CenterLeft => "items-center justify-start",
        ModalPosition.CenterRight => "items-center justify-end",
        ModalPosition.BottomLeft => "items-end justify-start",
        ModalPosition.BottomCenter => "items-end justify-center",
        ModalPosition.BottomRight => "items-end justify-end",
        _ => "items-center justify-center" // Center is default
    };
    
    /// <summary>
    /// Gets the CSS classes for the modal size.
    /// </summary>
    /// <returns>The CSS classes for the modal size.</returns>
    private string GetSizeClasses() => Size switch
    {
        ModalSize.Small => "max-w-sm",
        ModalSize.Medium => "max-w-md",
        ModalSize.Large => "max-w-lg",
        ModalSize.ExtraLarge => "max-w-xl",
        ModalSize.TwoExtraLarge => "max-w-2xl",
        ModalSize.ThreeExtraLarge => "max-w-3xl",
        ModalSize.FourExtraLarge => "max-w-4xl",
        ModalSize.FiveExtraLarge => "max-w-5xl",
        ModalSize.SixExtraLarge => "max-w-6xl",
        ModalSize.SevenExtraLarge => "max-w-7xl",
        _ => "max-w-2xl" // Default
    };
}
