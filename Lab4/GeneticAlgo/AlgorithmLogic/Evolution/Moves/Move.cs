using AlgorithmLogic.Map;

namespace AlgorithmLogic.Evolution.Moves;

public class Move
{
    public Move(Location location, Action action)
    {
        Location = location;
        Action = action;
    }
    
    public Location Location { get; }
    public Action Action { get; }
}