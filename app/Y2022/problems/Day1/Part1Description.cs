using AdventOfCode.Shared;

namespace AdventOfCode.App.Y2022.Problems.Day1;

public class Part1Description : Description
{
    public override string Text => "Given a list of integers, group the items that are separated by a blank item together and compute their sum. Identify the highest sum of all the groups.";

    public override string Example =>
@"Given: [1, , 2,3, , 4]
Output: 5";

    public override string Explanation =>
@"There are 3 groups in the list:
|-------|-------|-----|
| Group | Items | Sum |
|-------|-------|-----|
|   1   |   1   |  1  |
|   2   |  2, 3 |  5  |
|   3   |   4   |  4  |
|-------|-------|-----|

The second group has the highest sum out of every group. The highest sum is 5.";
}