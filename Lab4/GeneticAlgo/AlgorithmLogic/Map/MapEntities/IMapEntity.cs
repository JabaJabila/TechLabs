using AlgorithmLogic.Evolution.EvolutionEntities;

namespace AlgorithmLogic.Map.MapEntities;

public interface IMapEntity
{
    Location Location { get; set; }
    void Interact(Creature creature, IMapInspector mapInspector);
}