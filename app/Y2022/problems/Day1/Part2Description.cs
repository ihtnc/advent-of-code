using AdventOfCode.Shared;

namespace AdventOfCode.App.Y2022.Problems.Day1;

public class Part2Description : Description
{
    public override string Text => "Given a list of integers, group the items that are separated by a blank item together and compute their sum. Then calculate the sum of the top 3 highest values.";

    public override string Example =>
@"Given: [1, , 2,3, , 4, , 2,2, , 1,2]
Output: 13";

    public override string Explanation =>
@"There are 5 groups in the list:
|-------|-------|-----|
| Group | Items | Sum |
|-------|-------|-----|
|   1   |   1   |  1  |
|   2   |  2, 3 |  5  |
|   3   |   4   |  4  |
|   4   |  2, 2 |  4  |
|   5   |  1, 2 |  3  |
|-------|-------|-----|

The groups 1,2,3 have the highest sum out of all the groups. Their sum is 13.";
}