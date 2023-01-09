using AdventOfCode.Shared;

namespace AdventOfCode.App.Y2022.Problems.Day9;

public class Part1Description : Description
{
    public override string Text =>
@"Given a list of commands, count the number of unique coordinates that the tail end of a rope moved to given the following rules:
1. A command is in the format, ""Direction Value"".
2. Direction can be L, R, D, U which corresponds to Left, Right, Down, Up respectively.
3. Value is an integer which corresponds to the number of steps to take towards the Direction value of the command.
4. The rope consists of 2 segments, the head and the tail.
5. The rope and all its segments start in the same coordinates.
6. The head is the one performing the command and the tail just follow behind using certain rules.
7. The tail moves towards the same direction as the head if the head moves in one direction. For example, if the head moves 1 step up, the tail also moves one step up.
8. The tail does not move if the head is on the same coordinates.
8. The tail does not move if the head is adjacent to it on any direction (including diagonals).
9. The tail moves diagonally toward the head if the head moves in a coordinate where the tail is no longer adjacent to it.
10. Count the number of unique coordinates which the tail moved to after the head executed all the commands.";

    public override string Example =>
@"Given: R 2 U 2
Output: 3";

    public override string Explanation =>
@"Legend:
H=Head
T=Tail

Initial state:
2|....
1|....
0|H...
Y*----
*X0123

Note: Both the Head and Tail are on the same coordinates (0, 0).

Step 1 (Command1 = R 2):
2|....
1|....
0|TH..
Y*----
*X0123

Note: Head moves to a new coordinate (1, 0) but the Tail does not move since it is still adjacent to the head.

Step 2 (Command1 = R 2):
2|....
1|....
0|.TH.
Y*----
*X0123

Note: Head moves to a new coordinate (2, 0) and the Tail now moves in the direction of the head (1, 0).

Step 3 (Command2 = U 2):
2|....
1|..H.
0|.T..
Y*----
*X0123

Note: Head moves to a new coordinate (2, 1) but the Tail does not move since it is still adjacent to the head.

Step 4 (Command2 = U 2):
2|..H.
1|..T.
0|....
Y*----
*X0123

Note: Head moves to a new coordinate (2, 2) and the Tail now moves diagonally in the direction of the head (2, 1).

At the end of the command, the Tail has moved to three unique coordinates: (0, 0), (1, 0), and (2, 1).";
}