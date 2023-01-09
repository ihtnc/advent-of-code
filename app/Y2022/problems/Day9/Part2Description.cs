using AdventOfCode.Shared;

namespace AdventOfCode.App.Y2022.Problems.Day9;

public class Part2Description : Description
{
    public override string Text =>
@"Given a list of commands, count the number of unique coordinates that the tail end of a rope moved to given the following rules:
1. A command is in the format, ""Direction Value"".
2. Direction can be L, R, D, U which corresponds to Left, Right, Down, Up respectively.
3. Value is an integer which corresponds to the number of steps to take towards the Direction value of the command.
4. The rope consists of 10 segments, the head, 8 segments in between, and the tail.
5. The rope and all its segments start in the same coordinates.
6. The head is the one performing the command and all the other segments just follow behind using certain rules.
7. A segment moves towards the same direction as the segment infront if that segment moves in one direction. For example, if the head moves 1 step up, the segment behind it also moves one step up, then the segment behind that segment also moves one step up, and so on.
8. A segment does not move if the segment infront is on the same coordinates.
8. A segment does not move if the segment infront is adjacent to it on any direction (including diagonals).
9. A segment moves diagonally toward the segment infront if it moves in a coordinate where the segment is no longer adjacent to it.
10. Count the number of unique coordinates which the tail moved to after the head executed all the commands.";

    public override string Example =>
@"Given: R 10
Output: 2";

    public override string Explanation =>
@"Legend:
H=Head
[number]=Segment
T=Tail

Initial state:
1|...........
0|H..........
Y*-----------
*X01234567891

Note: Both the Head and all the other segments are on the same coordinates (0, 0).

Step 1 (Command1 = R 10):
1|...........
0|1H.........
Y*-----------
*X01234567891

Note: Head moves to a new coordinate (1, 0) but all the other segments do not move since they are still adjacent to the head.

Step 2 (Command1 = R 10):
1|...........
0|21H........
Y*-----------
*X01234567891

Note: Head moves to a new coordinate (2, 0), Segment 1 now moves in the direction of the head (1, 0), but all the other segments do not move since they are still adjacent to Segment 1.

Step 3 (Command1 = R 10):
1|...........
0|321H.......
Y*-----------
*X01234567891

Note: Head moves to a new coordinate (3, 0), Segment 1 moves, Segment 2 moves, but all the other segments do not move since they are still adjacent to Segment 2.

Step 4 (Command1 = R 10):
1|...........
0|4321H......
Y*-----------
*X01234567891

Note: Head moves to a new coordinate (4, 0), Segment 1, 2, and 3 moves, but all the other segments do not move since they are still adjacent to Segment 3.

Steps 5 to 9 are similar.

Step 10 (Command1 = R 10):
1|...........
0|.T87654321H
Y*-----------
*X01234567891

Note: Head moves to a new coordinate (10, 0), all the segments move accordingly, including the Tail which moved to a new coordinate (1, 0).

At the end of the command, the Tail has moved to two unique coordinates: (0, 0), (1, 0).";
}