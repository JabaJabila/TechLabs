using AlgorithmLogic.Configuration;
using AlgorithmLogic.Evolution.Breeding;
using AlgorithmLogic.Evolution.Environment;
using AlgorithmLogic.Evolution.EvolutionEntities;
using AlgorithmLogic.Genes;
using AlgorithmLogic.Map.MapEntities;
using AlgorithmLogic.Tools.Extensions;
using AlgorithmLogic.Tools.Loggers;

namespace AlgorithmLogic.Evolution;

public class EvolutionNoGui : IEvolutionAlgorithm
{
    private readonly IConfiguration _configuration;
    private readonly IProgressLogger _logger;
    private readonly IBreeder _breeder;
    private readonly IGeneFactory _geneFactory;
    private readonly IEnvironmentInspector _environmentInspector;

    public EvolutionNoGui(IConfiguration configuration, IProgressLogger logger)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _breeder = new BreederOnDistance(configuration, configuration, new RandomMutator());
        _geneFactory = new GeneCreator();
        _environmentInspector = new CommonEnvironmentInspector(configuration);
        Location.SetConfiguration(configuration);
    }

    public void RunGenerations(int n)
    {
        var population = new Population(_configuration, _configuration, _geneFactory);
        for (var i = 1; i <= n; i++ )
            population = RunSingleGeneration(i, population);
    }

    public void RunGenerationsInfinityLoop()
    {
        var i = 1;
        var population = new Population(_configuration, _configuration, _geneFactory);
        
        while (true)
            population = RunSingleGeneration(i++, population);
    }

    private Population RunSingleGeneration(int number, Population population)
    {
        _environmentInspector.GenerateEnvironment(population);
        var iterationsSurvived = 0;

        while (!population.IsInBreedZone)
        {
            _environmentInspector.HandleIteration(population);
            iterationsSurvived++;
        }
        
        _logger.LogProgress(GenerateStringMessage(population, number, iterationsSurvived));
        var newPopulation = _breeder.BreedPopulation(population, _geneFactory);

        return newPopulation;
    }

    private static string GenerateStringMessage(Population population, int generation, int iterations)
    {
        return $"Generation #{generation}: iterations survived: {iterations}\n{population.GenerationInfo()}";
    }
}