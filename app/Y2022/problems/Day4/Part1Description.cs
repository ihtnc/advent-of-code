using AdventOfCode.Shared;

namespace AdventOfCode.App.Y2022.Problems.Day4;

public class Part1Description : Description
{
    public override string Text => "Given a list of range pairs, count the number of pairs that has one item completely overlapping the other item.";

    public override string Example =>
@"Given: [2-4,6-8 5-7,7-9 6-6,4-6 2-6,4-8]
Output: 1";

    public override string Explanation =>
@"There are 4 pairs in the list:
|------|--------|--------|-------------------------------------------------------|
| Pair | Range1 | Range2 |                         Notes                         |
|------|--------|--------|-------------------------------------------------------|
|  1   |  2-4   |  6-8   | Range1 and Range2 does not intersect at all           |
|  2   |  5-7   |  7-9   | Both ranges are only partially overlapping each other |
|  3   |  6-6   |  4-6   | Range2 completely overlaps Range1                     |
|  4   |  2-6   |  4-8   | Both ranges are only partially overlapping each other |
|------|--------|--------|-------------------------------------------------------|

There is only 1 pair where one range completely overlaps the other range.";
}