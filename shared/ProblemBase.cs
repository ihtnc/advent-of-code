using AdventOfCode.Shared.Utilities;

namespace AdventOfCode.Shared;

public abstract class ProblemBase<T> : IProblem
{
    protected readonly ITextHelper _textHelper;
    protected readonly IFileHelper _fileHelper;

    public ProblemBase(ITextHelper textHelper, IFileHelper fileHelper)
    {
        _textHelper = textHelper;
        _fileHelper = fileHelper;
    }

    protected virtual IEnumerable<T> GetInput(RunOption option)
    {
        var part = option.Part;
        var input = option.Input;

        InputConverterDelegate internalInputValueConverter = (i) => InputValueConverter(part, i);
        ValueConverterDelegate<T> internalLineValueConverter = (string? v, out T c) => LineValueConverter(part, v, out c);

        IEnumerable<T> values;
        if (IsInlineInput(part, input))
        {
            values = _textHelper.ParseInput<T>(input, internalInputValueConverter, internalLineValueConverter);
        }
        else
        {
            values = _fileHelper.ParseLines<T>(input, internalLineValueConverter);
        }

        return values;
    }

    public virtual Description GetDescription(DescribeOption option)
    {
        if (ProblemParts.ContainsKey(option.Part))
        {
            return ProblemParts[option.Part];
        }
        else
        {
            return ErrorDescription.UnsupportedProblemPart(option.Part);
        }
    }

    public abstract string Url { get; }

    public virtual object Solve(RunOption option)
    {
        if (ProblemParts.ContainsKey(option.Part) is false)
        {
            return ErrorDescription.UnsupportedProblemPart(option.Part);
        }

        var values = GetInput(option);
        return Solve(values, option.Part);
    }

    public abstract object Solve(IEnumerable<T> input, int problemPart);

    public abstract bool IsInlineInput(int part, string input);

    public abstract bool LineValueConverter(int part, string? value, out T converted);

    public abstract IEnumerable<string> InputValueConverter(int part, string? input);

    public abstract Dictionary<int, Description> ProblemParts { get; }
}