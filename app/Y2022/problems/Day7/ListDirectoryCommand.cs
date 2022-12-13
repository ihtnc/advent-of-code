namespace AdventOfCode.App.Y2022.Problems.Day7;

public interface IListDirectoryCommand : IFileSystemCommand
{
    IEnumerable<string> Output { init; get; }
}

public class ListDirectoryCommand : IListDirectoryCommand
{
    public IEnumerable<string> Output { init; get; } = new string[0];
}