using AdventOfCode.Shared;
using AdventOfCode.Shared.Utilities;

namespace AdventOfCode.App.Y2022.Problems.Day6;

[Problem(Year = 2022, Day = 6)]
public class Problem : ProblemBase<string>
{
    public Problem() : base(new TextHelper(), new FileHelper())
    { }

    public override string Url => "https://adventofcode.com/2022/day/6";

    public override object Solve(IEnumerable<string> input, int problemPart)
    {
        var uniqueCount = problemPart == 1 ? 4 : 14;
        var value = string.Concat(input);
        var reader = UniqueStringReader.Create(uniqueCount);

        foreach(var c in value)
        {
            if (reader.Add(c) is false) { break; }
        }

        return reader.UniqueTextIndex + uniqueCount;
    }

    public override bool IsInlineInput(int part, string input)
    {
        return _fileHelper.FileExists(input) is false;
    }

    public override bool LineValueConverter(int part, string? value, out string converted)
    {
        var result = true;
        converted = string.IsNullOrWhiteSpace(value) ? string.Empty : value;
        return result;
    }

    public override IEnumerable<string> InputValueConverter(int part, string? input)
    {
        yield return string.IsNullOrWhiteSpace(input) ? string.Empty : input;
    }

    public override Dictionary<int, Description> ProblemParts =>
        new Dictionary<int, Description>
        {
            {1, new Part1Description()},
            {2, new Part2Description()},
        };
}