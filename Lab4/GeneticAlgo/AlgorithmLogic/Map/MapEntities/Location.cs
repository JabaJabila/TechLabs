using AlgorithmLogic.Configuration;
using AlgorithmLogic.Tools.Exceptions;

namespace AlgorithmLogic.Map.MapEntities;

public class Location : IEquatable<Location>
{
    private static int _xLimit;
    private static int _yLimit;
    private static bool _configured;
    
    public Location(int x = 0, int y = 0)
    {
        if (!_configured) 
            throw new GeneticAlgoException("Can't create location until Location.SetConfigurations() is done");
        
        X = x % _xLimit;
        Y = y;
    }
    
    public int X { get; }
    public int Y { get; }

    public static void SetConfiguration(IMapConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration, nameof(configuration));
        _xLimit = configuration.MapWidth;
        _yLimit = configuration.MapHeight;
        _configured = true;
    }

    public Location MoveOn(int x, int y)
    {
        return new Location((int) ((x + X) % _xLimit), (int) ((y + Y) % _yLimit));
    }

    public int DistanceTo(Location other)
    {
        var x1 = X - other.X;
        var y1 = Y - other.Y;

        return (int) Math.Floor(Math.Sqrt(x1 * x1 + y1 * y1));
    }

    public bool Equals(Location? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return X == other.X && Y == other.Y;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == GetType() && Equals((Location) obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }
}