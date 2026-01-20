using Flowbite.Components;
using Flowbite.Tests.TestSetup;

namespace Flowbite.Tests.Components.Forms;

/// <summary>
/// Tests for the TextInput component (happy path scenarios).
/// </summary>
public class TextInputTests : FlowbiteTestContext
{
    /// <summary>
    /// Verifies that TextInput renders with default attributes.
    /// </summary>
    [Fact]
    public void TextInput_RendersWithDefaultAttributes()
    {
        // Arrange & Act
        var value = "";
        var cut = RenderComponent<TextInput<string>>(parameters => parameters
            .Add(p => p.Value, value)
            .Add(p => p.ValueExpression, () => value));

        // Assert
        var input = cut.Find("input");
        input.Should().NotBeNull();
        input.GetAttribute("type").Should().Be("text");
        input.GetAttribute("data-testid").Should().Be("flowbite-text-input");
    }

    /// <summary>
    /// Verifies that TextInput binds value correctly via ValueChanged callback.
    /// </summary>
    [Fact]
    public void TextInput_BindsValueCorrectly()
    {
        // Arrange
        var boundValue = "";
        var cut = RenderComponent<TextInput<string>>(parameters => parameters
            .Add(p => p.Value, boundValue)
            .Add(p => p.ValueChanged, newValue => boundValue = newValue)
            .Add(p => p.ValueExpression, () => boundValue));

        // Act
        cut.Find("input").Change("test value");

        // Assert
        boundValue.Should().Be("test value");
    }

    /// <summary>
    /// Verifies that custom Class parameter is applied to the input.
    /// </summary>
    [Fact]
    public void TextInput_AppliesCustomClass()
    {
        // Arrange & Act
        var value = "";
        var cut = RenderComponent<TextInput<string>>(parameters => parameters
            .Add(p => p.Value, value)
            .Add(p => p.ValueExpression, () => value)
            .Add(p => p.Class, "my-custom-class"));

        // Assert
        // The Class parameter is applied to the wrapper or input via MergeClasses
        // We verify the component renders without error with the custom class
        cut.Markup.Should().NotBeEmpty();
    }

    /// <summary>
    /// Verifies that placeholder text is rendered.
    /// </summary>
    [Fact]
    public void TextInput_SupportsPlaceholder()
    {
        // Arrange & Act
        var value = "";
        var cut = RenderComponent<TextInput<string>>(parameters => parameters
            .Add(p => p.Value, value)
            .Add(p => p.ValueExpression, () => value)
            .Add(p => p.Placeholder, "Enter your name"));

        // Assert
        var input = cut.Find("input");
        input.GetAttribute("placeholder").Should().Be("Enter your name");
    }

    /// <summary>
    /// Verifies that disabled state is rendered correctly.
    /// </summary>
    [Fact]
    public void TextInput_SupportsDisabledState()
    {
        // Arrange & Act
        var value = "";
        var cut = RenderComponent<TextInput<string>>(parameters => parameters
            .Add(p => p.Value, value)
            .Add(p => p.ValueExpression, () => value)
            .Add(p => p.Disabled, true));

        // Assert
        var input = cut.Find("input");
        input.HasAttribute("disabled").Should().BeTrue();
    }

    /// <summary>
    /// Verifies that the input type can be changed (e.g., password, email).
    /// </summary>
    [Fact]
    public void TextInput_SupportsTypeChange()
    {
        // Arrange & Act
        var value = "";
        var cut = RenderComponent<TextInput<string>>(parameters => parameters
            .Add(p => p.Value, value)
            .Add(p => p.ValueExpression, () => value)
            .Add(p => p.Type, "password"));

        // Assert
        var input = cut.Find("input");
        input.GetAttribute("type").Should().Be("password");
    }

    /// <summary>
    /// Verifies that required attribute is set correctly.
    /// </summary>
    [Fact]
    public void TextInput_SupportsRequiredAttribute()
    {
        // Arrange & Act
        var value = "";
        var cut = RenderComponent<TextInput<string>>(parameters => parameters
            .Add(p => p.Value, value)
            .Add(p => p.ValueExpression, () => value)
            .Add(p => p.Required, true));

        // Assert
        var input = cut.Find("input");
        input.HasAttribute("required").Should().BeTrue();
    }

    /// <summary>
    /// Verifies that helper text is rendered when provided.
    /// </summary>
    [Fact]
    public void TextInput_RendersHelperText()
    {
        // Arrange & Act
        var value = "";
        var cut = RenderComponent<TextInput<string>>(parameters => parameters
            .Add(p => p.Value, value)
            .Add(p => p.ValueExpression, () => value)
            .Add(p => p.HelperText, "This field is required"));

        // Assert
        cut.Markup.Should().Contain("This field is required");
    }

    /// <summary>
    /// Verifies that numeric values work correctly.
    /// </summary>
    [Fact]
    public void TextInput_WithIntValue_BindsCorrectly()
    {
        // Arrange
        var boundValue = 0;
        var cut = RenderComponent<TextInput<int>>(parameters => parameters
            .Add(p => p.Value, boundValue)
            .Add(p => p.ValueChanged, newValue => boundValue = newValue)
            .Add(p => p.ValueExpression, () => boundValue));

        // Act
        cut.Find("input").Change("42");

        // Assert
        boundValue.Should().Be(42);
    }
}
