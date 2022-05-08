using AlgorithmLogic.Evolution.EvolutionEntities;

namespace AlgorithmLogic.Map.MapEntities;

public class Poison : IMapEntity
{
    public Poison(Location location)
    {
        Location = location ?? throw new ArgumentNullException(nameof(location));
    }

    public Location Location { get; set; }
    
    public void Interact(Creature creature)
    {
        ArgumentNullException.ThrowIfNull(creature, nameof(creature));
        creature.Health = 0;
    }
}