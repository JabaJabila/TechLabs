using AlgorithmLogic.Configuration;
using AlgorithmLogic.Genes;
using AlgorithmLogic.Map.MapEntities;

namespace AlgorithmLogic.Evolution.EvolutionEntities;

public class Population
{
    private readonly IPopulationConfiguration _configuration;
    private readonly Creature[] _creatures;

    public Population(
        IPopulationConfiguration populationConfiguration,
        ICreatureConfiguration creatureConfiguration,
        IGeneFactory geneFactory)
    {
        _configuration = populationConfiguration ?? throw new ArgumentNullException(nameof(populationConfiguration));
        ArgumentNullException.ThrowIfNull(creatureConfiguration, nameof(creatureConfiguration));
        ArgumentNullException.ThrowIfNull(geneFactory, nameof(geneFactory));

        _creatures = new Creature[_configuration.PopulationAmount];
        
        for (var i = 0; i < _creatures.Length; i++)
        {
            _creatures[i] = new Creature(
                new Location(),
                new Chromosome(creatureConfiguration.GenesInChromosome, geneFactory));
        }
    }
    
    public Population(
        IPopulationConfiguration populationConfiguration, 
        Creature[] creatures)
    {
        _configuration = populationConfiguration ?? throw new ArgumentNullException(nameof(populationConfiguration));
        _creatures = creatures ?? throw new ArgumentNullException(nameof(creatures));
    }

    public IReadOnlyCollection<Creature> AllCreatures => _creatures;

    public bool IsInBreedZone => AllCreatures.Count(c => c.IsAlive) <= _configuration.PopulationBreedZone;
}