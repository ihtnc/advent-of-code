using AdventOfCode.Shared;

namespace AdventOfCode.App.Y2022.Problems.Day6;

public class Part1Description : Description
{
    public override string Text => "Given a string, find the index of the string immediately following the first set of 4 unique characters.";

    public override string Example =>
@"Given: [mjqjpqmgbljsphdztnvjfqwrcgsmlb]
Output: 7";

    public override string Explanation =>
@"The first occurrence of a string with 4 unique characters is on index 3 ""jpqm"".
|---|--------------------------------|---------------|
|   | mjqjpqmgbljsphdztnvjfqwrcgsmlb |               |
|---|--------------------------------|---------------|
| 0 | mjqj                           | Duplicate j   |
| 1 |  jqjp                          | Duplicate j   |
| 2 |   qjpq                         | Duplicate q   |
| 3 |    jpqm                        | No duplicates |
|---|--------------------------------|---------------|

The index of the string immediately after ""jpqm"" is 7.";
}