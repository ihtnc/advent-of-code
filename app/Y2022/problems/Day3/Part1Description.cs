using AdventOfCode.Shared;

namespace AdventOfCode.App.Y2022.Problems.Day3;

public class Part1Description : Description
{
    public override string Text =>
@"Given a list of alphabetic character groups, calculate the total priority based on the following rules:
1. Each character carries a certain value (a=1, b=2, ..., z=26, A=27, B=28, ..., Z=52).
2. Find the common characters between the two halves of a group.
3. Priority = sum of all the values of the common characters between the two halves.
4. Total priority = sum of all the priorities.";

    public override string Example =>
@"Given: [vJrwpWtwJgWrhcsFMMfFFhFp jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL PmmdzqPrVvPwwTWBwg]
Output: 96";

    public override string Explanation =>
@"There are 3 groups in the list:
|----------------------------------|------------------|------------------|--------|----------|
|              Value               |     1st Half     |     2nd Half     | Common | Priority |
|----------------------------------|------------------|------------------|--------|----------|
| vJrwpWtwJgWrhcsFMMfFFhFp         | vJrwpWtwJgWr     | hcsFMMfFFhFp     |   p    |    16    |
| jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL | jqHRNqRjqzjGDLGL | rsFMfFZSrLrFZsSL |   L    |    38    |
| PmmdzqPrVvPwwTWBwg               | PmmdzqPrV        | vPwwTWBwg        |   P    |    42    |
|----------------------------------|------------------|------------------|--------|----------|

Common letters between the first and second half of group 1: p (Priority=16)
Common letters between the first and second half of group 2: L (Priority=38)
Common letters between the first and second half of group 3: P (Priority=42)
Total priority is 96 (16+38+42).";
}