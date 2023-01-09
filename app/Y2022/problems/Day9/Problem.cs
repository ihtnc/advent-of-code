using System.Text.RegularExpressions;
using AdventOfCode.Shared;
using AdventOfCode.Shared.Utilities;

namespace AdventOfCode.App.Y2022.Problems.Day9;

[Problem(Year = 2022, Day = 9)]
public class Problem : ProblemBase<Command>
{
    private static readonly Regex _inlineInputFormat = new Regex(@"\s*\b(?'direction'L|D|R|U)\s+(?'steps'\d+)\b\s*", RegexOptions.Multiline | RegexOptions.IgnoreCase);

    public Problem() : base(new TextHelper(), new FileHelper())
    { }

    public override string Url => "https://adventofcode.com/2022/day/9";

    public override object Solve(IEnumerable<Command> input, int problemPart)
    {
        var tailLocations = new HashSet<Coordinate>();

        var segmentCount = 0;
        switch (problemPart)
        {
            case 1: segmentCount = 1; break;
            case 2: segmentCount = 9; break;
        }

        var rope = new RopeHead();
        var tail = ExtendRope(rope, segmentCount);
        tail.OnLocationChanged += (sender, e) => { tailLocations.Add(e.Value); };

        foreach(var cmd in input)
        {
            rope.Move(cmd);
        }

        return tailLocations.Count;
    }

    public override bool IsInlineInput(int part, string input)
    {
        var invalid = _inlineInputFormat.Replace(input, string.Empty);
        return string.IsNullOrWhiteSpace(invalid);
    }

    public override bool LineValueConverter(int part, string? value, out Command converted)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            converted = new Command();
            return false;
        }

        var result = TryParseCommand(value, out converted);
        return result;
    }

    public override IEnumerable<string> InputValueConverter(int part, string? input)
    {
        if (string.IsNullOrWhiteSpace(input)) { yield break; }

        var match = _inlineInputFormat.Match(input);
        while(match.Success)
        {
            yield return $"{match.Groups["direction"].Value} {match.Groups["steps"].Value}";

            match = match.NextMatch();
        }
    }

    public override Dictionary<int, Description> ProblemParts =>
        new Dictionary<int, Description>
        {
            {1, new Part1Description()},
            {2, new Part2Description()},
        };

    public static bool TryParseCommand(string value, out Command parsed)
    {
        var match = _inlineInputFormat.Match(value);
        if (match.Success is false)
        {
            parsed = new Command();
            return false;
        }

        var direction = match.Groups["direction"].Value;
        var steps = match.Groups["steps"].Value;

        if (Enum.TryParse<Direction>(direction, true, out var directionValue) is false
            || int.TryParse(steps, out var stepsValue) is false)
        {
            parsed = new Command();
            return false;
        }

        parsed = new Command
        {
            CommandDirection = directionValue,
            Steps = stepsValue
        };
        return true;
    }

    public RopeSegment ExtendRope(RopeHead head, int segmentCount)
    {
        RopeSegment tail = head;
        for(var i = 0; i < segmentCount; i++)
        {
            tail = tail.AddChild();
        }
        return tail;
    }
}
