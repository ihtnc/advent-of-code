using System.Text;

namespace AdventOfCode.Shared;

public abstract class Description
{
    public abstract string Text { get; }
    public abstract string Example { get; }
    public abstract string Explanation { get; }

    public override string ToString()
    {
        var description = new StringBuilder();

        if(string.IsNullOrWhiteSpace(Text) is false)
        {
            description.AppendLine("Description:");

            var lines = Text.Split('\n', StringSplitOptions.TrimEntries);
            foreach(var line in lines)
            {
                description.AppendLine($"\t{line}");
            }
        }
        else
        {
            description.Append("None supplied.");
        }


        if(string.IsNullOrWhiteSpace(Example) is false)
        {
            description.AppendLine();
            description.AppendLine("Example:");

            var lines = Example.Split('\n', StringSplitOptions.TrimEntries);
            foreach(var line in lines)
            {
                description.AppendLine($"\t{line}");
            }
        }

        if(string.IsNullOrWhiteSpace(Explanation) is false)
        {
            description.AppendLine();
            description.AppendLine("Explanation:");

            var lines = Explanation.Split('\n', StringSplitOptions.TrimEntries);
            foreach(var line in lines)
            {
                description.AppendLine($"\t{line}");
            }
        }

        return $"{description}";
    }
}