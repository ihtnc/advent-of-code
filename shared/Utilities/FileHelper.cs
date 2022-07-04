namespace AdventOfCode.Shared.Utilities;

public interface IFileHelper
{
    IEnumerable<T> ParseLines<T>(string filePath, ValueConverterDelegate<T> lineConverter);
}

public class FileHelper : IFileHelper
{
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