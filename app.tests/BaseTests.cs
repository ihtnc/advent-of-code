using System.Linq;
using Xunit;
using FluentAssertions;
using AdventOfCode.Shared;

namespace AdventOfCode.App.Tests;

public abstract class BaseTests<T> where T : IProblem, new()
{
    public BaseTests(int year, int day)
    {
        Year = year;
        Day = day;
    }

    private int Year { get; }
    private int Day { get; }

    [Fact]
    public void Should_Have_Correct_Attribute()
    {
        // ACT
        var attr = typeof(T)
            .GetCustomAttributes(typeof(ProblemAttribute), true)
            .FirstOrDefault() as ProblemAttribute;

        // ASSERT
        attr.Should().NotBeNull();
        attr?.Year.Should().Be(Year);
        attr?.Day.Should().Be(Day);
    }
}