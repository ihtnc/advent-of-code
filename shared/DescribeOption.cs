using CommandLine;

namespace AdventOfCode.Shared;

[Verb("desc", HelpText = "Describes the specified problem.")]
public class DescribeOption
{
    [Option('y', "year", Required = true, HelpText = "Specifies the problem for the particular year.")]
    public virtual int Year { get; set; }

    [Option('d', "day", Required = true, HelpText = "Specifies the problem for the particular day.")]
    public virtual int Day { get; set; }

    [Option('p', "part", Required = true, HelpText = "Specifies the particular part of the problem.")]
    public virtual int Part { get; set; }
}