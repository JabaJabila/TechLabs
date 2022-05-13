using AlgorithmLogic.Evolution.EvolutionEntities;
using AlgorithmLogic.Genes;

namespace AlgorithmLogic.Evolution.Breeding;

public interface IBreeder
{
    Population BreedPopulation(Population population, IGeneFactory geneFactory);
}