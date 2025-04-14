using System;
using System.Threading.Tasks;

namespace Flowbite.Components;

/// <summary>
/// Represents the context shared between a Modal and its child components.
/// </summary>
public class ModalContext
{
    /// <summary>
    /// Gets the unique identifier for the modal.
    /// </summary>
    public string Id { get; }
    
    /// <summary>
    /// Gets whether the modal can be dismissed by clicking outside or pressing Escape.
    /// </summary>
    public bool Dismissible { get; }
    
    /// <summary>
    /// Gets the function to close the modal.
    /// </summary>
    public Func<Task> CloseAsync { get; }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="ModalContext"/> class.
    /// </summary>
    /// <param name="id">The unique identifier for the modal.</param>
    /// <param name="dismissible">Whether the modal can be dismissed by clicking outside or pressing Escape.</param>
    /// <param name="closeAsync">The function to close the modal.</param>
    public ModalContext(string id, bool dismissible, Func<Task> closeAsync)
    {
        Id = id;
        Dismissible = dismissible;
        CloseAsync = closeAsync;
    }
}
