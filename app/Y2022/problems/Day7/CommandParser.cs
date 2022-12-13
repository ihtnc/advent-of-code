using System.Text.RegularExpressions;

namespace AdventOfCode.App.Y2022.Problems.Day7;

public class CommandParser
{
    private static readonly Regex _cdCommandFormat = new Regex(@"^\$\s*cd\s+(?'name'[\w]+|..|\/)$", RegexOptions.Multiline | RegexOptions.IgnoreCase);
    private static readonly Regex _lsCommandFormat = new Regex(@"^\$\s*ls", RegexOptions.Multiline | RegexOptions.IgnoreCase);

    public static IEnumerable<IFileSystemCommand> Parse(IEnumerable<string> input)
    {
        if (input.Any() is false) { yield break; }

        var queue = new Queue<string>(input);
        while(queue.Any())
        {
            var current = queue.Dequeue();
            var isCD = _cdCommandFormat.Match(current);
            var isLS = _lsCommandFormat.Match(current);

            if (isCD.Success)
            {
                var command = CreateChangeDirectoryCommand(isCD.Groups["name"].Value);
                yield return command;
            }
            else if (isLS.Success)
            {
                var contents = new List<string>();
                while (queue.Any() && queue.Peek().StartsWith("$") is false)
                {
                    var child = queue.Dequeue();
                    contents.Add(child);
                }

                var command = CreateListDirectoryCommand(contents);
                yield return command;
            }
        }
    }

    public static IFileSystemCommand CreateChangeDirectoryCommand(string name) =>
        new ChangeDirectoryCommand { DirectoryName = name };

    public static IFileSystemCommand CreateListDirectoryCommand(IEnumerable<string> contents) =>
        new ListDirectoryCommand { Output = contents };
}