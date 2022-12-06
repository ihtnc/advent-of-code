using System.Text.RegularExpressions;
using AdventOfCode.Shared;
using AdventOfCode.Shared.Utilities;

namespace AdventOfCode.App.Y2022.Problems.Day3;

[Problem(Year = 2022, Day = 3)]
public class Problem : ProblemBase<string>
{
    private static readonly Regex _inlineInputFormat = new Regex(@"(?'items'[A-Za-z]+)\b");

    public Problem() : base(new TextHelper(), new FileHelper())
    { }

    public override string Url => "https://adventofcode.com/2022/day/3";

    public override object Solve(IEnumerable<string> input, int problemPart)
    {
        long total = 0;
        var groups = GroupInput(problemPart, input);
        foreach (var item in groups)
        {
            var common = GetCommonCharacters(item);
            var priority = CalculatePriority(common);
            total += priority;
        }

        return total;
    }

    public override bool IsInlineInput(int part, string input)
    {
        var invalid = _inlineInputFormat.Replace(input, string.Empty);
        return string.IsNullOrWhiteSpace(invalid);
    }

    public override bool LineValueConverter(int part, string? value, out string converted)
    {
        converted = value ?? string.Empty;
        return true;
    }

    public override IEnumerable<string> InputValueConverter(int part, string? input)
    {
        if (string.IsNullOrWhiteSpace(input)) { yield break; }

        var match = _inlineInputFormat.Match(input);
        while(match.Success)
        {
            yield return match.Groups["items"].Value;

            match = match.NextMatch();
        }
    }

    public override Dictionary<int, Description> ProblemParts =>
        new Dictionary<int, Description>
        {
            {1, new Part1Description()},
            {2, new Part2Description()},
        };

    public static IEnumerable<string[]> GroupInput(int part, IEnumerable<string> input)
    {
        var group = new List<string>();
        foreach(var value in input)
        {
            if (string.IsNullOrWhiteSpace(value)) { continue; }

            if(part == 1)
            {
                var midPoint = value.Length / 2;
                var value1 = string.Concat(value.Take(midPoint));
                var value2 = string.Concat(value.Skip(midPoint));
                yield return new [] { value1, value2 };
            }
            else if (part == 2)
            {
                group.Add(value);
                if (group.Count < 3) { continue; }

                yield return group.ToArray();
                group = new List<string>();
            }
        }

        if (group.Any())
        {
            yield return group.ToArray();
        }
    }


    public static Letters GetCommonCharacters(params string[] values)
    {
        var firstValue = values.Take(1).SingleOrDefault();
        var otherValues = values.Skip(1);
        var commonCharacters = firstValue?.GetUniqueLetters() ?? Letters.Blank;

        foreach(var str in otherValues)
        {
            var current = str.GetUniqueLetters();
            commonCharacters &= current;
        }

        return commonCharacters;
    }

    public static int CalculatePriority(Letters value)
    {
        var total = 0;
        var str = value.GetString();
        foreach(var c in str)
        {
            var offset = 0;

            switch (true)
            {
                case bool _ when char.IsUpper(c): offset = (int)c - (int)'A' + 26; break;
                case bool _ when char.IsLower(c): offset = (int)c - (int)'a'; break;
                default: continue;
            }

            var priority = offset + 1;
            total += priority;
        }

        return total;
    }
}