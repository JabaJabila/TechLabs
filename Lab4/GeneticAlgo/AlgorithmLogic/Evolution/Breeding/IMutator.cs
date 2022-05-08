using AlgorithmLogic.Configuration;
using AlgorithmLogic.Evolution.EvolutionEntities;
using AlgorithmLogic.Genes;

namespace AlgorithmLogic.Evolution.Breeding;

public interface IMutator
{
    Chromosome MutateFertile(Chromosome chromosome, ICreatureConfiguration creatureConfiguration, IGeneFactory geneFactory);
    Chromosome MutateLonely(Chromosome chromosome, ICreatureConfiguration creatureConfiguration, IGeneFactory geneFactory);
}