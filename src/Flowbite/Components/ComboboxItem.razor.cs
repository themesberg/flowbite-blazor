using System;
using Flowbite.Base;
using Microsoft.AspNetCore.Components;

namespace Flowbite.Components;

/// <summary>
/// Represents a selectable option within a <see cref="Combobox"/>.
/// </summary>
public partial class ComboboxItem : FlowbiteComponentBase, IDisposable
{
    private ComboboxItemRegistration? _registration;
    private string? _previousValue;
    private string? _previousLabel;
    private bool _previousDisabled;
    private RenderFragment? _previousContent;

    [CascadingParameter] private Combobox? Owner { get; set; }

    /// <summary>
    /// Gets or sets the unique option value.
    /// </summary>
    [Parameter, EditorRequired]
    public string Value { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the display label when no custom content is provided.
    /// </summary>
    [Parameter]
    public string? Label { get; set; }

    /// <summary>
    /// Optional custom markup rendered inside the option row.
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// Gets or sets whether the option is disabled.
    /// </summary>
    [Parameter]
    public bool Disabled { get; set; }

    protected override void OnInitialized()
    {
        if (Owner is null)
        {
            throw new InvalidOperationException($"{nameof(ComboboxItem)} must be used inside a {nameof(Combobox)}.");
        }

        _registration = new ComboboxItemRegistration();
        UpdateRegistration();
        CacheParameters();
        Owner.RegisterItem(_registration);
    }

    protected override void OnParametersSet()
    {
        if (_registration is null)
        {
            return;
        }

        var nextLabel = Label ?? Value;
        var hasChanged = !string.Equals(_previousValue, Value, StringComparison.Ordinal) ||
                         !string.Equals(_previousLabel, nextLabel, StringComparison.Ordinal) ||
                         _previousDisabled != Disabled ||
                         !Equals(_previousContent, ChildContent);

        if (!hasChanged)
        {
            return;
        }

        UpdateRegistration();
        CacheParameters();
        Owner?.NotifyItemsChanged();
    }

    private void UpdateRegistration()
    {
        _registration?.Update(Value, Label ?? Value, ChildContent, Disabled);
    }

    private void CacheParameters()
    {
        _previousValue = Value;
        _previousLabel = Label ?? Value;
        _previousDisabled = Disabled;
        _previousContent = ChildContent;
    }

    /// <inheritdoc />
    public void Dispose()
    {
        if (_registration is not null)
        {
            Owner?.UnregisterItem(_registration);
        }
    }
}
