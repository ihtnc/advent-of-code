using System.Text.RegularExpressions;
using AdventOfCode.Shared;
using AdventOfCode.Shared.Utilities;

namespace AdventOfCode.App.Y2021.Problems.Day2;

[Problem(Year = 2021, Day = 2)]
public class Problem : IProblem
{
    private static readonly Regex _inlineInputFormat = new Regex(Input.INPUT_FORMAT, RegexOptions.IgnoreCase);

    private readonly ITextHelper _textHelper;
    private readonly IFileHelper _fileHelper;

    public Problem() : this(new TextHelper(), new FileHelper())
    { }

    public Problem(ITextHelper textHelper, IFileHelper filehelper)
    {
        _textHelper = textHelper;
        _fileHelper = filehelper;
    }

    public string Url => "https://adventofcode.com/2021/day/2";

    public object Solve(RunOption option)
    {
        var input = $"{option.Input}";

        bool depthUsesAim;
        switch (option?.Part)
        {
            case 1: depthUsesAim = true; break;
            case 2: depthUsesAim = false; break;
            default: return 0;
        }

        IEnumerable<Input?> values;
        if (IsInlineInput(input))
        {
            values = _textHelper.ParseInput<Input?>(input, InputValueConverter, LineValueConverter);
        }
        else
        {
            values = _fileHelper.ParseLines<Input?>(input, LineValueConverter);
        }

        var output = Calculate(values, depthUsesAim);
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

    public static bool LineValueConverter(string? value, out Input? converted) =>
        Input.TryParse(value, out converted);

    public static IEnumerable<string> InputValueConverter(string? input)
    {
        if (string.IsNullOrWhiteSpace(input)) { yield break; }

        var match = _inlineInputFormat.Match(input);
        while(match.Success)
        {
            var command =  match.Groups["command"].Value;
            var value =  match.Groups["value"].Value;
            yield return $"{command} {value}";

            match = match.NextMatch();
        }
    }

    public static int Calculate(IEnumerable<Input?> values, bool depthUsesAim)
    {
        var depth = 0;
        var position = 0;
        var aim = 0;

        foreach(var value in values)
        {
            switch (value?.Direction)
            {
                case Command.Forward:
                    position += value.Value;
                    depth += aim * value.Value;
                    break;

                case Command.Up:
                    aim -= value.Value;
                    break;

                case Command.Down:
                    aim += value.Value;
                    break;
            }
        }

        var depthValue = (depthUsesAim ? aim : depth);
        return depthValue * position;
    }
}