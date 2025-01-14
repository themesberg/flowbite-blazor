
using System.Text;

namespace Flowbite.Components.Base;

#pragma warning disable CA1708 // Identifiers should differ by more than case
public partial class UiComponentBase : ComponentBase
#pragma warning restore CA1708 // Identifiers should differ by more than case
{

    public string? Class => @class;

#pragma warning disable IDE1006 // Naming Styles
    [Parameter] public string? @class { get; set; }
#pragma warning restore IDE1006 // Naming Styles


    protected bool HasErrors { get; set; }
    protected List<string> ErrorMessages { get; set; } = [];


    protected virtual string ClassNames(params string?[] classes)
    {
        var sb = new StringBuilder();
        foreach (var cls in classes)
        {
            if (string.IsNullOrEmpty(cls))
            {
                continue;
            }

            if (sb.Length > 0)
            {
                _ = sb.Append(' ');
            }

            _ = sb.Append(cls);
        }
        return sb.ToString();
    }

}