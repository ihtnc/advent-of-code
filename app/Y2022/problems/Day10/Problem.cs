using System.Text;
using System.Text.RegularExpressions;
using AdventOfCode.Shared;
using AdventOfCode.Shared.Utilities;

namespace AdventOfCode.App.Y2022.Problems.Day10;

[Problem(Year = 2022, Day = 10)]
public class Problem : ProblemBase<Command>
{
    private static readonly Regex _inlineInputFormat = new Regex(@"\s*\b(?'instruction'noop|addx)(?(?<=addx)\s+(?'data'-?\d+)|\s*)", RegexOptions.Multiline | RegexOptions.IgnoreCase);

    private static readonly Dictionary<InstructionType, Type> _instructionMap = new()
    {
        {InstructionType.noop, typeof(NoopInstruction)},
        {InstructionType.addX, typeof(AddXInstruction)}
    };

    public Problem() : base(new TextHelper(), new FileHelper())
    { }

    public override string Url => "https://adventofcode.com/2022/day/10";

    public override object Solve(IEnumerable<Command> input, int problemPart)
    {
        var instructions = GetInstructions(input);
        var trace = RunInstructions(instructions);

        switch(problemPart)
        {
            case 1:
                var interestingSnapshotIds = new [] {20, 60, 100, 140, 180, 220};
                var sum = CalculateSignalStrengthSum(trace, interestingSnapshotIds);
                return sum;

            case 2:
                var screen = RenderScreen(trace);
                return screen;

            default:
                return $"Part {problemPart} not supported.";
        }
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
            var instruction = match.Groups["instruction"].Value;
            var data = match.Groups["data"].Value;
            yield return $"{instruction} {data}".Trim();

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

        var instruction = match.Groups["instruction"].Value;
        var data = match.Groups["data"].Value;
        var dataValue = 0;

        var isInstructionTypeValid = Enum.TryParse<InstructionType>(instruction, true, out var instructionTypeValue);
        var isInstructionDataValid = instructionTypeValue == InstructionType.addX ? int.TryParse(data, out dataValue) : true;
        if (isInstructionTypeValid is false || isInstructionDataValid is false)
        {
            parsed = new Command();
            return false;
        }

        parsed = CreateCommand(instructionTypeValue, dataValue);
        return true;
    }

    public static Command CreateCommand(InstructionType instructionType, object? data)
    {
        switch(instructionType)
        {
            case InstructionType.noop:
                return new Command
                {
                    CommandInstruction = instructionType,
                    Data = null
                };

            case InstructionType.addX:
                return new Command
                {
                    CommandInstruction = instructionType,
                    Data = data
                };

            default:
                return new Command();
        }
    }

    public static IEnumerable<Instruction> GetInstructions(IEnumerable<Command> commands)
    {
        foreach(var item in commands)
        {
            if (TryGetInstruction(item, out var instruction) is false || instruction is null)
            {
                yield break;
            }

            yield return instruction;
        }
    }

    public static bool TryGetInstruction(Command command, out Instruction? instruction)
    {
        if (_instructionMap.ContainsKey(command.CommandInstruction) is false)
        {
            instruction = default;
            return false;
        }

        var type = _instructionMap[command.CommandInstruction];
        var instance = null as Instruction;

        switch(command.CommandInstruction)
        {
            case InstructionType.noop:
                instance = Activator.CreateInstance(type) as Instruction;
                break;

            case InstructionType.addX:
                instance = Activator.CreateInstance(type, args: command.Data) as Instruction;
                break;
        }

        instruction = instance;
        return true;
    }

    public static IEnumerable<RegisterSnapshot> RunInstructions(IEnumerable<Instruction> instructions)
    {
        var register = new RegisterDetails();

        foreach(var instruction in instructions)
        {
            register.Execute(instruction);
            while(register.IsBusy)
            {
                register.TryWaitOne(out var snapshot);
                yield return snapshot;
            }

            register.TryGetResult(out var result);
            var value = result is int data ? data : 0;
            register.X += value;
        }
    }

    public int? GetSignalStrength(IEnumerable<RegisterSnapshot> history, int snapshotId)
    {
        var snapshot = history.FirstOrDefault(s => s.SnapshotId == snapshotId);
        if (snapshot is null) { return null; }

        return snapshot.SnapshotId * snapshot.X;
    }

    public int CalculateSignalStrengthSum(IEnumerable<RegisterSnapshot> history, params int[] snapshotIds)
    {
        int sum = 0;
        foreach(var id in snapshotIds)
        {
            var value = GetSignalStrength(history, id) ?? 0;
            sum += value;
        }
        return sum;
    }

    public string RenderScreen(IEnumerable<RegisterSnapshot> history)
    {
        var screenWidth = 40;
        var screenHeight = 6;
        var canvas = new char[screenHeight, screenWidth];
        var sprite = new Sprite();

        foreach(var item in history)
        {
            var pixelIndex = item.SnapshotId - 1;
            var row = Math.DivRem(pixelIndex, screenWidth, out var col);
            var pixel = sprite.RenderPixel(screenWidth, screenHeight, item);
            canvas[row, col] = pixel;
        }

        var screen = new StringBuilder();
        for(var i = 0; i < canvas.GetLength(0); i++)
        {
            for(var j = 0; j < canvas.GetLength(1); j++)
            {
                var pixel = canvas[i, j];
                screen.Append(pixel == default ? '.' : pixel);
            }

            screen.AppendLine();
        }
        return $"{screen}";
    }
}
