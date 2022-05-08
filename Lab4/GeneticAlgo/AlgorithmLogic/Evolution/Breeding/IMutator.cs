using AlgorithmLogic.Configuration;
using AlgorithmLogic.Evolution.EvolutionEntities;

namespace AlgorithmLogic.Evolution.Breeding;

public interface IMutator
{
    Chromosome Mutate(Chromosome chromosome, ICreatureConfiguration creatureConfiguration);
}