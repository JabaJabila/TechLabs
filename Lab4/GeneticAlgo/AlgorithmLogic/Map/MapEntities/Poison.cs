using AlgorithmLogic.Evolution.EvolutionEntities;

namespace AlgorithmLogic.Map.MapEntities;

public class Poison : IMapEntity
{
    public Poison(Location location)
    {
        Location = location ?? throw new ArgumentNullException(nameof(location));
    }

    public Location Location { get; set; }
    
    public void Interact(Creature creature, IMapInspector mapInspector)
    {
        ArgumentNullException.ThrowIfNull(creature, nameof(creature));
        ArgumentNullException.ThrowIfNull(mapInspector, nameof(mapInspector));
        
        creature.Health = 0;
        mapInspector.DeleteEntityFromMap(this);
    }
}