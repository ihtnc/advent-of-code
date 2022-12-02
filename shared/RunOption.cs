using CommandLine;

namespace AdventOfCode.Shared;

[Verb("run", isDefault: true, HelpText = "Runs the corresponding solution to the specified problem.")]
public class RunOption
{
    [Option('y', "year", Required = true, HelpText = "Specifies the solution to the problem for the particular year.")]
    public virtual int Year { get; set; }

    [Option('d', "day", Required = true, HelpText = "Specifies the solution to the problem for the particular day.")]
    public virtual int Day { get; set; }

    [Option('p', "part", Required = true, HelpText = "Specifies the solution to the particular part of the problem.")]
    public virtual int Part { get; set; }

    [Option('i', "input", Required = true, HelpText = "Specifies any additional input parameters required for solving the problem.")]
    public virtual string Input { get; set; } = string.Empty;
}