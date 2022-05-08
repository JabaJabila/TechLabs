using AlgorithmLogic.Evolution.EvolutionEntities;
using AlgorithmLogic.Genes;
using AlgorithmLogic.Tools.Exceptions;

namespace AlgorithmLogic.Evolution.Breeding;

public interface IBreeder
{
    Population BreedPopulation(Population population, IGeneFactory geneFactory);
}