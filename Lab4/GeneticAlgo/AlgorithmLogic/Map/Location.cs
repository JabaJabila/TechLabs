using AlgorithmLogic.Evolution.Configuration;
using AlgorithmLogic.Tools.Exceptions;

namespace AlgorithmLogic.Map;

public class Location
{
    private static uint _xLimit;
    private static uint _yLimit;
    private static bool _configured = false;
    
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
}