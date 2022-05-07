﻿using AlgorithmLogic.Evolution.Configuration;
using AlgorithmLogic.Tools.Exceptions;

namespace AlgorithmLogic.Map;

public class Location : IEquatable<Location>
{
    private static uint _xLimit;
    private static uint _yLimit;
    private static bool _configured;
    
    public Location(uint x, uint y)
    {
        if (!_configured) 
            throw new GeneticAlgoException("Can't create location until Location.SetConfigurations() is done");
        
        X = x;
        Y = y;
    }
    
    public uint X { get; }
    public uint Y { get; }

    public static void SetConfiguration(IMapConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration, nameof(configuration));
        _xLimit = configuration.MapWidth;
        _yLimit = configuration.MapHeight;
        _configured = true;
    }

    public Location MoveOn(int x, int y)
    {
        return new Location((uint)((x + X) % _xLimit), (uint)((y + Y) % _yLimit));
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