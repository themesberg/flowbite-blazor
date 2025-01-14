using Microsoft.AspNetCore.Components;
using System.Linq;

namespace Flowbite.Base
{
    /// <summary>
    /// Base class for Flowbite Blazor components, providing common functionality and styling.
    /// </summary>
    public abstract class FlowbiteComponentBase : ComponentBase
    {
        /// <summary>
        /// Additional CSS classes to apply to the component.
        /// </summary>
        [Parameter]
        public string? Class { get; set; }

        /// <summary>
        /// Combines multiple CSS class strings with additional user-provided classes.
        /// </summary>
        /// <param name="classes">The CSS classes to combine</param>
        /// <returns>A combined string of CSS classes</returns>
        protected string CombineClasses(params string?[] classes)
        {
            var allClasses = classes.Concat(new[] { Class }).Where(c => !string.IsNullOrWhiteSpace(c));
            return string.Join(" ", allClasses).Trim();
        }

    }
}
