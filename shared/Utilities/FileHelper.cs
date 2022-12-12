namespace AdventOfCode.Shared.Utilities;

public interface IFileHelper
{
    bool FileExists(string filePath);
    IEnumerable<T> ParseLines<T>(string filePath, ValueConverterDelegate<T> lineConverter);
}

public class FileHelper : IFileHelper
{
    public bool FileExists(string filePath) =>
        File.Exists(filePath);

    public IEnumerable<T> ParseLines<T>(string filePath, ValueConverterDelegate<T> lineConverter)
    {
        using(var reader = System.IO.File.OpenText(filePath))
        {
            while(reader.EndOfStream is false)
            {
                var line = reader.ReadLine();
                if(lineConverter(line, out var value))
                {
                    yield return value;
                }
            }
        }
    }
}