using AlgorithmLogic.Evolution.EvolutionEntities;

namespace AlgorithmLogic.Map.MapEntities;

public class Food : IMapEntity
{
    public Food(Location location)
    {
        Location = location ?? throw new ArgumentNullException(nameof(location));
    }

    public Location Location { get; set; }
    
    public void Interact(Creature creature)
    {
        ArgumentNullException.ThrowIfNull(creature, nameof(creature));
        creature.Health = Creature.MaxHealthPoints;
    }
}