using Flowbite.Common;

namespace Flowbite.Tests.Components;

/// <summary>
/// Tests for the CollapseState enum.
/// </summary>
public class CollapseStateTests
{
    /// <summary>
    /// Verifies that CollapseState enum has all required values for the animation state machine.
    /// </summary>
    [Fact]
    public void CollapseState_HasAllRequiredValues()
    {
        // Arrange
        var expectedValues = new[]
        {
            CollapseState.Collapsed,
            CollapseState.Expanding,
            CollapseState.Expanded,
            CollapseState.Collapsing
        };

        // Act
        var actualValues = Enum.GetValues<CollapseState>();

        // Assert
        actualValues.Should().HaveCount(4, because: "CollapseState should have exactly 4 states");
        actualValues.Should().BeEquivalentTo(expectedValues);
    }

    /// <summary>
    /// Verifies the logical state transitions are representable.
    /// </summary>
    [Theory]
    [InlineData(CollapseState.Collapsed, CollapseState.Expanding, "Expanding from collapsed")]
    [InlineData(CollapseState.Expanding, CollapseState.Expanded, "Finished expanding")]
    [InlineData(CollapseState.Expanded, CollapseState.Collapsing, "Starting to collapse")]
    [InlineData(CollapseState.Collapsing, CollapseState.Collapsed, "Finished collapsing")]
    public void CollapseState_SupportsExpectedTransitions(CollapseState from, CollapseState to, string description)
    {
        // This test documents the expected state transitions
        // The actual transition logic is in the component
        from.Should().NotBe(to, because: $"{description} should change state");
    }

    /// <summary>
    /// Verifies that the default value is Collapsed.
    /// </summary>
    [Fact]
    public void CollapseState_DefaultIsCollapsed()
    {
        // Arrange & Act
        CollapseState defaultState = default;

        // Assert
        defaultState.Should().Be(CollapseState.Collapsed,
            because: "the first enum value (0) should be Collapsed for sensible defaults");
    }
}
