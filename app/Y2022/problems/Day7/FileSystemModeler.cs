using System.Text.RegularExpressions;

namespace AdventOfCode.App.Y2022.Problems.Day7;

public class FileSystemModeler
{
    private static readonly Regex _fileFormat = new Regex(@"^\s*(?'size'[\d]+)\s*(?'name'.+)$", RegexOptions.Multiline | RegexOptions.IgnoreCase);
    private static readonly Regex _directoryFormat = new Regex(@"^\s*dir\s*(?'name'.+)$", RegexOptions.Multiline | RegexOptions.IgnoreCase);

    private static readonly string _upOneLevel = "..";
    private static readonly string _rootLevel = "/";

    public static IDirectory RebuildFileSystem(IEnumerable<IFileSystemCommand> commands)
    {
        var root = Directory.CreateRoot();
        var current = root;
        foreach(var command in commands)
        {
            switch (command)
            {
                case IChangeDirectoryCommand cd:
                    current = HandleChangeDirectory(current, cd);
                    break;
                case IListDirectoryCommand ls:
                    current = HandleListDirectory(current, ls);
                    break;
            }
        }

        return root;
    }

    public static IDirectory HandleChangeDirectory(IDirectory current, IChangeDirectoryCommand command)
    {
        if (string.Equals(command.DirectoryName, _rootLevel, StringComparison.OrdinalIgnoreCase))
        {
            return current.Root ?? current;
        }

        if (string.Equals(command.DirectoryName, _upOneLevel, StringComparison.OrdinalIgnoreCase))
        {
            return current.Parent ?? current;
        }

        if (current.HasDirectory(command.DirectoryName) is false)
        {
            return current.AddDirectory(command.DirectoryName);
        }

        var next = current.FindDirectory(command.DirectoryName);
        return next ?? current;
    }

    public static IDirectory HandleListDirectory(IDirectory current, IListDirectoryCommand command)
    {
        foreach(var content in command.Output)
        {
            var isFile = _fileFormat.Match(content);
            if (isFile.Success)
            {
                if (int.TryParse(isFile.Groups["size"].Value, out var size))
                {
                    var name = isFile.Groups["name"].Value;
                    current.AddFile(name, size);
                }

                continue;
            }

            var isDirectory = _directoryFormat.Match(content);
            if (isDirectory.Success)
            {
                var name = isDirectory.Groups["name"].Value;
                current.AddDirectory(name);
            }
        }

        return current;
    }
}