using System.Text.RegularExpressions;
using AdventOfCode.Shared;
using AdventOfCode.Shared.Utilities;

namespace AdventOfCode.App.Y2022.Problems.Day2;

[Problem(Year = 2022, Day = 2)]
public class Problem : ProblemBase<Tuple<Command, Command>>
{
    private static readonly Regex _inlineInputFormat = new Regex(@"\s*(?'P1'[abc])[\s]+(?'P2'[xyz])\s*", RegexOptions.Multiline | RegexOptions.IgnoreCase);

    public Problem() : base(new TextHelper(), new FileHelper())
    { }

    public override string Url => "https://adventofcode.com/2022/day/2";

    public override object Solve(IEnumerable<Tuple<Command, Command>> input, int problemPart)
    {
        var scores = ScoreCommands(input);
        var p2 = scores.Sum(s => s.Item2);
        return p2;
    }

    public override bool IsInlineInput(int part, string input)
    {
        var invalid = _inlineInputFormat.Replace(input, string.Empty);
        return string.IsNullOrWhiteSpace(invalid);
    }

    public override bool LineValueConverter(int part, string? value, out Tuple<Command, Command> converted)
    {
        var result = false;
        var command1 = default(Command);
        var command2 = default(Command);

        if (string.IsNullOrWhiteSpace(value) is false)
        {
            var match = _inlineInputFormat.Match(value);
            if (match.Success)
            {
                var parse1 = TryParseCommand(match.Groups["P1"].Value, out command1);

                var command2String = TranslateCommand(part, match.Groups["P2"].Value);
                if (part == 2)
                {
                    var outcome = default(Outcome);
                    result = parse1 && TryParseOutcome(command2String, out outcome);
                    command2 = CalculateTargetMove(command1, outcome);
                }
                else
                {
                    result = parse1 && TryParseCommand(command2String, out command2);
                }
            }
        }

        converted = new Tuple<Command, Command>(command1, command2);
        return result;
    }

    public override IEnumerable<string> InputValueConverter(int part, string? input)
    {
        if (string.IsNullOrWhiteSpace(input)) { yield break; }

        var match = _inlineInputFormat.Match(input);
        while(match.Success)
        {
            var p1 = match.Groups["P1"].Value;
            var p2 = match.Groups["P2"].Value;
            yield return $"{p1} {p2}";

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
        switch (true)
        {
            case bool _ when string.Equals(value, "A", StringComparison.OrdinalIgnoreCase):
                parsed = Command.Rock;
                return true;

            case bool _ when string.Equals(value, "B", StringComparison.OrdinalIgnoreCase):
                parsed = Command.Paper;
                return true;

            case bool _ when string.Equals(value, "C", StringComparison.OrdinalIgnoreCase):
                parsed = Command.Scissors;
                return true;

            default:
                parsed = default(Command);
                return false;
        };
    }

    public static bool TryParseOutcome(string value, out Outcome parsed)
    {
        switch (true)
        {
            case bool _ when string.Equals(value, "X", StringComparison.OrdinalIgnoreCase):
                parsed = Outcome.Lose;
                return true;

            case bool _ when string.Equals(value, "Y", StringComparison.OrdinalIgnoreCase):
                parsed = Outcome.Draw;
                return true;

            case bool _ when string.Equals(value, "Z", StringComparison.OrdinalIgnoreCase):
                parsed = Outcome.Win;
                return true;

            default:
                parsed = default(Outcome);
                return false;
        };
    }

    public static string TranslateCommand(int part, string value)
    {
        switch (true)
        {
            case bool _ when part == 1 && string.Equals(value, "X", StringComparison.OrdinalIgnoreCase):
                return "A";

            case bool _ when part == 1 && string.Equals(value, "Y", StringComparison.OrdinalIgnoreCase):
                return "B";

            case bool _ when part == 1 && string.Equals(value, "Z", StringComparison.OrdinalIgnoreCase):
                return "C";

            default:
                return value;
        };
    }

    public static Command CalculateTargetMove(Command move, Outcome targetResult)
    {
        switch (move)
        {
            case Command.Rock:
                switch (targetResult)
                {
                    case Outcome.Win:
                        return Command.Paper;
                    case Outcome.Lose:
                        return Command.Scissors;
                    case Outcome.Draw:
                        return move;
                }
                break;

            case Command.Paper:
                switch (targetResult)
                {
                    case Outcome.Win:
                        return Command.Scissors;
                    case Outcome.Lose:
                        return Command.Rock;
                    case Outcome.Draw:
                        return move;
                }
                break;

            case Command.Scissors:
                switch (targetResult)
                {
                    case Outcome.Win:
                        return Command.Rock;
                    case Outcome.Lose:
                        return Command.Paper;
                    case Outcome.Draw:
                        return move;
                }
                break;

            default:
                throw new ArgumentOutOfRangeException(nameof(move), move, "Unsupported value.");
        }

        throw new ArgumentOutOfRangeException(nameof(targetResult), targetResult, "Unsupported value.");
    }

    public static IEnumerable<Tuple<int, int>> ScoreCommands(IEnumerable<Tuple<Command, Command>> commands)
    {
        foreach(var value in commands)
        {
            if (value is null) { continue; }

            yield return ScoreCommand(value);
        }
    }

    public static Tuple<int, int> ScoreCommand(Tuple<Command, Command> value)
    {
        var p1 = value.Item1;
        var p2 = value.Item2;
        var outcome1 = ScoreOutcome(p1, p2);
        var outcome2 = ScoreOutcome(p2, p1);
        var move1 = ScoreMove(p1);
        var move2 = ScoreMove(p2);
        var total1 = outcome1 + move1;
        var total2 = outcome2 + move2;

        return new Tuple<int, int>(total1, total2);
    }

    public static int ScoreMove(Command move)
    {
        switch (move)
        {
            case Command.Rock:
                return 1;
            case Command.Paper:
                return 2;
            case Command.Scissors:
                return 3;
        }

        throw new ArgumentOutOfRangeException(nameof(move), move, "Unsupported value.");
    }

    public static int ScoreOutcome(Command move, Command opponentMove)
    {
        switch (move)
        {
            case Command.Rock:
                switch (opponentMove)
                {
                    case Command.Rock:
                        return 3;
                    case Command.Paper:
                        return 0;
                    case Command.Scissors:
                        return 6;
                }
                break;

            case Command.Paper:
                switch (opponentMove)
                {
                    case Command.Rock:
                        return 6;
                    case Command.Paper:
                        return 3;
                    case Command.Scissors:
                        return 0;
                }
                break;

            case Command.Scissors:
                switch (opponentMove)
                {
                    case Command.Rock:
                        return 0;
                    case Command.Paper:
                        return 6;
                    case Command.Scissors:
                        return 3;
                }
                break;

            default:
                throw new ArgumentOutOfRangeException(nameof(move), move, "Unsupported value.");
        }

        throw new ArgumentOutOfRangeException(nameof(opponentMove), opponentMove, "Unsupported value.");
    }
}