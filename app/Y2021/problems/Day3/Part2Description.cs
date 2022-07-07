using AdventOfCode.Shared;

namespace AdventOfCode.App.Y2021.Problems.Day3;

public class Part2Description : Description
{
    public override string Text =>
@"Given a list of binary numbers, calculate the LIFE SUPPORT RATING.

Formula:
LIFE SUPPORT RATING = OXYGEN GENERATOR RATING * CARBON DIOXIDE SCRUBBER RATING.
OXYGEN GENERATOR RATING = most common binary number converted into decimal.
CARBON DIOXIDE SCRUBBER RATING = least common binary number converted into decimal.

To calculate the most common binary number:
1. Find the most common bit on the first position on each item in the list.
2. Filter the list to contain only those having the same bit in their first position.
3. Find the most common bit on the second position on each item in the filtered list.
4. Further filter the list to contain only those having the same bit in their second position.
5. Repeat the process for the third, fourth, and further positions, further filtering the list until you only have one item left.
6. If both bits are equally common, use the 1 bit.
7. The last item remaining is the most common binary number.

To calculate the least common binary number:
1. Do the same logic as the most common bit but find the least common bit instead.
2. Likewise, if both bits are equally common, use the 1 bit.
3. The last item remaining is the least common binary number.";

    public override string Example =>
@"Given: [100, 001, 110]
Output: 6";

    public override string Explanation =>
@"Most common bit:
1. Most common bit on the first position: 1
2. New list: [100, 110]
3. Most common bit on the second position for the new list: 1 (1 is preferred if both are equally common)
4. New list: [110]
5. End filtering (one item remains)
6. Most common binary number is 110 in binary or 6 in decimal

Least common bit:
1. Least common bit on the first position: 0
2. New list: [001]
3. End filtering (one item remains)
4. Least common binary number is 001 in binary or 1 in decimal

LIFE SUPPORT RATING = OXYGEN GENERATOR RATING * CARBON DIOXIDE SCRUBBER RATING.
OXYGEN GENERATOR RATING = 6
CARBON DIOXIDE SCRUBBER RATING = 1
LIFE SUPPORT RATING = 6
";
}