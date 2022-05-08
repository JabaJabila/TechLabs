using AlgorithmLogic.Evolution.EvolutionEntities;

namespace AlgorithmLogic.Evolution.Breeding;

public interface IBreeder
{
    Population BreedPopulation(Population population);
}