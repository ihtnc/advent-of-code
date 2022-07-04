using AdventOfCode.Shared;

namespace AdventOfCode.App.Y2021.Problems.Day1;

public class Part1Description : Description
{
    public override string Text => "Given a list of integers, count the number of times a value increases from the previous item in the list.";

    public override string Example =>
@"Given: [2, 3, 1, 4]
Output: 2";

    public override string Explanation =>
@"Disregard the first value (2) since that is the first item in the list.
The third value (1) is a NOT an increase in the previous value (3) and so is not counted.
The second and fourth values (3 and 4) are an increase from their respective previous values (2 and 1).
Only two numbers met the criteria and so the output is 2.";
}