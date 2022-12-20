using System.Text.RegularExpressions;
using AdventOfCode.Shared;
using AdventOfCode.Shared.Utilities;

namespace AdventOfCode.App.Y2022.Problems.Day8;

[Problem(Year = 2022, Day = 8)]
public class Problem : ProblemBase<string>
{
    private static readonly Regex _inlineInputFormat = new Regex(@"\b\d+\b");

    public Problem() : base(new TextHelper(), new FileHelper())
    { }

    public override string Url => "https://adventofcode.com/2022/day/8";

    public override object Solve(IEnumerable<string> input, int problemPart)
    {
        var values = MapHelper.CreateValueMap(input);

        switch (problemPart)
        {
            case 1:
                var visibilityMap = MapHelper.CreateVisibilityMap(values);
                var count = CountVisibleItems(visibilityMap);
                return count;

            case 2:
                var scoreMap = MapHelper.CreateScenicScoreMap(values);
                var highest = FindHighestValue(scoreMap);
                return highest;

             default:
                return $"Part {problemPart} not supported.";
        }
    }

    public override bool IsInlineInput(int part, string input)
    {
        var invalid = _inlineInputFormat.Replace(input, string.Empty);
        return string.IsNullOrWhiteSpace(invalid);
    }

    public override bool LineValueConverter(int part, string? value, out string converted)
    {
        converted = string.IsNullOrWhiteSpace(value) ? string.Empty : value;
        return true;
    }

    public override IEnumerable<string> InputValueConverter(int part, string? input)
    {
        if (string.IsNullOrWhiteSpace(input)) { yield break; }

        var match = _inlineInputFormat.Match(input);
        while(match.Success)
        {
            yield return match.Value;

            match = match.NextMatch();
        }
    }

    public override Dictionary<int, Description> ProblemParts =>
        new Dictionary<int, Description>
        {
            {1, new Part1Description()},
            {2, new Part2Description()},
        };

    public static int CountVisibleItems(bool[,] map)
    {
        var visible = 0;

        var rowCount = map.GetLength(0);
        var colCount = map.GetLength(1);
        for(var i = 0; i < rowCount; i++)
        {
            for(var j = 0; j < colCount; j++)
            {
                if (map[i, j] is true) { visible++; }
            }
        }

        return visible;
    }

    public static int FindHighestValue(int[,] map)
    {
        var highest = 0;

        var rowCount = map.GetLength(0);
        var colCount = map.GetLength(1);
        for(var i = 0; i < rowCount; i++)
        {
            for(var j = 0; j < colCount; j++)
            {
                highest = map[i,j] > highest ? map[i,j] : highest;
            }
        }

        return highest;
    }
}