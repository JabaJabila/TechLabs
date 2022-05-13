using AlgorithmLogic.Evolution.EvolutionEntities;

namespace AlgorithmLogic.Tools.Loggers;

public interface IProgressLogger
{
    void LogProgress(Population population, int generation, int iterations);
}