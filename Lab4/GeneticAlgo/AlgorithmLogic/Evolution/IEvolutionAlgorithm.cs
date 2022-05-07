using AlgorithmLogic.Evolution.Configuration;
using AlgorithmLogic.Evolution.Environment;

namespace AlgorithmLogic.Evolution;

public interface IEvolutionAlgorithm
{
    IEnvironment GenerateEnvironment(IConfiguration configuration);
}