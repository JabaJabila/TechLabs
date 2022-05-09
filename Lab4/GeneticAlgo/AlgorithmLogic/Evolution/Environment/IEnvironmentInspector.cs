using AlgorithmLogic.Evolution.EvolutionEntities;

namespace AlgorithmLogic.Evolution.Environment;

public interface IEnvironmentInspector
{
    void GenerateEnvironment(Population population);
    
    void HandleIteration(Population population);
}