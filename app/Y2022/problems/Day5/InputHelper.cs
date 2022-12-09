using System.Text.RegularExpressions;

namespace AdventOfCode.App.Y2022.Problems.Day5;

public static class InputHelper
{
    private static readonly Regex _emptyLineFormat = new Regex(@"^(?'separator'\s*)$", RegexOptions.Multiline);
    public static readonly Regex MoveFormat = new Regex(@"move\s+(?'count'\d+)\s+from\s+(?'source'\d+)\s+to\s+(?'target'\d+)", RegexOptions.IgnoreCase);

    public static Workspace Rearrange(Workspace workspace, IEnumerable<MoveCommand> commands, bool preserveSourceStackOrder)
    {
        var output = new Workspace(workspace);

        foreach(var command in commands)
        {
            if (command is null) { continue; }

            if (output.ContainsKey(command.Source) is false
                || output.ContainsKey(command.Target) is false
                || output[command.Source].Any() is false)
            { continue; }

            var stack = new Stack<string>();
            output[command.Source].PopTo(stack, command.Count);
            if (preserveSourceStackOrder is false) { stack.Reverse(); }

            stack.PopAllTo(output[command.Target]);
        }

        return output;
    }

    public static Workspace ToWorkspace(this string input)
    {
        var separator = _emptyLineFormat.Match(input);
        if (separator.Success is false) { return new Workspace(); }
        var workspaceText =  input.Substring(0, separator.Index);

        if (Workspace.TryParse(workspaceText, out var workspace) is false)
        {
            return new Workspace();
        }

        return workspace;
    }

    public static IEnumerable<MoveCommand> ToMoveCommands(this string input)
    {
        var match = MoveFormat.Match(input);
        while(match.Success)
        {
            if (MoveCommand.TryParse(match.Value, out var parsed) is false)
            {
                yield break;
            }

            yield return parsed;
            match = match.NextMatch();
        }
    }

    public static void Reverse<T>(this Stack<T> source)
    {
        var reversed = new Queue<T>();
        while (source.Count > 0)
        {
            var value = source.Pop();
            reversed.Enqueue(value);
        }

        while (reversed.Count > 0)
        {
            var value = reversed.Dequeue();
            source.Push(value);
        }
    }

    public static void PopTo<T>(this Stack<T> source, Stack<T> target, int count = 1)
    {
        if (source.Any() is false || count > source.Count) { return; }

        var index = 0;
        while(index < count)
        {
            var value = source.Pop();
            target.Push(value);
            index++;
        }
    }

    public static void PopAllTo<T>(this Stack<T> source, Stack<T> target)
    {
        source.PopTo(target, source.Count);
    }
}