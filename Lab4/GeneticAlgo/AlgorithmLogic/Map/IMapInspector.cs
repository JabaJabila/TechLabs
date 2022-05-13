using AlgorithmLogic.Evolution.EvolutionEntities;
using AlgorithmLogic.Map.MapEntities;

namespace AlgorithmLogic.Map;

public interface IMapInspector
{
    Location GetFreeLocation();
    
    IReadOnlyCollection<IMapEntity> Entities { get; }

    IMapEntity? GetEntityFromMap(Location location);
    void DeleteEntityFromMap(Location location);
    void DeleteEntityFromMap(IMapEntity entity);

    void GenerateStarterFood();
    void GenerateStarterPoison();

    void RegenerateFoodAfterIteration();
    void RegeneratePoisonAfterIteration();
    
    void GeneratePopulationOnMap(Population population);

    void ConvertPoisonToFood(IMapEntity entity);

    void ClearMap();
}