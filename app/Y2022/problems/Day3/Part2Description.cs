using AdventOfCode.Shared;

namespace AdventOfCode.App.Y2022.Problems.Day3;

public class Part2Description : Description
{
    public override string Text =>
@"Given a list of alphabetic character groups, calculate the total priority based on the following rules:
1. Each character carries a certain value (a=1, b=2, ..., z=26, A=27, B=28, ..., Z=52).
2. Find the common characters between the first 3 character groups.
3. Priority = sum of all the values of the common characters between the 3 character groups.
4. Calculate the priority for the next 3 character groups and repeat until the end of the list.
4. Total priority = sum of all the priorities.";

    public override string Example =>
@"Given: [vJrwpWtwJgWrhcsFMMfFFhFp jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL PmmdzqPrVvPwwTWBwg wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn ttgJtRGJQctTZtZT CrZsJsPPZsGzwwsLwLmpwMDw]
Output: 70";

    public override string Explanation =>
@"There are 2 groups in the list
|-------|----------------------------------|--------|----------|
| Group |              Value               | Common | Priority |
|-------|----------------------------------|--------|----------|
|       | vJrwpWtwJgWrhcsFMMfFFhFp         |        |          |
|   1   | jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL |   r    |    18    |
|       | PmmdzqPrVvPwwTWBwg               |        |          |
|-------|----------------------------------|--------|----------|
|       | wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn   |        |          |
|   2   | ttgJtRGJQctTZtZT                 |   Z    |    52    |
|       | CrZsJsPPZsGzwwsLwLmpwMDw         |        |          |
|-------|----------------------------------|--------|----------|

Common letters between the first three groups: r (Priority=18)
Common letters between the next three groups: Z (Priority=52)
Total priority is 70 (18+52).";
}