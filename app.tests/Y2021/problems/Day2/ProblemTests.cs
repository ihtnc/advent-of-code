using Xunit;
using FluentAssertions;
using AdventOfCode.App.Y2021.Problems.Day2;
using AdventOfCode.Shared.Utilities;
using NSubstitute;
using AdventOfCode.Shared;
using AdventOfCode.App.Y2021.Problems;

namespace AdventOfCode.App.Tests.Y2021.Problems.Day2;

public class ProblemTests : BaseTests<Problem>
{
    private readonly ITextHelper _textHelper;
    private readonly IFileHelper _fileHelper;

    private readonly Problem _problem;

    public ProblemTests() : base(2021, 2)
    {
        _textHelper = Substitute.For<ITextHelper>();
        _fileHelper = Substitute.For<IFileHelper>();

        _problem = new Problem(_textHelper, _fileHelper);
    }

    [Theory]
    [InlineData("forward 1", Command.Forward, 1)]
    [InlineData("up 2", Command.Up, 2)]
    [InlineData("down 3", Command.Down, 3)]
    public void LineValueConverter_Should_Return_Correctly(string value, Command expectedDirection, int expectedValue)
    {
        // ACT
        var actualResult = Problem.LineValueConverter(value, out var actualValue);

        // ARRANGE
        actualResult.Should().BeTrue();
        actualValue.Should().NotBeNull();
        actualValue?.Direction.Should().Be(expectedDirection);
        actualValue?.Value.Should().Be(expectedValue);
    }

