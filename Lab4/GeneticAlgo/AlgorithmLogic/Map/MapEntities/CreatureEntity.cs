using AlgorithmLogic.Evolution.EvolutionEntities;

namespace AlgorithmLogic.Map.MapEntities;

public class CreatureEntity : IMapEntity
{
    public CreatureEntity(Creature creature, Location location)
    {
        Creature = creature ?? throw new ArgumentNullException(nameof(creature));
        Location = location ?? throw new ArgumentNullException(nameof(location));
    }

    public Creature Creature { get; }

    public Location Location
    {
        get => Creature.Location;
        set => Creature.Location = value;
    }

    public void Interact(Creature creature, IMapInspector mapInspector)
    {
        ArgumentNullException.ThrowIfNull(creature, nameof(creature));
        ArgumentNullException.ThrowIfNull(mapInspector, nameof(mapInspector));

        if (Creature.Health >= creature.Health)
        {
            creature.Health = 0;
            Creature.Health = Creature.MaxHealthPoints;
            mapInspector.DeleteEntityFromMap(creature.Location);
        }

        else
        {
            Creature.Health = 0;
            creature.Health = Creature.MaxHealthPoints;
            mapInspector.DeleteEntityFromMap(this);
        }
    }
}