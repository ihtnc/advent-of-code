namespace AdventOfCode.App.Y2022.Problems.Day9;

public class RopeHead : RopeSegment
{
    public Coordinate Move(Command command)
    {
        var isReverse = command.Steps < 0;

        var steps = Math.Abs(command.Steps);
        for(var i = 0; i < steps; i++)
        {
            var xOffset = 0;
            var yOffset = 0;

            switch (command.CommandDirection)
            {
                case Direction.D: yOffset = -1; break;
                case Direction.U: yOffset = 1; break;
                case Direction.L: xOffset = -1; break;
                case Direction.R: xOffset = 1; break;
                default: return this.Location;
            }

            if (isReverse)
            {
                xOffset = xOffset * -1;
                yOffset = yOffset * -1;
            }

            Location = new Coordinate
            {
                X = Location.X + xOffset,
                Y = Location.Y + yOffset
            };

            Child?.MoveTowardsParent();
        }

        return Location;
    }
}