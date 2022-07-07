using AdventOfCode.Shared;

namespace AdventOfCode.App.Y2021.Problems.Day3;

public class Part1Description : Description
{
   public override string Text =>
@"Given a list of binary numbers, calculate the POWER CONSUMPTION.

Formula:
POWER CONSUMPTION = GAMMA RATE * EPSILON RATE
GAMMA RATE = most common bit in each position on all binary numbers converted into decimal
EPSILON RATE = least common bit in each position on all binary numbers converted into decimal";

    public override string Example =>
@"Given: [100, 001, 110]
Output: 12";

    public override string Explanation =>
@"|----------------|-------|
| Binary numbers |       |
|----------------|-------|
|      100       | 1 0 0 |
|      001       | 0 0 1 |
|      110       | 1 1 0 |
|----------------|-------|
|  Most common   | 1 0 0 |
|  Least common  | 0 1 1 |
|----------------|-------|

GAMMA RATE        = 100 in binary or 4 in decimal
EPSILON RATE      = 011 in binary or 3 in decimal
POWER CONSUMPTION = 4 * 3 = 12";
}