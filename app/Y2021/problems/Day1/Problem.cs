using System.Text.RegularExpressions;
using AdventOfCode.Shared;
using AdventOfCode.Shared.Utilities;

namespace AdventOfCode.App.Y2021.Problems.Day1;

[Problem(Year = 2021, Day = 1)]
public class Problem : IProblem
{
    private static readonly Regex _inlineInputFormat = new Regex(@"(?'value'\d+)\s*");

    private readonly ITextHelper _textHelper;
    private readonly IFileHelper _fileHelper;

    public Problem() : this(new TextHelper(), new FileHelper())
    { }

    public Problem(ITextHelper textHelper, IFileHelper fileHelper)
    {
        _textHelper = textHelper;
        _fileHelper = fileHelper;
    }

    public string Url => "https://adventofcode.com/2021/day/1";

    public object Solve(RunOption option)
    {
        var input = $"{option.Input}";

        var groupSize = 0;
        switch (option?.Part)
        {
            case 1: groupSize = 1; break;
            case 2: groupSize = 3; break;
        }

        IEnumerable<int> values;
        if (IsInlineInput(input))
        {
            values = _textHelper.ParseInput<int>(input, InputValueConverter, LineValueConverter);
        }
        else
        {
            values = _fileHelper.ParseLines<int>(input, LineValueConverter);
        }

        var output = CountValueIncrease(values, groupSize);
        return output;
    }

    public Description GetDescription(DescribeOption option)
    {
        switch (option.Part)
        {
            case 1:
                return new Part1Description();
            case 2:
                return new Part2Description();
            default:
                return new ErrorDescription($"Problem has no part {option.Part}.");
      }
    }

    public static bool IsInlineInput(string input)
    {
        var invalid = _inlineInputFormat.Replace(input, string.Empty);
        return string.IsNullOrWhiteSpace(invalid);
    }

    public static bool LineValueConverter(string? value, out int converted) =>
        int.TryParse(value, out converted);

    public static IEnumerable<string> InputValueConverter(string? input)
    {
        if (string.IsNullOrWhiteSpace(input)) { yield break; }

        var match = _inlineInputFormat.Match(input);
        while(match.Success)
        {
            yield return match.Groups["value"].Value;

            match = match.NextMatch();
        }
    }

    public static int CountValueIncrease(IEnumerable<int> values, int groupSize)
    {
        var groupSum = new Dictionary<int, int>();
        var index = 0;
        var valuesCount = values.Count();

        foreach(var value in values)
        {
            for(var i = 0; i < groupSize; i++)
            {
                var sumIndex = index - i;
                if(sumIndex < 0) { continue; }
                if(sumIndex + groupSize > valuesCount) { continue; }

                if(groupSum.ContainsKey(sumIndex) is false)
                {
                    groupSum.Add(sumIndex, 0);
                }

                groupSum[sumIndex] += value;
            }

            index++;
        }

        var count = 0;
        var previous = null as int?;
        foreach(var sum in groupSum)
        {
            if (previous < sum.Value)
            {
                count++;
            }

            previous = sum.Value;
        }

        return count;
    }
}