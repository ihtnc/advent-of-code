namespace AdventOfCode.Shared;

public class ErrorDescription : Description
{
    public ErrorDescription(string message)
    {
        Text = message;
    }

    public override string Text { get; }
    public override string Example => string.Empty;
    public override string Explanation => string.Empty;

    public override string ToString()
    {
        return Text;
    }
}