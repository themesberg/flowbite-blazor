using Flowbite.Utilities;

namespace Flowbite.Tests.Utilities;

/// <summary>
/// Tests for the ElementClass fluent builder.
/// </summary>
public class ElementClassTests
{
    /// <summary>
    /// Verifies that an empty ElementClass returns an empty string.
    /// </summary>
    [Fact]
    public void Empty_ReturnsEmptyString()
    {
        // Arrange & Act
        var result = ElementClass.Empty().ToString();

        // Assert
        result.Should().BeEmpty();
    }

    /// <summary>
    /// Verifies that adding a single class returns that class.
    /// </summary>
    [Fact]
    public void Add_SingleClass_ReturnsClass()
    {
        // Arrange & Act
        var result = ElementClass.Empty()
            .Add("px-4")
            .ToString();

        // Assert
        result.Should().Be("px-4");
    }

    /// <summary>
    /// Verifies that adding multiple classes combines them with spaces.
    /// </summary>
    [Fact]
    public void Add_MultipleClasses_CombinesClasses()
    {
        // Arrange & Act
        var result = ElementClass.Empty()
            .Add("px-4")
            .Add("py-2")
            .Add("rounded font-medium")
            .ToString();

        // Assert
        result.Should().Be("px-4 py-2 rounded font-medium");
    }

    /// <summary>
    /// Verifies that conditional add with true includes the class.
    /// </summary>
    [Fact]
    public void Add_ConditionalTrue_IncludesClass()
    {
        // Arrange
        var isDisabled = true;

        // Act
        var result = ElementClass.Empty()
            .Add("base-class")
            .Add("opacity-50 cursor-not-allowed", when: isDisabled)
            .ToString();

        // Assert
        result.Should().Be("base-class opacity-50 cursor-not-allowed");
    }

    /// <summary>
    /// Verifies that conditional add with false excludes the class.
    /// </summary>
    [Fact]
    public void Add_ConditionalFalse_ExcludesClass()
    {
        // Arrange
        var isDisabled = false;

        // Act
        var result = ElementClass.Empty()
            .Add("base-class")
            .Add("opacity-50 cursor-not-allowed", when: isDisabled)
            .ToString();

        // Assert
        result.Should().Be("base-class");
    }

    /// <summary>
    /// Verifies that implicit string conversion works correctly.
    /// </summary>
    [Fact]
    public void ImplicitConversion_WorksCorrectly()
    {
        // Arrange & Act
        string result = ElementClass.Empty()
            .Add("flex")
            .Add("gap-4");

        // Assert
        result.Should().Be("flex gap-4");
    }

    /// <summary>
    /// Verifies that null and empty values are ignored.
    /// </summary>
    [Fact]
    public void Add_NullOrEmptyValues_AreIgnored()
    {
        // Arrange & Act
        var result = ElementClass.Empty()
            .Add("valid")
            .Add(null)
            .Add("")
            .Add("   ")
            .Add("also-valid")
            .ToString();

        // Assert
        result.Should().Be("valid also-valid");
    }

    /// <summary>
    /// Verifies that the builder can be used in complex scenarios.
    /// </summary>
    [Fact]
    public void Add_ComplexScenario_BuildsCorrectly()
    {
        // Arrange
        var size = "medium";
        var isActive = true;
        var customClass = "custom-class";

        // Act
        var result = ElementClass.Empty()
            .Add("base")
            .Add("small", when: size == "small")
            .Add("medium", when: size == "medium")
            .Add("large", when: size == "large")
            .Add("active", when: isActive)
            .Add("inactive", when: !isActive)
            .Add(customClass)
            .ToString();

        // Assert
        result.Should().Be("base medium active custom-class");
    }
}
