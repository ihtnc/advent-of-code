namespace AdventOfCode.App.Y2022.Problems.Day7;

public interface IChangeDirectoryCommand : IFileSystemCommand
{
    string DirectoryName { init; get; }
}

public class ChangeDirectoryCommand : IChangeDirectoryCommand
{
    public string DirectoryName { init; get; } = string.Empty;
}