using AlgorithmLogic.Configuration;
using AlgorithmLogic.Evolution.EvolutionEntities;
using AlgorithmLogic.Genes;
using AlgorithmLogic.Map.MapEntities;
using AlgorithmLogic.Tools.Exceptions;

namespace AlgorithmLogic.Evolution.Breeding;

public class BreederOnDistance : IBreeder
{
    private static readonly Random Random;
    private readonly IPopulationConfiguration _populationConfiguration;
    private readonly ICreatureConfiguration _creatureConfiguration;
    private readonly IMutator _mutator;

    static BreederOnDistance()
    {
        Random = new Random((int) DateTime.Now.Ticks & 0x0000FFFF);
    }

    public BreederOnDistance(
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

    public Population BreedPopulation(Population population, IGeneFactory geneFactory)
    {
        ArgumentNullException.ThrowIfNull(population, nameof(population));
        ArgumentNullException.ThrowIfNull(geneFactory, nameof(geneFactory));

        if (population.AliveCreatures.Count == 0) 
            throw new GeneticAlgoException("Impossible to breed. No creatures alive");
        
        if (!population.IsInBreedZone) throw new GeneticAlgoException("Population is not ready for breeding");

        var newGeneration = new List<Creature>();
        var fertilePairs = GetFertilePairs(population, out var lonelyCreatures);
        var toReproduce = _populationConfiguration.PopulationAmount / (fertilePairs.Count + lonelyCreatures.Count);
        
        foreach (var (creature1, creature2) in fertilePairs)
            BreedPairs(creature1, creature2, toReproduce, newGeneration, geneFactory);

        foreach (var lonelyCreature in lonelyCreatures)
            BreedLonely(lonelyCreature, toReproduce, newGeneration, geneFactory);

        toReproduce = _populationConfiguration.PopulationAmount - newGeneration.Count;
        
        if (toReproduce > 0)
            return new Population(_populationConfiguration, newGeneration.ToArray());
        
        if (fertilePairs.Count > 0)
            BreedPairs(fertilePairs.First().Item1, fertilePairs.First().Item2, toReproduce, newGeneration, geneFactory);
        else
            BreedLonely(lonelyCreatures.First(), toReproduce, newGeneration, geneFactory);

        return new Population(_populationConfiguration, newGeneration.ToArray());
    }

    private void BreedLonely(Creature lonelyCreature, long toReproduce, List<Creature> newGeneration, IGeneFactory geneFactory)
    {
        for (var i = 0; i < toReproduce; i++)
        {
            newGeneration.Add(new Creature(
                    new Location(),
                    _mutator.MutateLonely(lonelyCreature.Chromosome, _creatureConfiguration, geneFactory)));
        }
    }

    private void BreedPairs(Creature creature1, Creature creature2, long toReproduce, List<Creature> newGeneration, IGeneFactory geneFactory)
    {
        for (var i = 0; i < toReproduce; i++)
        {
            var countGenes = (int) _creatureConfiguration.GenesInChromosome;
            var genotypeSeparator = Random.Next(0, countGenes);
            var currentGenePosition = genotypeSeparator;
            var newGenotype = new IGene[countGenes];
            var genotype1 = creature1.Chromosome.Genotype.ToArray();
            var genotype2 = creature2.Chromosome.Genotype.ToArray();

            while (currentGenePosition != (currentGenePosition + countGenes / 2) % countGenes)
            {
                newGenotype[currentGenePosition] = genotype1[currentGenePosition];
                currentGenePosition = (currentGenePosition + 1) % countGenes;
            }
            
            while (currentGenePosition != genotypeSeparator)
            {
                newGenotype[currentGenePosition] = genotype2[currentGenePosition];
                currentGenePosition = (currentGenePosition + 1) % countGenes;
            }
            
            newGeneration.Add(new Creature(
                new Location(), 
                _mutator.MutateFertile(new Chromosome(newGenotype), _creatureConfiguration, geneFactory)));
        }
    }

    private List<(Creature, Creature)> GetFertilePairs(Population population, out List<Creature> lonely)
    {
        throw new NotImplementedException();
    }
}