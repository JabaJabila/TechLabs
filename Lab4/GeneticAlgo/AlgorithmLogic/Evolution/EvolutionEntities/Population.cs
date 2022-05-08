using AlgorithmLogic.Evolution.Configuration;
using AlgorithmLogic.Genes;
using AlgorithmLogic.Map;

namespace AlgorithmLogic.Evolution.EvolutionEntities;

public class Population
{
    private readonly IPopulationConfiguration _configuration;
    private readonly Creature[] _creatures;

    public Population(
        IPopulationConfiguration populationConfiguration,
        ICreatureConfiguration creatureConfiguration,
        IGeneFactory geneFactory,
        IMapInspector mapInspector,
        uint generationNumber = 1)
    {
        _configuration = populationConfiguration ?? throw new ArgumentNullException(nameof(populationConfiguration));
        ArgumentNullException.ThrowIfNull(creatureConfiguration, nameof(creatureConfiguration));
        ArgumentNullException.ThrowIfNull(geneFactory, nameof(geneFactory));
        ArgumentNullException.ThrowIfNull(mapInspector, nameof(mapInspector));
        GenerationNumber = generationNumber;
        
        _creatures = new Creature[_configuration.PopulationAmount];
        
        for (var i = 0; i < _creatures.Length; i++)
        {
            _creatures[i] = new Creature(
                mapInspector.GetFreeLocation(),
                new Chromosome(creatureConfiguration.GenesInChromosome, geneFactory));
        }
    }
    
    public uint GenerationNumber { get; }

    public IReadOnlyCollection<Creature> AllCreatures => _creatures;
    
    public IReadOnlyCollection<Creature> AliveCreatures => _creatures.Where(c => c.IsAlive).ToList();
    
    public IReadOnlyCollection<Creature> DeadCreatures => _creatures.Where(c => !c.IsAlive).ToList();

    public bool IsInBreedZone => AliveCreatures.Count <= _configuration.PopulationBreedZone;
}