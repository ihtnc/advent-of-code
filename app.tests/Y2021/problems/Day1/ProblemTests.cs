using Xunit;
using FluentAssertions;
using AdventOfCode.App.Y2021.Problems.Day1;
using AdventOfCode.Shared.Utilities;
using NSubstitute;
using AdventOfCode.Shared;

namespace AdventOfCode.App.Tests.Y2021.Problems.Day1;

public class ProblemTests : BaseTests<Problem>
{
    private readonly ITextHelper _textHelper;
    private readonly IFileHelper _fileHelper;

    private readonly Problem _problem;

    public ProblemTests() : base(2021, 1)
    {
        _textHelper = Substitute.For<ITextHelper>();
        _fileHelper = Substitute.For<IFileHelper>();

        _problem = new Problem(_textHelper, _fileHelper);
    }

    [Theory]
    [InlineData(new [] {1, 1, 1}, 0)]
    [InlineData(new [] {1, 1, 2}, 1)]
    [InlineData(new [] {1, 2, 1}, 1)]
    [InlineData(new [] {2, 1, 1}, 0)]
    [InlineData(new [] {1, 2, 2}, 1)]
    [InlineData(new [] {1, 2, 3}, 2)]
    [InlineData(new [] {1, 3, 3}, 1)]
    [InlineData(new [] {1, 3, 2}, 1)]
    [InlineData(new [] {2, 1, 2}, 1)]
    [InlineData(new [] {2, 1, 3}, 1)]
    [InlineData(new [] {2, 2, 1}, 0)]
    [InlineData(new [] {2, 3, 1}, 1)]
    [InlineData(new [] {3, 1, 1}, 0)]
    [InlineData(new [] {3, 1, 2}, 1)]
    [InlineData(new [] {3, 1, 3}, 1)]
    [InlineData(new [] {3, 2, 1}, 0)]
    [InlineData(new [] {3, 3, 1}, 0)]
    [InlineData(new [] {3, 3, 2}, 0)]
    public void CountValueIncrease_Should_Count_Increase_In_Previous_Value(int[] values, int expectedCount)
    {
        // ACT
        var actual = Problem.CountValueIncrease(values, 1);

        // ASSERT
        actual.Should().Be(expectedCount);
    }

    [Theory]
    [InlineData(new [] {1, 1, 2, 2}, 2)]
    [InlineData(new [] {1, 1, 2, 0}, 1)]
    [InlineData(new [] {1, 1, 2, 1}, 1)]
    [InlineData(new [] {1, 1, 1, 2}, 1)]
    [InlineData(new [] {1, 1, 1, 1}, 0)]
    [InlineData(new [] {1, 1, 1, 0}, 0)]
    [InlineData(new [] {2, 1, 1, 1}, 0)]
    [InlineData(new [] {2, 1, 1, 0}, 0)]
    [InlineData(new [] {2, 1, 0, 2}, 1)]
    [InlineData(new [] {2, 1, 0, 1}, 0)]
    [InlineData(new [] {3, 0, 1, 1}, 1)]
    [InlineData(new [] {3, 0, 1, 2}, 1)]
    public void CountValueIncrease_Should_Count_Increase_In_Previous_Group_Value(int[] values, int expectedCount)
    {
        // ACT
        var actual = Problem.CountValueIncrease(values, 2);

        // ASSERT
        actual.Should().Be(expectedCount);
    }

    [Theory]
    [InlineData(new [] {1}, 1)]
    [InlineData(new [] {1, 2}, 2)]
    [InlineData(new [] {1, 2, 3}, 3)]
    [InlineData(new [] {1, 2, 3, 4}, 4)]
    public void CountValueIncrease_Should_Handle_Single_Group(int[] values, int groupSize)
    {
        // ARRANGE
        var expected = 0;

        // ACT
        var actual = Problem.CountValueIncrease(values, groupSize);

        // ASSERT
        actual.Should().Be(expected);
    }

    [Theory]
    [InlineData(new int[] {}, 1)]
    [InlineData(new [] {1}, 0)]
    [InlineData(new [] {1}, -1)]
    public void CountValueIncrease_Should_Handle_Invalid_Parameters(int[] values, int groupSize)
    {
        // ARRANGE
        var expected = 0;

        // ACT
        var actual = Problem.CountValueIncrease(values, groupSize);

        // ASSERT
        actual.Should().Be(expected);
    }

