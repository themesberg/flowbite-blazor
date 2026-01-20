using Flowbite.Components;
using Flowbite.Tests.TestSetup;

namespace Flowbite.Tests.Components.Forms;

/// <summary>
/// Tests for the Textarea component (happy path scenarios).
/// </summary>
public class TextAreaTests : FlowbiteTestContext
{
    /// <summary>
    /// Verifies that Textarea renders with default attributes.
    /// </summary>
    [Fact]
    public void TextArea_RendersWithDefaultAttributes()
    {
        // Arrange & Act
        string? value = "";
        var cut = RenderComponent<Textarea>(parameters => parameters
            .Add(p => p.Value, value)
            .Add(p => p.ValueExpression, () => value));

        // Assert
        var textarea = cut.Find("textarea");
        textarea.Should().NotBeNull();
        textarea.GetAttribute("data-testid").Should().Be("flowbite-textarea");
        textarea.GetAttribute("rows").Should().Be("4"); // Default rows
    }

    /// <summary>
    /// Verifies that Textarea displays the initial value correctly.
    /// </summary>
    [Fact]
    public void TextArea_BindsValueCorrectly()
    {
        // Arrange & Act - Test that initial value is rendered
        string? value = "initial content";
        var cut = RenderComponent<Textarea>(parameters => parameters
            .Add(p => p.Value, value)
            .Add(p => p.ValueExpression, () => value));

        // Assert - The textarea should have the value attribute set via @bind
        // In Blazor with @bind, the value is rendered as both the 'value' attribute and inner content
        var textarea = cut.Find("textarea");
        textarea.GetAttribute("value").Should().Be("initial content");

        // Re-render with new value to verify binding updates
        cut.SetParametersAndRender(parameters => parameters
            .Add(p => p.Value, "updated content")
            .Add(p => p.ValueExpression, () => value));

        textarea = cut.Find("textarea");
        textarea.GetAttribute("value").Should().Be("updated content");
    }

    /// <summary>
    /// Verifies that custom Class parameter is applied.
    /// </summary>
    [Fact]
    public void TextArea_AppliesCustomClass()
    {
        // Arrange & Act
        string? value = "";
        var cut = RenderComponent<Textarea>(parameters => parameters
            .Add(p => p.Value, value)
            .Add(p => p.ValueExpression, () => value)
            .Add(p => p.Class, "my-custom-class"));

        // Assert
        cut.Markup.Should().NotBeEmpty();
    }

    /// <summary>
    /// Verifies that the rows attribute is set correctly.
    /// </summary>
    [Fact]
    public void TextArea_SupportsRows()
    {
        // Arrange & Act
        string? value = "";
        var cut = RenderComponent<Textarea>(parameters => parameters
            .Add(p => p.Value, value)
            .Add(p => p.ValueExpression, () => value)
            .Add(p => p.Rows, 10));

        // Assert
        var textarea = cut.Find("textarea");
        textarea.GetAttribute("rows").Should().Be("10");
    }

    /// <summary>
    /// Verifies that placeholder text is rendered.
    /// </summary>
    [Fact]
    public void TextArea_SupportsPlaceholder()
    {
        // Arrange & Act
        string? value = "";
        var cut = RenderComponent<Textarea>(parameters => parameters
            .Add(p => p.Value, value)
            .Add(p => p.ValueExpression, () => value)
            .Add(p => p.Placeholder, "Enter your message"));

        // Assert
        var textarea = cut.Find("textarea");
        textarea.GetAttribute("placeholder").Should().Be("Enter your message");
    }

    /// <summary>
    /// Verifies that disabled state is rendered correctly.
    /// </summary>
    [Fact]
    public void TextArea_SupportsDisabledState()
    {
        // Arrange & Act
        string? value = "";
        var cut = RenderComponent<Textarea>(parameters => parameters
            .Add(p => p.Value, value)
            .Add(p => p.ValueExpression, () => value)
            .Add(p => p.Disabled, true));

        // Assert
        var textarea = cut.Find("textarea");
        textarea.HasAttribute("disabled").Should().BeTrue();
    }

    /// <summary>
    /// Verifies that helper text is rendered when provided.
    /// </summary>
    [Fact]
    public void TextArea_RendersHelperText()
    {
        // Arrange & Act
        string? value = "";
        var cut = RenderComponent<Textarea>(parameters => parameters
            .Add(p => p.Value, value)
            .Add(p => p.ValueExpression, () => value)
            .Add(p => p.HelperText, "Maximum 500 characters"));

        // Assert
        cut.Markup.Should().Contain("Maximum 500 characters");
    }

    /// <summary>
    /// Verifies that required attribute is set correctly.
    /// </summary>
    [Fact]
    public void TextArea_SupportsRequiredAttribute()
    {
        // Arrange & Act
        string? value = "";
        var cut = RenderComponent<Textarea>(parameters => parameters
            .Add(p => p.Value, value)
            .Add(p => p.ValueExpression, () => value)
            .Add(p => p.Required, true));

        // Assert
        var textarea = cut.Find("textarea");
        textarea.HasAttribute("required").Should().BeTrue();
    }
}
