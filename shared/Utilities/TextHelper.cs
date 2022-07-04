namespace AdventOfCode.Shared.Utilities;

public interface ITextHelper
{
    IEnumerable<T> ParseInput<T>(string input, InputConverterDelegate inputConverter, ValueConverterDelegate<T> valueConverter);
}

public class TextHelper : ITextHelper
{
    public IEnumerable<T> ParseInput<T>(string input, InputConverterDelegate inputConverter, ValueConverterDelegate<T> valueConverter)
    {
        var lines = inputConverter(input);
        foreach(var line in lines)
        {
            if(valueConverter(line, out var value))
            {
                yield return value;
            }
        }
    }
}