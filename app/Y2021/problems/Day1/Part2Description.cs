using AdventOfCode.Shared;

namespace AdventOfCode.App.Y2021.Problems.Day1;

public class Part2Description : Description
{
    public override string Text => "Given a list of integers, count the number of times a group of 3 consecutive values increases from the previous group of 3 consecutive item in the list.";

    public override string Example =>
@"Given: [1, 2, 3, 4, 2]
Output: 1";

    public override string Explanation =>
@"Disregard the first group (1, 2, 3) since that is first in the list.
The sum of the second group (2, 3, 4) is 9 which is an increase from the previous group's (1, 2, 3) sum of 6.
The third group's (3, 4, 2) sum of 9 is not an increase from the previous group (2, 3, 4) so is not counted.
The group needs to have three values for it to be considered so the third group is the last group.
Only one group met the criteria and so the output is 1.";
}