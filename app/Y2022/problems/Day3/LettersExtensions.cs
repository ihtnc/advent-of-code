using System.Text;

namespace AdventOfCode.App.Y2022.Problems.Day3;

public static class LettersExtensions
{
    public static Letters GetUniqueLetters(this string value)
    {
        var response = Letters.Blank;
        foreach(var c in value)
        {
            if (Enum.TryParse<Letters>($"{c}", false, out var current) is false) { continue; }
            response |= current;
        }
        return response;
    }

    public static IEnumerable<Letters> ToIndividualLetters(this Letters value)
    {
        var response = new List<Letters>();
        var toProcess = value;
        var names = Enum.GetNames<Letters>();
        foreach(var name in names)
        {
            if (toProcess == Letters.Blank) { break; }
            if (Enum.TryParse<Letters>(name, false, out var current) is false) { continue; }
            if (current == Letters.Blank || toProcess.HasFlag(current) is false) { continue; }

            response.Add(current);
            toProcess ^= current;
        }

        return response;
    }

    public static string GetString(this IEnumerable<Letters> value)
    {
        var response = new StringBuilder();
        var list = value ?? new Letters[0];
        foreach(var item in list)
        {
            response.Append(item);
        }

        return $"{response}";
    }

    public static string GetString(this Letters value)
    {
        var letters = value.ToIndividualLetters();
        return letters.GetString();
    }
}