using FluentAssertions;
using Xunit;

namespace AdventOfCode.App.Y2021.Problems.Day2;

public class InputTests
{
    [Theory]
    [InlineData("forward 1", Command.Forward, 1)]
    [InlineData("UP 2", Command.Up, 2)]
    [InlineData("DoWn 3", Command.Down, 3)]
    [InlineData("forward   4", Command.Forward, 4)]
    [InlineData("   up 5", Command.Up, 5)]
    [InlineData("down 6   ", Command.Down, 6)]
    [InlineData("\nforward 7", Command.Forward, 7)]
    [InlineData("up\n8", Command.Up, 8)]
    [InlineData("down 9\n", Command.Down, 9)]
    public void TryParse_Should_Return_Correctly(string input, Command expectedDirection, int expectedValue)
    {
        // ACT
        var result = Input.TryParse(input, out var actual);

        // ASSERT
        result.Should().BeTrue();
        actual.Should().NotBeNull();
        actual?.Direction.Should().Be(expectedDirection);
        actual?.Value.Should().Be(expectedValue);
    }

    [Theory]
    [InlineData("forward")]
    [InlineData("2")]
    [InlineData("down3")]
    [InlineData("forward one")]
    [InlineData("up 2 3")]
    [InlineData("down 3 forward 1")]
    [InlineData("forward 1\nup 2")]
    [InlineData("invalid 1")]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void TryParse_Should_Handle_Invalid_Input(string input)
    {
        // ACT
        var result = Input.TryParse(input, out var actual);

        // ASSERT
        result.Should().BeFalse();
        actual.Should().BeNull();
    }
}