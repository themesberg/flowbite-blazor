using System;
using Microsoft.AspNetCore.Components;

namespace Flowbite.Components;

internal sealed class ComboboxItemRegistration
{
    private RenderFragment? _content;

    public string Id { get; } = $"flowbite-combobox-item-{Guid.NewGuid():N}";

    public string Value { get; private set; } = string.Empty;

    public string Label { get; private set; } = string.Empty;

    public bool Disabled { get; private set; }

    public RenderFragment? ChildContent => _content;

    public void Update(string value, string label, RenderFragment? content, bool disabled)
    {
        Value = value;
        Label = label;
        _content = content;
        Disabled = disabled;
    }
}
