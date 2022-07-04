namespace AdventOfCode.Shared;

[AttributeUsage(AttributeTargets.Class)]
public class ProblemAttribute : Attribute
{
    public int Year { get; set; }
    public int Day { get; set; }
}