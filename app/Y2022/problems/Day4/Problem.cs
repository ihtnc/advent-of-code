using System.Text.RegularExpressions;
using AdventOfCode.Shared;
using AdventOfCode.Shared.Utilities;

namespace AdventOfCode.App.Y2022.Problems.Day4;

[Problem(Year = 2022, Day = 4)]
public class Problem : ProblemBase<Tuple<Range, Range>>
{
    private static readonly Regex _inlineInputFormat = new Regex(@"(?'start1'[\d]+)-(?'end1'[\d]+)\s*,\s*(?'start2'[\d]+)-(?'end2'[\d]+)");

    public Problem() : base(new TextHelper(), new FileHelper())
    { }

    public override string Url => "https://adventofcode.com/2022/day/4";

    public override object Solve(IEnumerable<Tuple<Range, Range>> input, int problemPart)
    {
        var completeOverlapOnly = problemPart == 1;
        var output = CountOverlaps(input, completeOverlapOnly);
        return output;
    }

    public override bool IsInlineInput(int part, string input)
    {
        var invalid = _inlineInputFormat.Replace(input, string.Empty);
        return string.IsNullOrWhiteSpace(invalid);
    }

    public override bool LineValueConverter(int part, string? value, out Tuple<Range, Range> converted)
    {
        converted = new Tuple<Range, Range>(default(Range), default(Range));
        if (string.IsNullOrWhiteSpace(value)) { return false; }

        var match = _inlineInputFormat.Match(value);
        if (match.Success is false) { return false; }

        if (int.TryParse(match.Groups["start1"].Value, out var start1) is false
            || int.TryParse(match.Groups["end1"].Value, out var end1) is false
            || int.TryParse(match.Groups["start2"].Value, out var start2) is false
            || int.TryParse(match.Groups["end2"].Value, out var end2) is false)
        { return false; }

        var range1 = new Range(start1, end1);
        var range2 = new Range(start2, end2);
        converted = new Tuple<Range, Range>(range1, range2);
        return true;
    }

    public override IEnumerable<string> InputValueConverter(int part, string? input)
    {
        if (string.IsNullOrWhiteSpace(input)) { yield break; }

        var match = _inlineInputFormat.Match(input);
        while(match.Success)
        {
            var start1 = match.Groups["start1"].Value;
            var end1 = match.Groups["end1"].Value;
            var start2 = match.Groups["start2"].Value;
            var end2 = match.Groups["end2"].Value;

            yield return $"{start1}-{end1},{start2}-{end2}";
            match = match.NextMatch();
        }
    }

    public override Dictionary<int, Description> ProblemParts =>
        new Dictionary<int, Description>
        {
            {1, new Part1Description()},
            {2, new Part2Description()},
        };

    public static bool IsOverlapping(Range reference, Range value, bool completeOverlapOnly)
    {
        var isStartInRange = IsWithinRange(reference, value.Start);
        var isEndInRange = IsWithinRange(reference, value.End);
        var partialOverlap = isStartInRange || isEndInRange;
        var completeOverlap = isStartInRange && isEndInRange;
        return completeOverlapOnly ? completeOverlap : partialOverlap;
    }

    public static bool IsWithinRange(Range reference, Index value) =>
        value.Value >= reference.Start.Value && value.Value <= reference.End.Value;

    public static int CountOverlaps(IEnumerable<Tuple<Range, Range>> input, bool completeOverlapOnly)
    {
        var count = 0;
        foreach(var value in input)
        {
            if (IsOverlapping(value.Item1, value.Item2, completeOverlapOnly) is false
                && IsOverlapping(value.Item2, value.Item1, completeOverlapOnly) is false)
            {
                continue;
            }

            count++;
        }

        return count;
    }
}