    [Theory]
    [InlineData("1", true, 1)]
    [InlineData("0", true, 0)]
    [InlineData("1234", true, 1234)]
    [InlineData("", false, default(int))]
    [InlineData("   ", false, default(int))]
    [InlineData(null, false, default(int))]
    [InlineData("string", false, default(int))]
    [InlineData("1.1", false, 0)]
    [InlineData("1,000", false, 0)]
    public void LineValueConverter_Should_Return_Correctly(string value, bool result, int expected)
    {
        // ACT
        var actualResult = Problem.LineValueConverter(value, out var actualValue);

        // ARRANGE
        actualResult.Should().Be(result);
        actualValue.Should().Be(expected);
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
    [InlineData("1 2 3", new [] {"1", "2", "3"})]
    [InlineData("1\n2\n3", new [] {"1", "2", "3"})]
    [InlineData("   1\n2 3", new [] {"1", "2", "3"})]
    [InlineData("1 2 3 four", new [] {"1", "2", "3"})]
    [InlineData("1 2 3 four 5", new [] {"1", "2", "3", "5"})]
    [InlineData("one 2 3", new [] {"2", "3"})]
    public void InputValueConverter_Should_Return_Correctly(string input, string[] expectedValues)
    {
        // ACT
        var actual = Problem.InputValueConverter(input);

        // ASSERT
        actual.Should().BeEquivalentTo(expectedValues);
    }

    [Fact]
    public void Solve_Should_Call_FileHelper_ParseLines_If_Input_Is_Not_Inline()
    {
        // ARRANGE
        var path = "filePath";
        var option = new RunOption
        {
            Input = path
        };

        // ACT
        var actual = _problem.Solve(option);

        // ASSERT
        _textHelper.Received(0).ParseInput<int>(Arg.Any<string>(), Arg.Any<InputConverterDelegate>(), Arg.Any<ValueConverterDelegate<int>>());
        _fileHelper.Received(1).ParseLines<int>(path, Arg.Any<ValueConverterDelegate<int>>());
    }

    [Fact]
    public void Solve_Should_Call_TextHelper_ParseInput_If_Input_Is_Inline()
    {
        // ARRANGE
        var input = "1 2 3";
        var option = new RunOption
        {
            Input = input
        };

        // ACT
        var actual = _problem.Solve(option);

        // ASSERT
        _textHelper.Received(1).ParseInput<int>(input, Arg.Any<InputConverterDelegate>(), Arg.Any<ValueConverterDelegate<int>>());
        _fileHelper.Received(0).ParseLines<int>(Arg.Any<string>(), Arg.Any<ValueConverterDelegate<int>>());
    }

    [Theory]
    [InlineData(new [] {1,1,1,2,2}, 1, 1)]
    [InlineData(new [] {1,1,1,2,2}, 2, 2)]
    public void Solve_Should_Return_Correctly_For_FileInput(int[] values, int part, int expectedCount)
    {
        // ARRANGE
        var filePath = "filePath";
        var option = new RunOption
        {
            Part = part,
            Input = filePath
        };

        _fileHelper.ParseLines<int>(filePath, Arg.Any<ValueConverterDelegate<int>>())
            .Returns(values);

        // ACT
        var actual = _problem.Solve(option);

        // ASSERT
        actual.Should().Be(expectedCount);
    }

    [Theory]
    [InlineData(new [] {1,1,1,2,2}, 1, 1)]
    [InlineData(new [] {1,1,1,2,2}, 2, 2)]
    public void Solve_Should_Return_Correctly_For_InlineInput(int[] values, int part, int expectedCount)
    {
        // ARRANGE
        var input = "1 2 3";
        var option = new RunOption
        {
            Part = part,
            Input = input
        };

        _textHelper.ParseInput<int>(input, Arg.Any<InputConverterDelegate>(), Arg.Any<ValueConverterDelegate<int>>())
            .Returns(values);

        // ACT
        var actual = _problem.Solve(option);

        // ASSERT
        actual.Should().Be(expectedCount);
    }

    [Fact]
    public void Url_Should_Have_Correct_Value()
    {
        // ARRANGE
        var url = "https://adventofcode.com/2021/day/1";

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
    [InlineData("1 2 3", true)]
    [InlineData("1\n2\n3", true)]
    [InlineData("1\n\n2\n3\n", true)]
    [InlineData("\n1\t\n2   \n3", true)]
    [InlineData("  1\n2\n3", true)]
    [InlineData("1 2\n3", true)]
    [InlineData("filePath", false)]
    public void IsInlineInput_Should_Return_Correctly(string input, bool expected)
    {
        // ACT
        var actual = Problem.IsInlineInput(input);

        // ASSERT
        actual.Should().Be(expected);
    }
}