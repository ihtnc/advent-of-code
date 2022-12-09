namespace AdventOfCode.App.Y2022.Problems.Day5;

public class MoveCommand
{
    public int Count { get; set; }
    public int Source { get; set; }
    public int Target { get; set; }

    public static bool TryParse(string input, out MoveCommand parsed)
    {
        parsed = new MoveCommand();

        var match = InputHelper.MoveFormat.Match(input);
        if (match.Success is false)
        {
            return false;
        }

        if (int.TryParse(match.Groups["count"].Value, out var count) is false
            || int.TryParse(match.Groups["source"].Value, out var source) is false
            || int.TryParse(match.Groups["target"].Value, out var target) is false)
        { return false; }

        parsed.Count = count;
        parsed.Source  = source;
        parsed.Target = target;
        return true;
    }
}