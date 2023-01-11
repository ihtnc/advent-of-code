using AdventOfCode.Shared;

namespace AdventOfCode.App.Y2022.Problems.Day10;

public class Part1Description : Description
{
    public override string Text =>
@"Given a list of instructions, determine the sum of the Signal Strength of the following cycles: 20, 60, 100, 140, 180, and 220 using the following rules:
1. The CPU that performs the instruction runs in cycles. This is a unit of time where something happens.
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
13. The Signal Strength is the product of the cycle number and the value of the X register at that time.
14. Calculate the sum of the Signal Strength of the 20th, 60th, 100th, 140th, 180th, and 220th cycles.";

    public override string Example =>
@"Given: noop addx 2 addx -1 noop
Output: 0";

    public override string Explanation => @"
|-------|-------------|------------|-----------------|----------------------------------------------------------|
| Cycle | Instruction | X Register | Signal Strength | Notes                                                    |
|-------|-------------|------------|-----------------|----------------------------------------------------------|
|   1   | noop        |          1 |        1        | noop is completed here as it only takes 1 cycle          |
|   2   | addx 2      |          1 |        2        | The CPU is now free to run the next instruction (addx 2) |
|   3   |             |          1 |        3        | The addition will only take effect on the next cycle     |
|   4   | addx -1     |          3 |       12        | The +2 to the X register from addx 2 takes effect        |
|   5   |             |          3 |       15        | Signal Strength = cycle number * X register at this time |
|   6   | noop        |          2 |       12        | The -1 to the X register from addx -1 takes effect       |
|-------|-------------|------------|-----------------|----------------------------------------------------------|

Sum of the Signal Strength of cycles 20, 60, 100, 140, 180, 220: 0 (0 + 0 + 0 + 0 + 0 + 0).

Note: The example is only to illustrate the behaviours of the CPU as it runs the instructions and how the Signal Strength for each cycle is calculated.";
}