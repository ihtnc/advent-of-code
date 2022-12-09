using System.Text;
using System.Text.RegularExpressions;
using AdventOfCode.Shared;
using AdventOfCode.Shared.Utilities;

namespace AdventOfCode.App.Y2022.Problems.Day5;

[Problem(Year = 2022, Day = 5)]
public class Problem : ProblemBase<string>
{
    private static readonly Regex _inlineInputFormat = new Regex(@",?\s*(?'value'\d+|\s*)\s*");

    public Problem() : base(new TextHelper(), new FileHelper())
    { }

    public override string Url => "https://adventofcode.com/2022/day/5";

    public override object Solve(IEnumerable<string> input, int problemPart)
    {
        var inputText = string.Join(Environment.NewLine, input);
        var workspace = inputText.ToWorkspace();
        var commands = inputText.ToMoveCommands();

        var preserveOrder = problemPart == 2;
        var result = InputHelper.Rearrange(workspace, commands, preserveOrder);
        var top = result.GetTopItems();
        return string.Concat(top);
    }

    public override bool IsInlineInput(int part, string input) => false;

    public override bool LineValueConverter(int part, string? value, out string converted)
    {
        converted = string.IsNullOrWhiteSpace(value) ? string.Empty : value;
        return true;
    }

    public override IEnumerable<string> InputValueConverter(int part, string? input)
    {
        if (string.IsNullOrWhiteSpace(input)) { yield break; }

        yield return input;
    }

    public override Dictionary<int, Description> ProblemParts =>
        new Dictionary<int, Description>
        {
            {1, new Part1Description()},
            {2, new Part2Description()},
        };
}