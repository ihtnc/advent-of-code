using System.Text.RegularExpressions;
using AdventOfCode.Shared;
using AdventOfCode.Shared.Utilities;

namespace AdventOfCode.App.Y2022.Problems.Day1;

[Problem(Year = 2022, Day = 1)]
public class Problem : ProblemBase<int?>
{
    private static readonly Regex _inlineInputFormat = new Regex(@",?\s*(?'value'\d+|\s*)\s*");

    public Problem() : base(new TextHelper(), new FileHelper())
    { }

    public override string Url => "https://adventofcode.com/2022/day/1";

    public override object Solve(IEnumerable<int?> input, int problemPart)
    {
        var group = GroupItems(input);
        var groupSum = CalculateSum(group);
        var ordered = Sort(groupSum);

        var groupCount = 0;
        switch(problemPart)
        {
            case 1: groupCount = 1; break;
            case 2: groupCount = 3; break;
        }
        var topGroups = ordered.Take(groupCount);
        var output = CalculateSum(topGroups);
        return output;
    }

    public override bool IsInlineInput(int part, string input)
    {
        var invalid = _inlineInputFormat.Replace(input, string.Empty);
        return string.IsNullOrWhiteSpace(invalid);
    }

    public override bool LineValueConverter(int part, string? value, out int? converted)
    {
        var result = true;
        if (string.IsNullOrWhiteSpace(value))
        {
            converted = null;
        }
        else
        {
            result = int.TryParse(value, out var parsed);
            converted = parsed;
        }

        return result;
    }

    public override IEnumerable<string> InputValueConverter(int part, string? input)
    {
        if (string.IsNullOrWhiteSpace(input)) { yield break; }

        var match = _inlineInputFormat.Match(input);
        while(match.Success)
        {
            yield return match.Groups["value"].Value;

            match = match.NextMatch();
        }
    }

    public override Dictionary<int, Description> ProblemParts =>
        new Dictionary<int, Description>
        {
            {1, new Part1Description()},
            {2, new Part2Description()},
        };

    public static IEnumerable<IEnumerable<int>> GroupItems(IEnumerable<int?> values)
    {
        var group = new List<int>();

        foreach(var item in values)
        {
            if (item.HasValue is false && group.Count == 0) { continue; }
            if (item.HasValue is false)
            {
                var result = group;
                group = new List<int>();
                yield return result;
            }

            group.Add(item.GetValueOrDefault());
        }
    }

    public static IEnumerable<long> Sort(IEnumerable<long> values) =>
        values.OrderByDescending(v => v);

    public static IEnumerable<long> CalculateSum(IEnumerable<IEnumerable<int>> groups) =>
        groups.Select(CalculateSum);
    public static long CalculateSum(IEnumerable<int> values) =>
        values.Sum(v => (long)v);

    public static long CalculateSum(IEnumerable<long> values) =>
        values.Sum();
}