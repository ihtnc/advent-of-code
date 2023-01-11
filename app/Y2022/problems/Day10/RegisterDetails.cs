namespace AdventOfCode.App.Y2022.Problems.Day10;

public class RegisterDetails
{
    public int _snapshotId = 0;
    private int _busyCount = 0;
    private Instruction? _currentInstruction;

    public int X { get; set; } = 1;

    public bool IsBusy { get => _busyCount > 0; }

    private RegisterSnapshot CreateSnapshot() => new RegisterSnapshot
    {
        SnapshotId = ++_snapshotId,
        X = X
    };

    private RegisterSnapshot GetCurrentSnapshot() => new RegisterSnapshot
    {
        SnapshotId = _snapshotId,
        X = X
    };

    public bool Execute(Instruction instruction)
    {
        if (IsBusy) { return false; }

        _currentInstruction = instruction;
        _busyCount = instruction.CycleTime;

        return true;
    }

    public bool TryWaitOne(out RegisterSnapshot currentState)
    {
        if (IsBusy is false || _currentInstruction is null)
        {
            currentState = GetCurrentSnapshot();
            return false;
        }

        _busyCount--;
        currentState = CreateSnapshot();
        return true;
    }

    public bool TryGetResult(out object? result)
    {
        result = null;

        if (IsBusy || _currentInstruction is null) { return false; }

        switch(true)
        {
            case var _ when _currentInstruction is AddXInstruction addx:
                var data = addx.Data != null && addx.Data is int ? (int)addx.Data : 0;
                result = data;
                break;

            case var _ when _currentInstruction is NoopInstruction noop:
            default:
                break;
        }

        _currentInstruction = null;
        return true;
    }
}