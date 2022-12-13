namespace AdventOfCode.App.Y2022.Problems.Day7;

public interface IDirectory : IFileSystemItem
{
    IEnumerable<IFileSystemItem> Contents { get; }
    IDirectory AddDirectory(string name);
    IFile AddFile(string name, int size);
    bool HasDirectory(string name);
    IDirectory? FindDirectory(string name);
    long CalculateTotalSize();
}

public class Directory : IDirectory
{
    private readonly Dictionary<string, IFileSystemItem> _contents = new Dictionary<string, IFileSystemItem>();

    private Directory(){}

    public IDirectory? Root { init; get; } = null;
    public IDirectory? Parent { init; get; } = null;
    public string Name { init; get; } = string.Empty;
    public IEnumerable<IFileSystemItem> Contents => _contents.Values;

    public IDirectory AddDirectory(string name)
    {
        if (_contents.ContainsKey(name))
        {
            throw new Exception($"Error adding directory '{name}' within {Name}. Name already exists.");
        }

        var child = new Directory
        {
            Root = this.Root ?? this,
            Parent = this,
            Name = name
        };

        _contents.Add(name, child);
        return child;
    }

    public IFile AddFile(string name, int size)
    {
        if (_contents.ContainsKey(name))
        {
            throw new Exception($"Error adding file '{name}' within {Name}. Name already exists.");
        }

        var child = new File
        {
            Root = this.Root,
            Parent = this,
            Name = name,
            FileSize = size
        };

        _contents.Add(name, child);
        return child;
    }

    public IDirectory? FindDirectory(string name)
    {
        if (_contents.TryGetValue(name, out var content) is false)
        {
            return null;
        }

        return content as IDirectory;
    }

    public bool HasDirectory(string name) =>
        _contents.ContainsKey(name) && _contents[name] is IDirectory;

    public long CalculateTotalSize()
    {
        long total = 0;

        foreach(var item in Contents)
        {
            if (item is IFile file)
            {
                total += file.FileSize;
            }
            else if (item is IDirectory directory)
            {
                total += directory.CalculateTotalSize();
            }
        }

        return total;
    }

    public static IDirectory CreateRoot() =>
        new Directory
        {
            Parent = null,
            Name = "/"
        };
}