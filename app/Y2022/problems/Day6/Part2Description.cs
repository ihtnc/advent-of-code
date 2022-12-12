using AdventOfCode.Shared;

namespace AdventOfCode.App.Y2022.Problems.Day6;

public class Part2Description : Description
{
    public override string Text => "Given a string, find the index of the string immediately following the first set of 14 unique characters.";

    public override string Example =>
@"Given: [mjqjpqmgbljsphdztnvjfqwrcgsmlb]
Output: 19";

    public override string Explanation =>
@"The first occurrence of a string with 14 unique characters is on index 5 ""qmgbljsphdztnv"".
|---|--------------------------------|----------------------|
|   | mjqjpqmgbljsphdztnvjfqwrcgsmlb |                      |
|---|--------------------------------|----------------------|
| 0 | mjqjpqmgbljsph                 | Duplicate m, j, q, p |
| 1 |  jqjpqmgbljsphd                | Duplicate j, q, p    |
| 2 |   qjpqmgbljsphdz               | Duplicate q, j, p    |
| 3 |    jpqmgbljsphdzt              | Duplicate j, p       |
| 4 |     pqmgbljsphdztn             | Duplicate p          |
| 5 |      qmgbljsphdztnv            | No duplicates        |
|---|--------------------------------|----------------------|

The index of the string immediately after ""qmgbljsphdztnv"" is 19.";
}