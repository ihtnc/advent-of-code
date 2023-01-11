namespace AdventOfCode.App.Y2022.Problems.Day10;

public abstract class Instruction
{
    public Instruction(object? data) { Data = data; }
    public abstract int CycleTime { get; }
    public virtual object? Data { get; }
}