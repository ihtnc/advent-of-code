using AdventOfCode.Shared;

namespace AdventOfCode.App.Y2022.Problems.Day10;

public class Part2Description : Description
{
    public override string Text =>
@"Given a list of instructions, render the screen using the following rules:
1. The screen is 40 pixels wide and 6 pixels long.
2. The screen renders 1 pixel per CPU cycle. This is a unit of time where something happens.
3. Cycles 1 to 40 corresponds to rendering the pixels on the first line of the screen from left to right.
4. Likewise, cycles 201 to 240 corresponds to the sixth line of the screen.
5. The pixels on the each line starts at position 0 and ends at position 39.
6. Pixels can only be lit [#] or or unlit [.].

The pixel is either lit or unlit based on the following rules:
1. The screen uses a sprite that moves along a reference line as basis of what to render.
2. The pixels in the reference line are all initially unlit.
3. The state of the pixels in the reference line persists across CPU cycles.
4. The reference line starts at position 0 and ends at position 39 (40 pixels wide).
5. The sprite is a group of 3 consecutive lit pixels that moves together.
6. The sprite starts on the left most side of the line. This means the first 3 pixels of the reference line are lit.
7. The screen moves the sprite along the reference line by changing the position of the middle pixel of the sprite to the value of the X register in the curent CPU cycle.
8. When the sprite moves, the 3 pixels from its previous position are unlit.
9. When a specific pixel in the screen is being rendered, it copies the state of the pixel on the reference line in the exact same position as the pixel being rendered.
10. To put it simply, if a pixel on position 4 of a certain line on the screen is being rendered, it will be lit or unlit based on whether the pixel on position 4 of the reference line is lit or unlit.

The X register for each cycle is calculated using the following rules:
1. The CPU that performs the instruction runs in cycles.
2. The CPU cycle starts at 1.
3. If the CPU starts running an instruction that takes 2 cycles to complete, it can only run another instruction after 2 cycles.
4. The CPU has 1 register (X) that can store values and persist it across all cycles.
5. The X register has an initial value of 1.
6. Instructions can only be ""addx [number]"" or ""noop"".
7. The noop instruction takes 1 cycle to complete.
8. The noop instruction doesn't do anything.
9. The ""addx [number]"" instruction takes 2 cycles to complete.
10. The addx instruction adds the [number] value to the X register.
11. After its completion, the operation from the addx instruction takes effect on the next cycle.
12. The [number] value on the addx instruction can only be integers but can be negative values.
";
    public override string Example =>
@"Given: noop addx 2 addx -1 noop
Output:
#####...................................
........................................
........................................
........................................
........................................
........................................";

    public override string Explanation => @"
|-------|-------------|------------|------------------------------------------|
| Cycle | Instruction | X Register | Reference line                           |
|-------|-------------|------------|------------------------------------------|
|   1   | noop        |          1 | ###..................................... |
|   2   | addx 2      |          1 | ###..................................... |
|   3   |             |          1 | ###..................................... |
|   4   | addx -1     |          3 | ..###................................... |
|   5   |             |          3 | ..###................................... |
|   6   | noop        |          2 | .###.................................... |
|-------|-------------|------------|------------------------------------------|


Cycle 1: The pixel to render on the screen is at line 1 position 0. The pixel at position 0 of the reference line for this cycle is lit [#] so the pixel rendered onscreen is lit.
Cycle 2: The pixel to render on the screen is at line 1 position 1. The pixel at position 1 of the reference line for this cycle is lit [#] so the pixel rendered onscreen is lit.
Cycle 6: The pixel to render on the screen is at line 1 position 5. The pixel at position 5 of the reference line for this cycle is unlit [.] so the pixel rendered onscreen is unlit.
There are no more instructions so the remaining pixels on the screen remain unlit.";
}