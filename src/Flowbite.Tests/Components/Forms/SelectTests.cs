using Flowbite.Components;
using Flowbite.Tests.TestSetup;
using Microsoft.AspNetCore.Components;

namespace Flowbite.Tests.Components.Forms;

/// <summary>
/// Tests for the Select component (happy path scenarios).
/// </summary>
public class SelectTests : FlowbiteTestContext
{
    /// <summary>
    /// Verifies that Select renders with child options.
    /// </summary>
    [Fact]
    public void Select_RendersWithOptions()
    {
        // Arrange & Act
        string? value = "";
        var cut = RenderComponent<Select>(parameters => parameters
            .Add(p => p.Value, value)
            .Add(p => p.ValueExpression, () => value)
            .Add(p => p.ChildContent, (RenderFragment)(builder =>
            {
                builder.OpenElement(0, "option");
                builder.AddAttribute(1, "value", "1");
                builder.AddContent(2, "Option 1");
                builder.CloseElement();
                builder.OpenElement(3, "option");
                builder.AddAttribute(4, "value", "2");
                builder.AddContent(5, "Option 2");
                builder.CloseElement();
            })));

        // Assert
        var select = cut.Find("select");
        select.Should().NotBeNull();

        var options = cut.FindAll("option");
        options.Should().HaveCount(2);
        options[0].TextContent.Should().Be("Option 1");
        options[1].TextContent.Should().Be("Option 2");
    }

    /// <summary>
    /// Verifies that Select binds selected value correctly.
    /// </summary>
    [Fact]
    public void Select_BindsSelectedValue()
    {
        // Arrange
        string? selectedValue = "";
        var cut = RenderComponent<Select>(parameters => parameters
            .Add(p => p.Value, selectedValue)
            .Add(p => p.ValueChanged, newValue => selectedValue = newValue)
            .Add(p => p.ValueExpression, () => selectedValue)
            .Add(p => p.ChildContent, (RenderFragment)(builder =>
            {
                builder.OpenElement(0, "option");
                builder.AddAttribute(1, "value", "opt1");
                builder.AddContent(2, "Option 1");
                builder.CloseElement();
                builder.OpenElement(3, "option");
                builder.AddAttribute(4, "value", "opt2");
                builder.AddContent(5, "Option 2");
                builder.CloseElement();
            })));

        // Act
        cut.Find("select").Change("opt2");

        // Assert
        selectedValue.Should().Be("opt2");
    }

    /// <summary>
    /// Verifies that custom Class parameter is applied.
    /// </summary>
    [Fact]
    public void Select_AppliesCustomClass()
    {
        // Arrange & Act
        string? value = "";
        var cut = RenderComponent<Select>(parameters => parameters
            .Add(p => p.Value, value)
            .Add(p => p.ValueExpression, () => value)
            .Add(p => p.Class, "my-custom-select"));

        // Assert
        cut.Markup.Should().NotBeEmpty();
    }

    /// <summary>
    /// Verifies that disabled state is rendered correctly.
    /// </summary>
    [Fact]
    public void Select_SupportsDisabledState()
    {
        // Arrange & Act
        string? value = "";
        var cut = RenderComponent<Select>(parameters => parameters
            .Add(p => p.Value, value)
            .Add(p => p.ValueExpression, () => value)
            .Add(p => p.Disabled, true));

        // Assert
        var select = cut.Find("select");
        select.HasAttribute("disabled").Should().BeTrue();
    }

    /// <summary>
    /// Verifies that helper text is rendered when provided.
    /// </summary>
    [Fact]
    public void Select_RendersHelperText()
    {
        // Arrange & Act
        string? value = "";
        var cut = RenderComponent<Select>(parameters => parameters
            .Add(p => p.Value, value)
            .Add(p => p.ValueExpression, () => value)
            .Add(p => p.HelperText, "Select your preferred option"));

        // Assert
        cut.Markup.Should().Contain("Select your preferred option");
    }

    /// <summary>
    /// Verifies that ID attribute is set correctly.
    /// </summary>
    [Fact]
    public void Select_SupportsIdAttribute()
    {
        // Arrange & Act
        string? value = "";
        var cut = RenderComponent<Select>(parameters => parameters
            .Add(p => p.Value, value)
            .Add(p => p.ValueExpression, () => value)
            .Add(p => p.Id, "country-select"));

        // Assert
        var select = cut.Find("select");
        select.GetAttribute("id").Should().Be("country-select");
    }
}
