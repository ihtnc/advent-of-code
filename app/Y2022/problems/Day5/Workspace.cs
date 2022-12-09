using System.Text.RegularExpressions;

namespace AdventOfCode.App.Y2022.Problems.Day5;

public class Workspace : Dictionary<int, Stack<string>>
{
    private static readonly Regex _stackLabelFormat = new Regex(@"^\s*(\d+(?:\s+[\d]+)*)\s*$", RegexOptions.Multiline);
    private static readonly Regex _stackIndexFormat = new Regex(@"[\d]+");
    private static readonly Regex _stacksFormat = new Regex(@"(?'start'\[)\s*(?'value'[\w]+)\s*(?'end'\])");

    public Workspace() { }
    public Workspace(IDictionary<int, Stack<string>> initial) : base(initial) { }

    public static bool TryParse(string value, out Workspace parsed)
    {
        var index = GetStackLabelIndices(value);
        var stackText = GetStackText(value);
        parsed = GetWorkspace(stackText, index);
        return true;
    }

    public IEnumerable<string> GetTopItems()
    {
        foreach(var item in this)
        {
            var value = item.Value.Any() ? item.Value.Peek() : " ";
            yield return value;
        }
    }

    public static IReadOnlyDictionary<int, int> GetStackLabelIndices(string workspaceText)
    {
        var label = string.Empty;
        var stackLabel = _stackLabelFormat.Match(workspaceText);
        if (stackLabel.Success) { label = stackLabel.Value; }

        var stackIndex = new Dictionary<int, int>();
        var index = _stackIndexFormat.Match(label);
        while(index.Success)
        {
            if (int.TryParse(index.Value, out var i) is false) { continue; }
            stackIndex.Add(index.Index, i);
            index = index.NextMatch();
        }

        return stackIndex;
    }

    public static string GetStackText(string workspaceText)
    {
        var label = _stackLabelFormat.Match(workspaceText);
        if (label.Success is false) { return string.Empty; }
        return workspaceText.Substring(0, label.Index);
    }

    public static Workspace GetWorkspace(string stackText, IReadOnlyDictionary<int, int> stackIndex)
    {
        var stacks = new Dictionary<int, Stack<string>>();
        var lines = stackText.Split(new [] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).Reverse();
        foreach(var line in lines)
        {
            var stack = _stacksFormat.Match(line);
            while(stack.Success)
            {
                var start = stack.Groups["start"];
                var value = stack.Groups["value"].Value;
                var end = stack.Groups["end"];

                foreach(var index in stackIndex)
                {
                    if (start.Index < index.Key && end.Index > index.Key)
                    {
                        if (stacks.ContainsKey(index.Value) is false)
                        {
                            stacks.Add(index.Value, new Stack<string>());
                        }

                        stacks[index.Value].Push(value);
                    }
                }

                stack = stack.NextMatch();
            }
        }

        return new Workspace(stacks);
    }
}