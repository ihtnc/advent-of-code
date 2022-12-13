using System.Text.RegularExpressions;
using AdventOfCode.Shared;
using AdventOfCode.Shared.Utilities;

namespace AdventOfCode.App.Y2022.Problems.Day7;

[Problem(Year = 2022, Day = 7)]
public class Problem : ProblemBase<string>
{
    public Problem() : base(new TextHelper(), new FileHelper())
    { }

    public override string Url => "https://adventofcode.com/2022/day/7";

    public override object Solve(IEnumerable<string> input, int problemPart)
    {
        var commands = CommandParser.Parse(input);
        var root = FileSystemModeler.RebuildFileSystem(commands);

        switch(problemPart)
        {
            case 1:
                return SolvePart1(root);
            case 2:
                return SolvePart2(root);
            default:
                return $"Part {problemPart} not supported.";
        }
    }

    public override bool IsInlineInput(int part, string input) => false;

    public override bool LineValueConverter(int part, string? value, out string converted)
    {
        converted = string.IsNullOrWhiteSpace(value) ? string.Empty : value;
        return true;
    }

    public override IEnumerable<string> InputValueConverter(int part, string? input)
    {
        if (string.IsNullOrWhiteSpace(input)) { yield break; }

        yield return input;
    }

    public override Dictionary<int, Description> ProblemParts =>
        new Dictionary<int, Description>
        {
            {1, new Part1Description()},
            {2, new Part2Description()},
        };

    public static long SolvePart1(IDirectory root)
    {
        var maximumSize = 100000;
        var directories = GetDirectoriesBySize(root, maximumSize: maximumSize);
        return directories.Sum(i => i.Value);
    }

    public static long SolvePart2(IDirectory root)
    {
        var diskSize = 70000000L;
        var requiredSize = 30000000L;
        var usedSpace = root.CalculateTotalSize();
        var remainingSpace = diskSize - usedSpace;
        var additionalSizeNeeded = requiredSize - remainingSpace;
        if (additionalSizeNeeded <= 0) { return 0; }

        var directories = GetDirectoriesBySize(root, minimumSize: additionalSizeNeeded);
        var smallest = directories.OrderBy(d => d.Value).FirstOrDefault();
        return smallest.Value;
    }

    public static IDictionary<IDirectory, long> GetDirectoriesBySize(IDirectory parent, long? maximumSize = null, long? minimumSize = null)
    {
        var toProcess = new Queue<IDirectory>();
        toProcess.Enqueue(parent);

        var directories = new Dictionary<IDirectory, long>();
        while(toProcess.Any())
        {
            var directory = toProcess.Dequeue();
            foreach(var item in directory.Contents)
            {
                if (item is not IDirectory child) { continue; }
                toProcess.Enqueue(child);
            }

            directories.Add(directory, directory.CalculateTotalSize());
        }

        var filtered = directories.AsEnumerable();

        if (maximumSize.HasValue)
        {
            filtered = filtered.Where(d => d.Value <= maximumSize.Value);
        }

        if (minimumSize.HasValue)
        {
            filtered = filtered.Where(d => d.Value >= minimumSize.Value);
        }

        return filtered.ToDictionary(d => d.Key, d => d.Value);
    }
}