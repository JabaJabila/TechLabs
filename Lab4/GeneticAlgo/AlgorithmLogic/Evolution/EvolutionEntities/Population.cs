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
        IGeneFactory geneFactory,
        uint generationNumber = 1)
    {
        _configuration = populationConfiguration ?? throw new ArgumentNullException(nameof(populationConfiguration));
        ArgumentNullException.ThrowIfNull(creatureConfiguration, nameof(creatureConfiguration));
        ArgumentNullException.ThrowIfNull(geneFactory, nameof(geneFactory));
        GenerationNumber = generationNumber;
        
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
        Creature[] creatures,
        uint generationNumber = 1)
    {
        _configuration = populationConfiguration ?? throw new ArgumentNullException(nameof(populationConfiguration));
        _creatures = creatures ?? throw new ArgumentNullException(nameof(creatures));
        GenerationNumber = generationNumber;
    }
    
    public uint GenerationNumber { get; }

    public IReadOnlyCollection<Creature> AllCreatures => _creatures;
    
    public IReadOnlyCollection<Creature> AliveCreatures => _creatures.Where(c => c.IsAlive).ToList();
    
    public IReadOnlyCollection<Creature> DeadCreatures => _creatures.Where(c => !c.IsAlive).ToList();

    public bool IsInBreedZone => AliveCreatures.Count <= _configuration.PopulationBreedZone;
}