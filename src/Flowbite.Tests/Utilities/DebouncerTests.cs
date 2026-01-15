using Flowbite.Utilities;

namespace Flowbite.Tests.Utilities;

/// <summary>
/// Tests for the Debouncer utility class.
/// </summary>
public class DebouncerTests
{
    /// <summary>
    /// Verifies that rapid sequential calls result in only one callback after the delay.
    /// </summary>
    [Fact]
    public async Task Debounce_FiresOnceAfterDelay()
    {
        // Arrange
        var callCount = 0;
        var debouncer = new Debouncer();

        // Act - Make rapid calls
        // Using longer delays for CI reliability
        _ = debouncer.DebounceAsync(() => { callCount++; return Task.CompletedTask; }, 100);
        await Task.Delay(20);
        _ = debouncer.DebounceAsync(() => { callCount++; return Task.CompletedTask; }, 100);
        await Task.Delay(20);
        _ = debouncer.DebounceAsync(() => { callCount++; return Task.CompletedTask; }, 100);

        // Wait for debounce to complete (extra margin for CI)
        await Task.Delay(200);

        // Assert
        callCount.Should().Be(1, because: "only the last debounced call should execute");

        // Cleanup
        debouncer.Dispose();
    }

    /// <summary>
    /// Verifies that disposal cancels any pending debounced operation.
    /// </summary>
    [Fact]
    public async Task Debounce_CancelsOnDispose()
    {
        // Arrange
        var callCount = 0;
        var debouncer = new Debouncer();

        // Act - Start a debounced call then dispose immediately
        _ = debouncer.DebounceAsync(() => { callCount++; return Task.CompletedTask; }, 100);
        debouncer.Dispose();

        // Wait longer than the debounce delay
        await Task.Delay(150);

        // Assert
        callCount.Should().Be(0, because: "disposal should cancel the pending operation");
    }

    /// <summary>
    /// Verifies that each new call cancels the previous pending call.
    /// </summary>
    [Fact]
    public async Task Debounce_CancelsPreviousCallOnRapidCalls()
    {
        // Arrange
        var executedValues = new List<string>();
        var debouncer = new Debouncer();

        // Act - Make rapid calls with different values
        // Using longer delays for CI reliability
        _ = debouncer.DebounceAsync(() => { executedValues.Add("first"); return Task.CompletedTask; }, 100);
        await Task.Delay(30);
        _ = debouncer.DebounceAsync(() => { executedValues.Add("second"); return Task.CompletedTask; }, 100);
        await Task.Delay(30);
        _ = debouncer.DebounceAsync(() => { executedValues.Add("third"); return Task.CompletedTask; }, 100);

        // Wait for debounce to complete (extra margin for CI)
        await Task.Delay(200);

        // Assert
        executedValues.Should().HaveCount(1);
        executedValues.Should().Contain("third", because: "only the last call should execute");

        // Cleanup
        debouncer.Dispose();
    }

    /// <summary>
    /// Verifies that disposing before the delay expires prevents execution.
    /// </summary>
    [Fact]
    public async Task Debounce_DoesNotFireWhenDisposedBeforeDelay()
    {
        // Arrange
        var executed = false;
        var debouncer = new Debouncer();

        // Act - Start debounce and dispose before it fires
        _ = debouncer.DebounceAsync(() => { executed = true; return Task.CompletedTask; }, 200);
        await Task.Delay(50); // Wait a bit but less than debounce delay
        debouncer.Dispose();
        await Task.Delay(200); // Wait longer than original delay

        // Assert
        executed.Should().BeFalse(because: "disposal should prevent the action from executing");
    }
}
