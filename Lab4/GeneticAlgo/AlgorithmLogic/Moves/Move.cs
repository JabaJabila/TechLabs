using AlgorithmLogic.Map.MapEntities;

namespace AlgorithmLogic.Moves;

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