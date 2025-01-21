namespace Flowbite.Components.Table;

public class TableState
{
    public bool Striped { get; }
    public bool Hoverable { get; }
    public bool Bordered { get; }
    public bool Responsive { get; }
    public string? Alignment { get; set; }
    public bool HasCaption { get; set; }
    
    public TableState(bool striped,
                      bool hoverable,
                      bool bordered,
                      bool responsive)
    {
        Striped = striped;
        Hoverable = hoverable;
        Bordered = bordered;
        Responsive = responsive;
    }
}