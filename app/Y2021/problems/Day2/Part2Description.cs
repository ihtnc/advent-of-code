using AdventOfCode.Shared;

namespace AdventOfCode.App.Y2021.Problems.Day2;

public class Part2Description : Description
{
    public override string Text =>
@"Given a list of commands, perform the following rules:
1. Start with POSITION=0, DEPTH=0, AIM=0.
2. [Forward X] increases POSITION by X and DEPTH by AIM multiplied by X.
3. [Down X] increases AIM by X.
4. [Up X] decreases AIM by X.
5. Multiply the final values of POSITION and DEPTH.";

    public override string Example =>
@"Given: [Forward 1, Down 3, Forward 2, Up 1]
Output: 18";

    public override string Explanation =>
@"Current values: POSITION=0 DEPTH=0 AIM=0.
[Forward 1] increases POSITION by 1 (new values: POSITION=1 DEPTH=0 AIM=0).
It didn't increase DEPTH since AIM is still 0. AIM (0) multiplied by X (1) is 0.
[Down 3] increases AIM by 3 (new values: POSITION=1 DEPTH=0 AIM=3).
[Forward 2] increases POSITION by 2 (new values: POSITION=3 DEPTH=0 AIM=3).
It also increases DEPTH by 6. AIM (3) multiplied by X (2) is 6 (new values: POSITION=3 DEPTH=6 AIM=3).
[Up 1] decreases AIM by 1 (new values: POSITION=3 DEPTH=6 AIM=2).
Output is POSITION x DEPTH which is 18.";
}