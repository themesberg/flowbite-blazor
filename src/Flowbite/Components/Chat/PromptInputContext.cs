using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components.Forms;

namespace Flowbite.Components.Chat;

/// <summary>
/// Internal context shared between prompt input subcomponents.
/// </summary>
internal sealed class PromptInputContext
{
    private string _text = string.Empty;

    public PromptInputContext(Func<Task> triggerFilePicker, Func<Task> submitAsync, bool multiple)
    {
        TriggerFilePickerAsync = triggerFilePicker;
        SubmitAsync = submitAsync;
        Multiple = multiple;
    }

    /// <summary>
    /// Raised when the text value changes.
    /// </summary>
    public event System.Action? TextChanged;

    /// <summary>
    /// Raised when the attachments collection changes.
    /// </summary>
    public event System.Action? AttachmentsChanged;

    /// <summary>
    /// Invokes the file picker dialog.
    /// </summary>
    public Func<Task> TriggerFilePickerAsync { get; }

    /// <summary>
    /// Invokes a prompt submission.
    /// </summary>
    public Func<Task> SubmitAsync { get; }

    /// <summary>
    /// Indicates whether multiple attachments are allowed.
    /// </summary>
    public bool Multiple { get; set; }

    /// <summary>
    /// Gets the current text content.
    /// </summary>
    public string Text
    {
        get => _text;
        private set
        {
            if (_text == value)
            {
                return;
            }

            _text = value;
            TextChanged?.Invoke();
        }
    }

    /// <summary>
    /// The attachments associated with the prompt.
    /// </summary>
    public List<PromptAttachment> Attachments { get; } = new();

    /// <summary>
    /// Updates the input text.
    /// </summary>
    /// <param name="value">The new text value.</param>
    public void SetText(string value)
    {
        Text = value;
    }

    /// <summary>
    /// Adds the provided files as attachments.
    /// </summary>
    public void AddFiles(IEnumerable<IBrowserFile> files)
    {
        var updated = false;

        foreach (var file in files)
        {
            if (!Multiple && Attachments.Count >= 1)
            {
                break;
            }

            Attachments.Add(new PromptAttachment(file));
            updated = true;
        }

        if (updated)
        {
            AttachmentsChanged?.Invoke();
        }
    }

    /// <summary>
    /// Removes an attachment by identifier.
    /// </summary>
    /// <param name="id">The attachment id.</param>
    public void RemoveAttachment(string id)
    {
        var removed = Attachments.RemoveAll(x => x.Id == id) > 0;
        if (removed)
        {
            AttachmentsChanged?.Invoke();
        }
    }

    /// <summary>
    /// Clears all attachments.
    /// </summary>
    public void ClearAttachments()
    {
        if (Attachments.Count == 0)
        {
            return;
        }

        Attachments.Clear();
        AttachmentsChanged?.Invoke();
    }
}