    [Fact]
    public void LineValueConverter_Should_Handle_Invalid_Value()
    {
        // ACT
        var actualResult = Problem.LineValueConverter(null, out var actualValue);

        // ARRANGE
        actualResult.Should().BeFalse();
        actualValue.Should().BeNull();
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    [InlineData("filePath")]
    public void InputValueConverter_Should_Handle_Invalid_Input(string input)
    {
        // ACT
        var actual = Problem.InputValueConverter(input);

        // ASSERT
        actual.Should().BeEmpty();
    }

    [Theory]
    [InlineData("forward 1 up 2 down 3", new [] {"forward 1", "up 2", "down 3"})]
    [InlineData("forward 1\nup 2\ndown 3", new [] {"forward 1", "up 2", "down 3"})]
    [InlineData("   forward 1\nup 2 down 3", new [] {"forward 1", "up 2", "down 3"})]
    [InlineData("forward 1 up 2 down 3 up four", new [] {"forward 1", "up 2", "down 3"})]
    [InlineData("forward 1 up 2 down 3 up4 forward 5", new [] {"forward 1", "up 2", "down 3", "forward 5"})]
    [InlineData("1 up 2 down 3", new [] {"up 2", "down 3"})]
    public void InputValueConverter_Should_Return_Correctly(string input, string[] expectedValues)
    {
        // ACT
        var actual = Problem.InputValueConverter(input);

        // ASSERT
        actual.Should().BeEquivalentTo(expectedValues);
    }

    [Theory]
    [InlineData(true, 150)]
    [InlineData(false, 900)]
    public void Calculate_Should_Return_Correctly(bool depthUsesAim, int expectedValue)
    {
        // ARRANGE
        var values = new []
        {
            new Input { Direction = Command.Forward, Value = 5 },
            new Input { Direction = Command.Down, Value = 5 },
            new Input { Direction = Command.Forward, Value = 8 },
            new Input { Direction = Command.Up, Value = 3 },
            new Input { Direction = Command.Down, Value = 8 },
            new Input { Direction = Command.Forward, Value = 2 }
        };

        // ACT
        var actual = Problem.Calculate(values, depthUsesAim);

        // ASSERT
        actual.Should().Be(expectedValue);
    }

    [Fact]
    public void Solve_Should_Call_FileHelper_ParseLines_If_Input_Is_Not_Inline()
    {
        // ARRANGE
        var part = 1;
        var path = "filePath";
        var option = new RunOption
        {
            Input = path,
            Part = part
        };

        // ACT
        _problem.Solve(option);

        // ASSERT
        _textHelper.Received(0).ParseInput<Input?>(Arg.Any<string>(), Arg.Any<InputConverterDelegate>(), Arg.Any<ValueConverterDelegate<Input?>>());
        _fileHelper.Received(1).ParseLines<Input?>(path, Arg.Any<ValueConverterDelegate<Input?>>());
    }

    [Fact]
    public void Solve_Should_Call_TextHelper_ParseInput_If_Input_Is_Inline()
    {
        // ARRANGE
        var part = 1;
        var input = "forward 1";
        var option = new RunOption
        {
            Input = input,
            Part = part
        };

        // ACT
        _problem.Solve(option);

        // ASSERT
        _textHelper.Received(1).ParseInput<Input?>(input, Arg.Any<InputConverterDelegate>(), Arg.Any<ValueConverterDelegate<Input?>>());
        _fileHelper.Received(0).ParseLines<Input?>(Arg.Any<string>(), Arg.Any<ValueConverterDelegate<Input?>>());
    }

    [Fact]
    public void Solve_Should_Handle_Invalid_Part()
    {
        // ARRANGE
        var expected = 0;
        var part = 3;
        var path = "filePath";
        var option = new RunOption
        {
            Input = path,
            Part = part
        };

        // ACT
        var actual = _problem.Solve(option);

        // ASSERT
        actual.Should().Be(expected);
        _textHelper.Received(0).ParseInput<Input?>(Arg.Any<string>(), Arg.Any<InputConverterDelegate>(), Arg.Any<ValueConverterDelegate<Input?>>());
        _fileHelper.Received(0).ParseLines<Input?>(Arg.Any<string>(), Arg.Any<ValueConverterDelegate<Input?>>());
    }

    [Theory]
    [InlineData(1, 8)]
    [InlineData(2, 24)]
    public void Solve_Should_Return_Correctly_For_FileInput(int part, int expectedValue)
    {
        // ARRANGE
        var path = "filePath";
        var option = new RunOption
        {
            Part = part,
            Input = path
        };

        var values = new []
        {
            new Input { Direction = Command.Forward, Value = 1 },
            new Input { Direction = Command.Down, Value = 2 },
            new Input { Direction = Command.Forward, Value = 3 }
        };
        _fileHelper.ParseLines<Input?>(path, Arg.Any<ValueConverterDelegate<Input?>>())
            .Returns(values);

        // ACT
        var actual = _problem.Solve(option);

        // ASSERT
        actual.Should().Be(expectedValue);
    }

    [Theory]
    [InlineData(1, 8)]
    [InlineData(2, 24)]
    public void Solve_Should_Return_Correctly_For_InlineInput(int part, int expectedValue)
    {
        // ARRANGE
        var input = "forward 1";
        var option = new RunOption
        {
            Part = part,
            Input = input
        };

        var values = new []
        {
            new Input { Direction = Command.Forward, Value = 1 },
            new Input { Direction = Command.Down, Value = 2 },
            new Input { Direction = Command.Forward, Value = 3 }
        };
        _textHelper.ParseInput<Input?>(input, Arg.Any<InputConverterDelegate>(), Arg.Any<ValueConverterDelegate<Input?>>())
            .Returns(values);

        // ACT
        var actual = _problem.Solve(option);

        // ASSERT
        actual.Should().Be(expectedValue);
    }

    [Fact]
    public void Url_Should_Have_Correct_Value()
    {
        // ARRANGE
        var url = "https://adventofcode.com/2021/day/2";

        // ASSERT
        _problem.Url.Should().Be(url);
    }

    [Fact]
    public void GetDescription_Should_Support_Part1()
    {
        // ARRANGE
        var part = 1;
        var option = new DescribeOption
        {
            Part = part
        };

        // ACT
        var description = _problem.GetDescription(option);

        // ASSERT
        description.Should().BeOfType<Part1Description>();
    }

    [Fact]
    public void GetDescription_Should_Support_Part2()
    {
        // ARRANGE
        var part = 2;
        var option = new DescribeOption
        {
            Part = part
        };

        // ACT
        var description = _problem.GetDescription(option);

        // ASSERT
        description.Should().BeOfType<Part2Description>();
    }

    [Fact]
    public void GetDescription_Should_Handle_Invalid_Values()
    {
        // ARRANGE
        var part = 0;
        var option = new DescribeOption
        {
            Part = part
        };
        var expectedMessage = $"Problem has no part {part}.";

        // ACT
        var description = _problem.GetDescription(option);

        // ASSERT
        description.Should().BeOfType<ErrorDescription>();
        description.Text.Should().Be(expectedMessage);
    }

    [Theory]
    [InlineData("forward 1", true)]
    [InlineData("forward 1\nup 2\ndown 3", true)]
    [InlineData("forward 1\n\nup 2\ndown 3\n", true)]
    [InlineData("\nforward 1\t\nup 2   \ndown 3", true)]
    [InlineData("  forward 1\nup 2\ndown 3", true)]
    [InlineData("forward 1 up 2\ndown 3", true)]
    [InlineData("filePath", false)]
    [InlineData("invalid 1", false)]
    [InlineData("forward 1 invalid 2", false)]
    [InlineData("invalid 1 up 2", false)]
    public void IsInlineInput_Should_Return_Correctly(string input, bool expected)
    {
        // ACT
        var actual = Problem.IsInlineInput(input);

        // ASSERT
        actual.Should().Be(expected);
    }
}