using AdventOfCode.Shared;

namespace AdventOfCode.App.Y2021.Problems.Day2;

public class Part1Description : Description
{
    public override string Text =>
@"Given a list of commands, perform the following rules:
1. Start with POSITION=0 and DEPTH=0.
2. [Forward X] increases POSITION by X.
3. [Down X] increases DEPTH by X.
4. [Up X] decreases DEPTH by X.
5. Multiply the final values of POSITION and DEPTH.";

    public override string Example =>
@"Given: [Forward 1, Down 3, Forward 2, Up 1]
Output: 6";

    public override string Explanation =>
@"Current values: POSITION=0 DEPTH=0.
[Forward 1] increases POSITION by 1 (new values: POSITION=1 DEPTH=0).
[Down 3] increases DEPTH by 3 (new values: POSITION=1 DEPTH=3).
[Forward 2] increases POSITION by 2 (new values: POSITION=3 DEPTH=3).
[Up 1] decreases DEPTH by 1 (new values: POSITION=3 DEPTH=2).
Output is POSITION x DEPTH which is 6.";
}