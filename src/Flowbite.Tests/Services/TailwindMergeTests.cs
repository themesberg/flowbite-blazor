using Flowbite.Tests.TestSetup;
using TailwindMerge;

namespace Flowbite.Tests.Services;

/// <summary>
/// Tests for TailwindMerge integration in Flowbite services.
/// </summary>
public class TailwindMergeTests : FlowbiteTestContext
{
    /// <summary>
    /// Verifies that TailwindMerge resolves conflicting utility classes.
    /// </summary>
    [Fact]
    public void MergeClasses_ResolvesConflicts()
    {
        // Arrange
        var twMerge = Services.GetRequiredService<TwMerge>();

        // Act
        var result = twMerge.Merge("p-2 p-4 text-red-500 text-blue-500");

        // Assert
        result.Should().Be("p-4 text-blue-500", because: "later classes should override earlier conflicting classes");
    }

    /// <summary>
    /// Verifies that non-conflicting classes are preserved.
    /// </summary>
    [Fact]
    public void MergeClasses_PreservesNonConflicting()
    {
        // Arrange
        var twMerge = Services.GetRequiredService<TwMerge>();

        // Act
        var result = twMerge.Merge("p-2 m-4 text-red-500 bg-blue-500");

        // Assert
        result.Should().Contain("p-2");
        result.Should().Contain("m-4");
        result.Should().Contain("text-red-500");
        result.Should().Contain("bg-blue-500");
    }

    /// <summary>
    /// Verifies that null and empty inputs are handled gracefully.
    /// </summary>
    [Fact]
    public void MergeClasses_HandlesNullAndEmpty()
    {
        // Arrange
        var twMerge = Services.GetRequiredService<TwMerge>();

        // Act & Assert - Empty string returns null or empty (TwMerge returns null for empty input)
        var emptyResult = twMerge.Merge("");
        emptyResult.Should().BeNullOrEmpty();

        // Act & Assert - Whitespace only
        var whitespaceResult = twMerge.Merge("   ");
        whitespaceResult.Should().BeNullOrEmpty();

        // Act & Assert - Mixed with empty parts
        var mixedResult = twMerge.Merge("p-4  m-2");
        mixedResult.Should().Contain("p-4");
        mixedResult.Should().Contain("m-2");
    }

    /// <summary>
    /// Verifies that complex Tailwind class combinations are merged correctly.
    /// </summary>
    [Fact]
    public void MergeClasses_HandlesComplexCombinations()
    {
        // Arrange
        var twMerge = Services.GetRequiredService<TwMerge>();

        // Act - Simulate component base classes + user override
        var baseClasses = "bg-gray-50 border-gray-300 text-gray-900 rounded-lg p-2.5";
        var userClasses = "p-4 rounded-xl";
        var result = twMerge.Merge($"{baseClasses} {userClasses}");

        // Assert - User overrides should win
        result.Should().Contain("p-4");
        result.Should().NotContain("p-2.5");
        result.Should().Contain("rounded-xl");
        result.Should().NotContain("rounded-lg");
        // Non-conflicting base classes should be preserved
        result.Should().Contain("bg-gray-50");
        result.Should().Contain("border-gray-300");
        result.Should().Contain("text-gray-900");
    }
}
