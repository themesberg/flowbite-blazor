using System.Collections.Generic;

namespace Flowbite.Components.Table;

public class TableSelectionState<TItem>
{
    public HashSet<TItem> SelectedItems { get; } = new();
    public EventCallback<HashSet<TItem>> SelectionChanged { get; set; }

    public void ToggleSelection(TItem item)
    {
        if (SelectedItems.Contains(item))
            SelectedItems.Remove(item);
        else
            SelectedItems.Add(item);
        
        SelectionChanged.InvokeAsync(SelectedItems);
    }
}