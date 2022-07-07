using Xunit;
using FluentAssertions;
using System;
using System.Linq;
using AdventOfCode.App.Y2021.Problems.Day3;
using AdventOfCode.Shared.Utilities;
using NSubstitute;
using AdventOfCode.Shared;

namespace AdventOfCode.App.Tests.Y2021.Problems.Day3;

public class ProblemTests : BaseTests<Problem>
{
    private readonly ITextHelper _textHelper;
    private readonly IFileHelper _fileHelper;

    private readonly Problem _problem;

    public ProblemTests() : base(2021, 3)
    {
        _textHelper = Substitute.For<ITextHelper>();
        _fileHelper = Substitute.For<IFileHelper>();

        _problem = new Problem(_textHelper, _fileHelper);
    }

    [Fact]
    public void LineValueConverter_Should_Return_Correctly()
    {
        // ARRANGE
        var value = "1001";
        var expectedValue = "1001";

        // ACT
        var actualResult = Problem.LineValueConverter(value, out var actualValue);

        // ARRANGE
        actualResult.Should().BeTrue();
        actualValue.Should().Be(expectedValue);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    [InlineData("invalid")]
    [InlineData("102")]
    public void LineValueConverter_Should_Handle_Invalid_Value(string value)
    {
        // ACT
        var actualResult = Problem.LineValueConverter(value, out var actualValue);

        // ARRANGE
        actualResult.Should().BeFalse();
        actualValue.Should().BeEmpty();
    }

    [Theory]
    [InlineData("101 000 111", new [] {"101", "000", "111"})]
    [InlineData("101\n000\n111", new [] {"101", "000", "111"})]
    [InlineData("   101\n000 111", new [] {"101", "000", "111"})]
    [InlineData("101 000 111 102", new [] {"101", "000", "111"})]
    [InlineData("101 000 111 201 010", new [] {"101", "000", "111", "010"})]
    [InlineData("021 000 111", new [] {"000", "111"})]
    public void InputValueConverter_Should_Return_Correctly(string input, string[] expectedValues)
    {
        // ACT
        var actual = Problem.InputValueConverter(input);

        // ASSERT
        actual.Should().BeEquivalentTo(expectedValues);
    }

    [Theory]
    [InlineData(new [] {"1", "1", "1"}, 0)]
    [InlineData(new [] {"0", "0", "0"}, 0)]
    [InlineData(new [] {"100", "001", "110"}, 12)]
    public void GetPowerConsumption_Should_Return_Correctly(string[] input, long expected)
    {
        // ARRANGE
        var values = input.Select(i => Convert.ToInt32($"{i}", 2));

        // ACT
        var actual = Problem.GetPowerConsumption(values);

        // ASSERT
        actual.Should().Be(expected);
    }

    [Theory]
    [InlineData(new [] {"1", "1", "1"}, 1)]
    [InlineData(new [] {"0", "0", "0"}, 0)]
    [InlineData(new [] {"100", "001", "110"}, 6)]
    public void GetOxygenGeneratorRating_Should_Return_Correctly(string[] input, int expected)
    {
        // ARRANGE
        var values = input.Select(i => Convert.ToInt32($"{i}", 2));

        // ACT
        var actual = Problem.GetOxygenGeneratorRating(values);

        // ASSERT
        actual.Should().Be(expected);
    }

    [Theory]
    [InlineData(new [] {"1", "1", "1"}, 0)]
    [InlineData(new [] {"0", "0", "0"}, 0)]
    [InlineData(new [] {"100", "001", "110"}, 1)]
    public void GetCarbonDioxideScrubberRating_Should_Return_Correctly(string[] input, int expected)
    {
        // ARRANGE
        var values = input.Select(i => Convert.ToInt32($"{i}", 2));

        // ACT
        var actual = Problem.GetCarbonDioxideScrubberRating(values);

        // ASSERT
        actual.Should().Be(expected);
    }

    [Fact]
    public void Url_Should_Have_Correct_Value()
    {
        // ARRANGE
        var url = "https://adventofcode.com/2021/day/3";

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
    [InlineData("1001", true)]
    [InlineData("101\n000\n111", true)]
    [InlineData("101\n\n000\n111\n", true)]
    [InlineData("\n101\t\n000   \n111", true)]
    [InlineData("  101\n000\n111", true)]
    [InlineData("101 000\n111", true)]
    [InlineData("filePath", false)]
    [InlineData("101 102", false)]
    [InlineData("201", false)]
    [InlineData("121", false)]
    public void IsInlineInput_Should_Return_Correctly(string input, bool expected)
    {
        // ACT
        var actual = Problem.IsInlineInput(input);

        // ASSERT
        actual.Should().Be(expected);
    }
}