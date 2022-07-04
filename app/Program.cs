using CommandLine;
using AdventOfCode.Shared;

namespace AdventOfCode.App;

public class Program
{
    public static void Main(string[] args)
    {
        Parser.Default
            .ParseArguments<RunOption, DescribeOption>(args)
            .WithParsed<RunOption>(o => {
                var runner = new SolutionRunner();
                var output = runner.Run(o);
                Console.WriteLine(output);
            })
            .WithParsed<DescribeOption>(o => {
                var runner = new SolutionRunner();
                var output = runner.Describe(o);
                Console.WriteLine(output);
            });
    }
}