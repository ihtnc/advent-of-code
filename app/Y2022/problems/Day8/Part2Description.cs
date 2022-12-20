using AdventOfCode.Shared;

namespace AdventOfCode.App.Y2022.Problems.Day8;

public class Part2Description : Description
{
    public override string Text =>
@"Given a grid of integers, calculate the scenic score of each digit and determine the highest scenic score among all the digits. The scenic score is calculated by using the following rules:
1. Scenic score = Left viewing distance * Right viewing distance * Top viewing distance * bottom viewing distance.
2. Left viewing distance = number of digits between a particular digit and a value that is greater than or equal to the digit itself that is to the left of where it is located on the same row.
3. Right viewing distance = number of digits between a particular digit and a value that is greater than or equal to the digit itself that is to the right of where it is located on the same row.
4. Top viewing distance = number of digits between a particular digit and a value that is greater than or equal to the digit itself that is above where it is located on the same column.
5. Bottom viewing distance = number of digits between a particular digit and a value that is greater than or equal to the digit itself that is below where it is located on the same column.
6. The scenic score of the digits along the edge is always 0 since at least one of its viewing distance has no other digits.
7. Find the highest scenic score among all the digits.";

    public override string Example =>
@"Given:
30373
25512
65332
33549
35390

Output: 8";

    public override string Explanation =>
@"Example 1:
The 5 near the top-left corner (row 2, column 2) has a scenic score of 1.

c   12345
r  -------
1 |  0
2 | 25512
3 |  5
4 |  3
5 |  5

|------------------------|--------|------------------|
| Values                 | Digits | Viewing distance |
|------------------------|--------|------------------|
| Left along the row     | 2      |        1         |
| Right along the row    | 5 1 2  |        1         |
| Above along the column | 0      |        1         |
| Below along the column | 5 3 5  |        1         |
|------------------------|--------|------------------|

There is only 1 value to the left of the digit along the same row and it is less than the digit itself.
The value immediately to the right of the digit along the same row is greater than or equal to the digit itself.
There is only 1 value above the digit along the same column and it is less than the digit itself.
The value immediately below the digit along the same column is greater than or equal to the digit itself.

Scenic score = left viewing distance * right viewing distance * top viewing distance * bottom viewing distance
Scenic score = 1 * 1 * 1 * 1
The scenic score of the digit in row 2 column 2 is 1

Example 2:
The middle 5 near the bottom edge (row 4, column 3) has a scenic score of 8.

c   12345
r  -------
1 |   3
2 |   5
3 |   3
3 | 33549
5 |   3

|------------------------|--------|------------------|
| Values                 | Digits | Viewing distance |
|------------------------|--------|------------------|
| Left along the row     | 3 3    |        2         |
| Right along the row    | 4 9    |        2         |
| Above along the column | 3 5 3  |        2         |
| Below along the column | 3      |        1         |
|------------------------|--------|------------------|

The two values to the left of the digit along the same row is less than the digit itself.
The value to the right of the digit along the same row that is greater than or equal to the digit itself is 2 digits away.
The value above the digit along the same column that is greater than or equal to the digit itself is 2 digits away.
The only value below the digit along the same column is less than the digit itself.

Scenic score = left viewing distance * right viewing distance * top viewing distance * bottom viewing distance
Scenic score = 2 * 2 * 2 * 1
The scenic score of the digit in row 4 column 3 is 8

Ouput:
Applying the above examples to all the inner digits, the highest scenic score of all the digits is 8.";
}