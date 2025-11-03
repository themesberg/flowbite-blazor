using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Flowbite.Components.Chat;

/// <summary>
/// Internal context for the prompt input model select components.
/// </summary>
internal sealed class PromptInputModelSelectContext
{
    private readonly Func<string?, Task> _onValueChanged;

    public PromptInputModelSelectContext(Func<string?, Task> onValueChanged)
    {
        _onValueChanged = onValueChanged;
    }

    public event System.Action? StateChanged;

    public string? Value { get; private set; }

    public string? DisplayLabel { get; private set; }

    public bool IsOpen { get; private set; }

    private readonly Dictionary<string, string> _options = new();

    public IReadOnlyDictionary<string, string> Options => _options;

    public void RegisterOption(string value, string label)
    {
        if (!_options.ContainsKey(value))
        {
            _options[value] = label;
        }

        if (Value == value)
        {
            DisplayLabel = label;
            StateChanged?.Invoke();
        }
    }

    public void UpdateValue(string? value)
    {
        Value = value;
        DisplayLabel = value != null && _options.TryGetValue(value, out var label) ? label : null;
        StateChanged?.Invoke();
    }

    public void Toggle()
    {
        IsOpen = !IsOpen;
        StateChanged?.Invoke();
    }

    public void Close()
    {
        if (IsOpen)
        {
            IsOpen = false;
            StateChanged?.Invoke();
        }
    }

    public async Task SelectAsync(string value, string label)
    {
        Value = value;
        DisplayLabel = label;
        IsOpen = false;
        StateChanged?.Invoke();
        await _onValueChanged.Invoke(value);
    }
}
