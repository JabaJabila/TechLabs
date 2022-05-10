using AlgorithmLogic.Evolution.EvolutionEntities;

namespace AlgorithmLogic.Evolution;

public interface IEvolutionAlgorithm
{
    Population GenerateStarterPopulation();
    Population RunGeneration(int number, Population population);
}