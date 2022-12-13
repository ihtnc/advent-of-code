namespace AdventOfCode.App.Y2022.Problems.Day7;

public interface IFile : IFileSystemItem
{
    int FileSize { init; get; }
}

public class File : IFile
{
    public IDirectory? Root { init; get; }
    public IDirectory? Parent { init; get; }
    public string Name { init; get; } = string.Empty;
    public int FileSize { init; get; }
}