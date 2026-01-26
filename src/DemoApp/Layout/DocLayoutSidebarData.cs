namespace DemoApp.Layout;

public class DocLayoutSidebarSection
{
    public string Title { get; set; } = string.Empty;
    public List<DocLayoutSidebarItem> Items { get; set; } = new();
}

public class DocLayoutSidebarItem
{
    public enum StatusType
    {
        None,
        New,
        Updated
    }

    public string Title { get; set; } = string.Empty;
    public string Href { get; set; } = string.Empty;
    public StatusType Status { get; set; } = StatusType.None;

    public bool IsUpdated { get; set; }
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
            Title = "Demo Apps",
            Items = new List<DocLayoutSidebarItem>
            {
                new() { Title = "AI Chat", Href = "/docs/ai/chat", Status = DocLayoutSidebarItem.StatusType.New },
                new() { Title = "Admin Dashboard", Href = "https://flowbite-blazor-admin-dashboard.pages.dev/", Status = DocLayoutSidebarItem.StatusType.New, IsExternal = true },
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
                new() { Title = "Carousel", Href = "/docs/components/carousel", Status = DocLayoutSidebarItem.StatusType.New },
                new() { Title = "Chatbot", Href = "/docs/components/chatbot", Status = DocLayoutSidebarItem.StatusType.New },
                new() { Title = "Dropdown", Href = "/docs/components/dropdown" },
                new() { Title = "Drawer", Href = "/docs/components/drawer" },
                new() { Title = "Empty State", Href = "/docs/components/empty-state", Status = DocLayoutSidebarItem.StatusType.New },
                new() { Title = "Form Validation", Href = "/docs/components/form-validation"},
                new() { Title = "Forms", Href = "/docs/components/forms", Status = DocLayoutSidebarItem.StatusType.Updated },
                new() { Title = "Modal", Href = "/docs/components/modal" },
                new() { Title = "Navbar", Href = "/docs/components/navbar" },
                new() { Title = "Pagination", Href = "/docs/components/pagination", Status = DocLayoutSidebarItem.StatusType.New },
                new() { Title = "QuickGrid", Href = "/docs/components/quickgrid" },
                new() { Title = "Sidebar", Href = "/docs/components/sidebar" },
                new() { Title = "Skeleton", Href = "/docs/components/skeleton", Status = DocLayoutSidebarItem.StatusType.New },
                new() { Title = "Spinner", Href = "/docs/components/spinner" },
                new() { Title = "Table", Href = "/docs/components/table"},
                new() { Title = "Tabs", Href = "/docs/components/tabs" },
                new() { Title = "Timeline", Href = "/docs/components/timeline", Status = DocLayoutSidebarItem.StatusType.New },
                new() { Title = "Toast", Href = "/docs/components/toast" }, // Add Toast link here
                new() { Title = "Toolbar", Href = "/docs/components/toolbar", Status = DocLayoutSidebarItem.StatusType.New },
                new() { Title = "Tooltip", Href = "/docs/components/tooltip" },
            }
        },
        new DocLayoutSidebarSection
        {
            Title = "Typography",
            Items = new List<DocLayoutSidebarItem>
            {
                new() { Title = "Heading", Href = "/docs/components/heading", Status = DocLayoutSidebarItem.StatusType.New },
                new() { Title = "Paragraph", Href = "/docs/components/paragraph", Status = DocLayoutSidebarItem.StatusType.New },
                new() { Title = "Span", Href = "/docs/components/span", Status = DocLayoutSidebarItem.StatusType.New },
            }
        }
    };
}
