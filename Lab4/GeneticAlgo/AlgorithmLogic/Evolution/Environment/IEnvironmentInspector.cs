using AlgorithmLogic.Evolution.EvolutionEntities;
using AlgorithmLogic.Map.MapEntities;

namespace AlgorithmLogic.Evolution.Environment;

public interface IEnvironmentInspector
{
    void GenerateEnvironment(Population population);
    
    void HandleIteration(Population population);
    
    IReadOnlyCollection<IMapEntity> Entities { get; }
}