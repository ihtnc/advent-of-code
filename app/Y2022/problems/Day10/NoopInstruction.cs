namespace AdventOfCode.App.Y2022.Problems.Day10;

public class NoopInstruction : Instruction
{
    public NoopInstruction() : base(null) { }

    public override int CycleTime { get => 1; }
}