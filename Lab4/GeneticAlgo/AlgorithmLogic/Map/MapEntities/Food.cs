using AlgorithmLogic.Evolution.EvolutionEntities;

namespace AlgorithmLogic.Map.MapEntities;

public class Food : IMapEntity
{
    public Food(Location location)
    {
        Location = location ?? throw new ArgumentNullException(nameof(location));
    }

    public Location Location { get; set; }
    
    public void Interact(Creature creature, IMapInspector mapInspector)
    {
        ArgumentNullException.ThrowIfNull(creature, nameof(creature));
        ArgumentNullException.ThrowIfNull(mapInspector, nameof(mapInspector));
        creature.Health = Creature.MaxHealthPoints;
        mapInspector.DeleteEntityFromMap(this);
    }
}