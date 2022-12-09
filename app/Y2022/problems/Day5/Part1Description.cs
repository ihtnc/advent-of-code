using AdventOfCode.Shared;

namespace AdventOfCode.App.Y2022.Problems.Day5;

public class Part1Description : Description
{
    public override string Text =>
@"Given an initial layout of stacks and a list of commands, identify all the items at the top of each stack after perfoming all the commands using the following rules:
1. Each command is in the format: ""MOVE X FROM Y TO Z"".
2. Remove X items from stack Y.
3. Add each item removed on top of stack Z.
4. Items are added one at a time and in the order they are removed.";

    public override string Example =>
@"Given:
    [D]
[N] [C]
[Z] [M] [P]
 1   2   3

move 1 from 2 to 1
move 3 from 1 to 3
move 2 from 2 to 1
move 1 from 1 to 2

Output: CMZ";

    public override string Explanation =>
@"There are 3 stacks:
|-----------|
|  Stacks   |
|-----------|
| 1 | 2 | 3 |
|---|---|---|
|   | D |   |
| N | C |   |
| Z | M | P |
|-----------|

The first command is to move 1 item from stack 2 to stack 1 which would result in the following:
|-----------|
|  Stacks   |
|-----------|
| 1 | 2 | 3 |
|---|---|---|
| D |   |   |
| N | C |   |
| Z | M | P |
|-----------|

The second command is to move 3 items from stack 1 to stack 3 which would result in the following:
|-----------|
|  Stacks   |
|-----------|
| 1 | 2 | 3 |
|---|---|---|
|   |   | Z |
|   |   | N |
|   | C | D |
|   | M | P |
|-----------|
Note that items are only moved one at a time and each are added on top of the target stack.

After performing the remaining commands the result will be the following:
|-----------|
|  Stacks   |
|-----------|
| 1 | 2 | 3 |
|---|---|---|
|   |   | Z |
|   |   | N |
|   |   | D |
| C | M | P |
|-----------|

The top items on each stack is CMZ = C (stack 1), M (stack 2), and Z (stack 3).";
}