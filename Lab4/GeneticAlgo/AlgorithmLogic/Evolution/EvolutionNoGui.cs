using AlgorithmLogic.Configuration;
using AlgorithmLogic.Evolution.Breeding;
using AlgorithmLogic.Evolution.Environment;
using AlgorithmLogic.Genes;
using AlgorithmLogic.Map;
using AlgorithmLogic.Map.MapEntities;
using AlgorithmLogic.Tools.Loggers;

namespace AlgorithmLogic.Evolution;

public class EvolutionNoGui : IEvolutionAlgorithm
{
    private readonly IConfiguration _configuration;
    private readonly IProgressLogger _logger;
    private readonly IBreeder _breeder;
    private readonly IGeneFactory _geneFactory;
    private readonly IMapInspector _mapInspector;
    private readonly IEnvironmentInspector _environmentInspector;

    public EvolutionNoGui(IConfiguration configuration, IProgressLogger logger)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _breeder = new BreederOnDistance(configuration, configuration, new RandomMutator());
        _geneFactory = new GeneCreator();
        _mapInspector = new ListMapInspector(configuration);
        _environmentInspector = new CommonEnvironmentInspector(configuration);
    }

    public void RunIterations(int n)
    {
        throw new NotImplementedException();
    }

    public void RunInfinityLoop()
    {
        throw new NotImplementedException();
    }

    private void RunSingleIteration()
    {
        // TODO
    }
}