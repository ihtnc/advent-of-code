using System.Text.RegularExpressions;

namespace AdventOfCode.App.Y2021.Problems;

public class Input
{
    public const string INPUT_FORMAT = @"(?'command'forward|down|up)\s+(?'value'\d+)";
    private static readonly Regex inputFormat = new Regex(@$"^\s*{INPUT_FORMAT}\s*$", RegexOptions.IgnoreCase);

    public Command Direction { get; set; }
    public int Value { get; set; }

    public static bool TryParse(string? input, out Input? converted)
    {
        converted = null;

        if (string.IsNullOrWhiteSpace(input)) { return false; }

        var match = inputFormat.Match(input);
        if (match.Success is false) { return false; }

        var direction = Enum.Parse<Command>(match.Groups["command"].Value, true);
        var value = int.Parse(match.Groups["value"].Value);

        converted = new Input
        {
            Direction = direction,
            Value = value
        };

        return true;
    }
}