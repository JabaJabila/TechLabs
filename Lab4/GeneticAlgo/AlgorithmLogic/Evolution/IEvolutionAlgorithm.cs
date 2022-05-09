using AlgorithmLogic.Configuration;
using AlgorithmLogic.Tools.Loggers;

namespace AlgorithmLogic.Evolution;

public interface IEvolutionAlgorithm
{
    void RunGenerations(int n);
    void RunGenerationsInfinityLoop();
}