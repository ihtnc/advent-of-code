namespace AdventOfCode.App.Y2022.Problems.Day10;

public class AddXInstruction : Instruction
{
    public AddXInstruction(object? data) : base(data) { }
    public override int CycleTime { get => 2; }
}