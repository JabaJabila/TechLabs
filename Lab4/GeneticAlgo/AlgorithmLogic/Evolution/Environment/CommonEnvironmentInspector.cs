using AlgorithmLogic.Configuration;
using AlgorithmLogic.Evolution.EvolutionEntities;
using AlgorithmLogic.Map;
using AlgorithmLogic.Map.MapEntities;
using AlgorithmLogic.Tools.Exceptions;
using Action = AlgorithmLogic.Moves.Action;

namespace AlgorithmLogic.Evolution.Environment;

public class CommonEnvironmentInspector : IEnvironmentInspector
{
    private const int CreatureOffset = 1;
    private const int FoodOffset = 2;
    private const int PoisonOffset = 3;
    private const int DefaultOffset = 4;
    private readonly IMapInspector _mapInspector;
    
    public CommonEnvironmentInspector(IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration, nameof(configuration));
        _mapInspector = new ListMapInspector(configuration);
    }

    public void GenerateEnvironment(Population population)
    {
        ArgumentNullException.ThrowIfNull(population, nameof(population));
        
        _mapInspector.ClearMap();
        _mapInspector.GenerateStarterFood();
        _mapInspector.GenerateStarterPoison();
        _mapInspector.GeneratePopulationOnMap(population);
    }

    public void HandleIteration(Population population)
    {
        ArgumentNullException.ThrowIfNull(population, nameof(population));
        
        foreach (var creature in population.AliveCreatures)
        {
            creature.MakeMove();
            HandleMove(creature);
        }
        
        _mapInspector.RegenerateFoodAfterIteration();
        _mapInspector.RegeneratePoisonAfterIteration();
    }

    private void HandleMove(Creature creature)
    {
        switch (creature.Move!.Action)
        {
            case Action.Idle:
                break;
            case Action.Walk:
                HandleWalk(creature);
                break;
            case Action.SafeInteract:
                HandleSafeInteract(creature);
                break;
            case Action.UnsafeInteract:
                HandleUnsafeInteract(creature);
                break;
            case Action.Inspect:
                HandleInspect(creature);
                break;
            default:
                throw new GeneticAlgoException("Unknown action handle");
        }
    }

    private void HandleInspect(Creature creature)
    {
        var entity = _mapInspector.GetEntityFromMap(creature.Move!.Location);
        var current = creature.Chromosome.CurrentGenePosition;
        creature.Chromosome.CurrentGenePosition = entity switch
        {
            CreatureEntity => current + CreatureOffset,
            Food => current + FoodOffset,
            Poison => current + PoisonOffset,
            _ => current + DefaultOffset
        };
    }

    private void HandleSafeInteract(Creature creature)
    {
        var entity = _mapInspector.GetEntityFromMap(creature.Move!.Location);
        if (entity is Poison)
            _mapInspector.ConvertPoisonToFood(entity);
    }

    private void HandleUnsafeInteract(Creature creature)
    {
        var entity = _mapInspector.GetEntityFromMap(creature.Move!.Location);
        entity?.Interact(creature);
    }

    private void HandleWalk(Creature creature)
    {
        var entity = _mapInspector.GetEntityFromMap(creature.Move!.Location);
        entity?.Interact(creature);
        creature.Location = creature.Move!.Location;
    }
}