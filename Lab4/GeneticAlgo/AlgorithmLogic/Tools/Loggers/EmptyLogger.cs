using AlgorithmLogic.Evolution.EvolutionEntities;

namespace AlgorithmLogic.Tools.Loggers;

public class EmptyLogger : IProgressLogger
{
    public void LogProgress(Population population, int generation, int iterations)
    {
    }
}