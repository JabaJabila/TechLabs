using AlgorithmLogic.Configuration;
using AlgorithmLogic.Tools.Loggers;

namespace AlgorithmLogic.Evolution;

public interface IEvolutionAlgorithm
{
    void RunIterations(int n);
    void RunInfinityLoop();
}