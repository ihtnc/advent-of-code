namespace AdventOfCode.Shared;

public interface IProblem
{
    object Solve(RunOption option);
    string Url { get; }
    Description GetDescription(DescribeOption option);
}