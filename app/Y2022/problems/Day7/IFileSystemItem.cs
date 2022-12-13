namespace AdventOfCode.App.Y2022.Problems.Day7;

public interface IFileSystemItem
{
    IDirectory? Root { init; get; }
    IDirectory? Parent { init; get; }
    string Name { init; get; }
}