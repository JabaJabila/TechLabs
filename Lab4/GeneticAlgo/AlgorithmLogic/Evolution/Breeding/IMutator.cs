using AlgorithmLogic.Evolution.EvolutionEntities;

namespace AlgorithmLogic.Evolution.Breeding;

public interface IMutator
{
    void Mutate(Chromosome chromosome);
}