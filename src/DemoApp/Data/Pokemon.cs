namespace DemoApp.Data;

public class Pokemon
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Type1 { get; set; } = string.Empty;
    public string Type2 { get; set; } = string.Empty;
    public int HP { get; set; }
    public int Attack { get; set; }
    public int Defense { get; set; }
    public int SpecialAttack { get; set; }
    public int SpecialDefense { get; set; }
    public int Speed { get; set; }
}
