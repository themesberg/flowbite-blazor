namespace DemoApp.Layout;

public class DocLayoutSidebarSection
{
    public string Title { get; set; } = string.Empty;
    public List<DocLayoutSidebarItem> Items { get; set; } = new();
}

public class DocLayoutSidebarItem
{
    public string Title { get; set; } = string.Empty;
    public string Href { get; set; } = string.Empty;
    public bool IsNew { get; set; }
    public bool IsExternal { get; set; }
}

public static class DocLayoutSidebarData
{
    public static List<DocLayoutSidebarSection> Sections => new()
    {
        new DocLayoutSidebarSection
        {
            Title = "Getting Started",
            Items = new List<DocLayoutSidebarItem>
            {
                new() { Title = "Introduction", Href = "/docs/getting-started/introduction" },
                new() { Title = "Quickstart", Href = "/docs/getting-started/quickstart" },
            }
        },
        new DocLayoutSidebarSection
        {
            Title = "Components",
            Items = new List<DocLayoutSidebarItem>
            {
                new() { Title = "Alert", Href = "/docs/components/alert" },
                new() { Title = "Avatar", Href = "/docs/components/avatar" },
                new() { Title = "Badge", Href = "/docs/components/badge" },
                new() { Title = "Breadcrumb", Href = "/docs/components/breadcrumb" },
                new() { Title = "Button", Href = "/docs/components/button" },
                new() { Title = "Card", Href = "/docs/components/card" },
                new() { Title = "Dropdown", Href = "/docs/components/dropdown" },
                new() { Title = "Drawer", Href = "/docs/components/drawer", IsNew = true },
                new() { Title = "Form Validation", Href = "/docs/components/form-validation"},
                new() { Title = "Forms", Href = "/docs/components/forms" },
                new() { Title = "Modal", Href = "/docs/components/modal", IsNew = true },
                new() { Title = "Navbar", Href = "/docs/components/navbar" },
                new() { Title = "QuickGrid", Href = "/docs/components/quickgrid", IsNew = true },
                new() { Title = "Sidebar", Href = "/docs/components/sidebar" },
                new() { Title = "Spinner", Href = "/docs/components/spinner" },
                new() { Title = "Table", Href = "/docs/components/table"},
                new() { Title = "Tabs", Href = "/docs/components/tabs" },
                new() { Title = "Toast", Href = "/docs/components/toast", IsNew = true }, // Add Toast link here
                new() { Title = "Tooltip", Href = "/docs/components/tooltip" },
                
                // new() { Title = "Forms", Href = "/docs/components/form", IsNew = true },
            }
        }
    };
}
