using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace Flowbite.Components;

/// <summary>
/// Header component for the Modal dialog.
/// </summary>
public partial class ModalHeader
{
    /// <summary>
    /// Gets or sets the cascading modal context.
    /// </summary>
    [CascadingParameter] private ModalContext Context { get; set; } = default!;
    
    /// <summary>
    /// Gets or sets the content of the header.
    /// </summary>
    [Parameter] public RenderFragment? ChildContent { get; set; }
    
    
    /// <summary>
    /// Gets the modal ID from the context.
    /// </summary>
    private string ModalId => Context?.Id ?? "";
    
    /// <summary>
    /// Gets whether the modal can be dismissed from the context.
    /// </summary>
    private bool Dismissible => Context?.Dismissible ?? true;
    
    /// <summary>
    /// Handles the close button click event.
    /// </summary>
    private async Task HandleCloseClick()
    {
        if (Context?.CloseAsync != null)
        {
            await Context.CloseAsync();
        }
    }
    
    /// <summary>
    /// Gets the CSS classes for the header.
    /// </summary>
    /// <returns>The CSS classes for the header.</returns>
    private string GetHeaderClasses()
    {
        return MergeClasses(
            "flex items-start justify-between rounded-t border-b p-5 dark:border-gray-600",
            Class
        );
    }
    
    /// <summary>
    /// Method invoked when the component is initialized.
    /// </summary>
    protected override void OnInitialized()
    {
        if (Context == null)
        {
            throw new InvalidOperationException(
                "ModalHeader must be used within a Modal component. " +
                "Please ensure it is a child of a Modal component."
            );
        }
    }
}
