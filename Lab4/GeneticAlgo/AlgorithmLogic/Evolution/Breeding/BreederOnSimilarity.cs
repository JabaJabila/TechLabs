using AlgorithmLogic.Configuration;
using AlgorithmLogic.Evolution.EvolutionEntities;
using AlgorithmLogic.Tools.Exceptions;

namespace AlgorithmLogic.Evolution.Breeding;

public class BreederOnSimilarity : IBreeder
{
    private readonly IPopulationConfiguration _populationConfiguration;
    private readonly ICreatureConfiguration _creatureConfiguration;
    private readonly IMutator _mutator;

    public BreederOnSimilarity(
        IPopulationConfiguration populationConfiguration,
        ICreatureConfiguration creatureConfiguration,
        IMutator mutator)
    {
        _populationConfiguration = populationConfiguration ?? 
                                   throw new ArgumentNullException(nameof(populationConfiguration));
        _creatureConfiguration = creatureConfiguration ??
                                 throw new ArgumentNullException(nameof(creatureConfiguration));
        _mutator = mutator ?? throw new ArgumentNullException(nameof(mutator));
    }

    public Population BreedPopulation(Population population)
    {
        ArgumentNullException.ThrowIfNull(population, nameof(population));

        if (population.AliveCreatures.Count == 0) 
            throw new GeneticAlgoException("Impossible to breed. No creatures alive");
        
        if (!population.IsInBreedZone) throw new GeneticAlgoException("Population is not ready for breeding");

        var newGeneration = new List<Creature>();
        var fertilePairs = GetFertilePairs(population, out var lonelyCreatures);
        var toReproduce = _populationConfiguration.PopulationAmount / (fertilePairs.Count + lonelyCreatures.Count);
        
        foreach (var (creature1, creature2) in fertilePairs)
            BreedPairs(creature1, creature2, toReproduce, newGeneration);

        foreach (var lonelyCreature in lonelyCreatures)
            BreedLonely(lonelyCreature, toReproduce, newGeneration);

        toReproduce = _populationConfiguration.PopulationAmount - newGeneration.Count;
        
        if (toReproduce > 0)
            return new Population(_populationConfiguration, newGeneration.ToArray());
        
        if (fertilePairs.Count > 0)
            BreedPairs(fertilePairs.First().Item1, fertilePairs.First().Item2, toReproduce, newGeneration);
        else
            BreedLonely(lonelyCreatures.First(), toReproduce, newGeneration);

        return new Population(_populationConfiguration, newGeneration.ToArray());
    }

    private void BreedLonely(Creature lonelyCreature, long toReproduce, List<Creature> newGeneration)
    {
        throw new NotImplementedException();
    }

    private void BreedPairs(Creature creature1, Creature creature2, long toReproduce, List<Creature> newGeneration)
    {
        throw new NotImplementedException();
    }

    private List<(Creature, Creature)> GetFertilePairs(Population population, out List<Creature> lonely)
    {
        throw new NotImplementedException();
    }
}