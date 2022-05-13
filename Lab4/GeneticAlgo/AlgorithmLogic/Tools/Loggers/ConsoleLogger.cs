using AlgorithmLogic.Evolution.EvolutionEntities;
using AlgorithmLogic.Tools.Extensions;

namespace AlgorithmLogic.Tools.Loggers;

public class ConsoleLogger : IProgressLogger
{

    public void LogProgress(Population population, int generation, int iterations)
    {
        Console.WriteLine("------------------------------------------------");
        Console.WriteLine($"Generation #{generation}: iterations survived: {iterations}\n{population.GenerationInfo()}");
    }
}