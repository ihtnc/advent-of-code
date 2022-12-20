using AdventOfCode.Shared;

namespace AdventOfCode.App.Y2022.Problems.Day8;

public class Part1Description : Description
{
    public override string Text =>
@"Given a grid of integers, count the number of digits given the following rules:
1. Digits along the edges are always counted.
2. Edge digits = entire first row, entire last row, first digits of each row, and last digits of each row.
3. A digit inside the grid is only counted if it is greater than all the other digits along the row or column towards a certain edge.
4. Example: If all the other values to the left of a certain digit is less than the digit itself, then that digit is counted.";

    public override string Example =>
@"Given:
30373
25512
65332
33549
35390

Output: 21";

    public override string Explanation =>
@"There are 16 digits on the edge:
|-------|-----------|-------|
|  Row  |   Edges   | Count |
|-------|-----------|-------|
|   1   | 3 0 3 7 3 |   5   |
|   2   | 2       2 |   2   |
|   3   | 6       2 |   2   |
|   4   | 3       9 |   2   |
|   5   | 3 5 3 9 0 |   5   |
|-------|-----------|-------|
| Total             |  16   |
|-------------------|-------|

Example 1:
The 5 near the top-left corner (row 2, column 2) is counted because there is a row/column where all the other values are less than the digit itself.

c   12345
r  -------
1 |  0
2 | 25512
3 |  5
4 |  3
5 |  5

|------------------------|--------|----------|
| Values                 | Digits | Counted? |
|------------------------|--------|----------|
| Left along the row     | 2      |    Y     |
| Right along the row    | 5 1 2  |    N     |
| Above along the column | 0      |    Y     |
| Below along the column | 5 3 5  |    N     |
|------------------------|--------|----------|

All the values to the left of the digit along the same row is less than the digit itself.
All the values to above the digit along the same column is less than the digit itself.
Therefore, the digit in row 2 column 2 is counted.

Example 2:
The middle digit 3 (row 3, column 3) is not counted because there is a value that is greater than or equal to the digit itself along the row and column where it is located.
c   12345
r  -------
1 |   3
2 |   5
3 | 65332
4 |   5
5 |   3

|------------------------|--------|----------|
| Values                 | Digits | Counted? |
|------------------------|--------|----------|
| Left along the row     | 5 6    |    N     |
| Right along the row    | 3 2    |    N     |
| Above along the column | 5 3    |    N     |
| Below along the column | 5 3    |    N     |
|------------------------|--------|----------|

There is a value to the left and right of the digit along the same row that is greather than or equal to the digit itself.
There is a value above and below the digit along the same column that is greater than or equal to the digit itself.
Therefore, the digit in row 3 column 3 can not be counted.

Ouput:
Applying the above examples to all the inner digits, we get 5 digits that can be counted.
Total = 16 edge digits + 5 inner digits = 21.
";
}