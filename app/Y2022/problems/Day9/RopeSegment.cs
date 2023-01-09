namespace AdventOfCode.App.Y2022.Problems.Day9;

public class RopeSegment
{
    private Coordinate _location;

    public Coordinate Location
    {
        get => _location;
        protected set
        {
            _location = value;
            OnLocationChanged?.Invoke(this, new RopeSegmentLocationChangedEventArgs
            {
                Value = value
            });
        }
    }
    public RopeSegment? Parent { get; init; }
    public RopeSegment? Child { get; private set; }

    public event EventHandler<RopeSegmentLocationChangedEventArgs>? OnLocationChanged;

    public RopeSegment AddChild()
    {
        return AddChild(Location);
    }

    public RopeSegment AddChild(Coordinate location)
    {
        var child = new RopeSegment
        {
            Location = location,
            Parent = this
        };

        Child = child;
        return child;
    }

    public virtual void MoveTowardsParent()
    {
        if (Parent is null) { return; }

        var newLocation = CalculateNextStep(Parent.Location);

        Location = newLocation;

        if (Child is not null)
        {
            Child.MoveTowardsParent();
        }
    }

    public Coordinate CalculateNextStep(Coordinate parentLocation)
    {
        var current = Location;
        var parent = parentLocation;

        switch (true)
        {
            case var _ when Math.Abs(current.X - parent.X) <= 1 && Math.Abs(current.Y - parent.Y) <= 1:
                return current;

            case var _ when current.X == parent.X:
                current.Y = (current.Y < parent.Y) ? current.Y + 1 : current.Y - 1;
                break;

            case var _ when current.Y == parent.Y:
                current.X = (current.X < parent.X) ? current.X + 1 : current.X - 1;
                break;

            default:
                current.X = (current.X < parent.X) ? current.X + 1 : current.X - 1;
                current.Y = (current.Y < parent.Y) ? current.Y + 1 : current.Y - 1;
                break;
        }

        return current;
    }
}