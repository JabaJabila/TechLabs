using System;
using System.Windows.Controls;
using AlgorithmLogic.Configuration;
using AlgorithmLogic.Evolution;
using AlgorithmLogic.Evolution.Breeding;
using AlgorithmLogic.Evolution.Environment;
using AlgorithmLogic.Evolution.EvolutionEntities;
using AlgorithmLogic.Genes;
using AlgorithmLogic.Map.MapEntities;
using AlgorithmLogic.Tools.Extensions;
using AlgorithmLogic.Tools.Loggers;

namespace EvolutionSimulatorApp.GeneticAlgorithmGuiLogic;

public class EvolutionAlgorithmGui : IEvolutionAlgorithm
{
    private readonly IConfiguration _configuration;
    private readonly IProgressLogger _logger;
    private readonly IBreeder _breeder;
    private readonly IGeneFactory _geneFactory;
    private readonly IEnvironmentInspector _environmentInspector;

    public EvolutionAlgorithmGui(IConfiguration configuration, IProgressLogger logger, uint cellSize, Canvas canvas, int waitTimeMs)
    {
        ArgumentNullException.ThrowIfNull(canvas, nameof(canvas));
        ArgumentNullException.ThrowIfNull(logger, nameof(logger));
        ArgumentNullException.ThrowIfNull(configuration, nameof(configuration));

        var drawer = new SimpleDrawer(cellSize, canvas, waitTimeMs);
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _breeder = new BreederOnDistance(configuration, configuration, new RandomMutator());
        _geneFactory = new GeneCreator();
        _environmentInspector = new EnvironmentInspectorWrapper(configuration, drawer);
        Location.SetConfiguration(configuration);
    }

    public Population GenerateStarterPopulation()
    {
        return new Population(_configuration, _configuration, _geneFactory);
    }

    public Population RunGeneration(int number, Population population)
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