namespace AdventOfCode.App.Y2022.Problems.Day10;

public class Sprite
{
    public int StartIndex { get; private set; } = 0;
    public int EndIndex { get; private set; } = 2;

    public void Move(int index)
    {
        StartIndex = index - 1;
        EndIndex = index + 1;
    }

    public char RenderPixel(int screenWidth, int screenHeight, RegisterSnapshot snapshot)
    {
        var pixelIndex = (snapshot.SnapshotId - 1) % screenWidth;
        Move(snapshot.X);

        if (StartIndex <= pixelIndex && EndIndex >= pixelIndex) { return '#'; }

        return default;
    }
}