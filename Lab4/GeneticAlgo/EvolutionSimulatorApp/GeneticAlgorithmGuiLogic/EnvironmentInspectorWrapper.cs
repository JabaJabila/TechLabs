using System;
using System.Collections.Generic;
using AlgorithmLogic.Configuration;
using AlgorithmLogic.Evolution.Environment;
using AlgorithmLogic.Evolution.EvolutionEntities;
using AlgorithmLogic.Map.MapEntities;

namespace EvolutionSimulatorApp.GeneticAlgorithmGuiLogic;

public class EnvironmentInspectorWrapper : IEnvironmentInspector
{
    private readonly IEnvironmentInspector _wrappedInspector;
    private readonly IDrawer _drawer;
    
    public EnvironmentInspectorWrapper(IConfiguration configuration, IDrawer drawer)
    {
        _drawer = drawer ?? throw new ArgumentNullException(nameof(drawer));
        _wrappedInspector = new CommonEnvironmentInspector(configuration);
    }
    
    public void GenerateEnvironment(Population population)
    {
        _wrappedInspector.GenerateEnvironment(population);
        _drawer.Draw(Entities);
    }

    public void HandleIteration(Population population)
    {
        _wrappedInspector.HandleIteration(population);
        _drawer.Draw(Entities);
    }

    public IReadOnlyCollection<IMapEntity> Entities => _wrappedInspector.Entities;
}