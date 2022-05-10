using AlgorithmLogic.Configuration;
using AlgorithmLogic.Evolution.EvolutionEntities;
using AlgorithmLogic.Tools.Loggers;

namespace AlgorithmLogic.Evolution;

public interface IEvolutionAlgorithm
{
    Population GenerateStarterPopulation();
    Population RunGeneration(int number, Population population);
}