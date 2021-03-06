using AlgorithmLogic.Configuration;
using AlgorithmLogic.Evolution.EvolutionEntities;
using AlgorithmLogic.Map.MapEntities;
using AlgorithmLogic.Tools.Exceptions;

namespace AlgorithmLogic.Map;

public class ListMapInspector : IMapInspector
{
    private const float MaxPlaceAvailable = .95f;
    private static readonly Random Random;
    private readonly IMapConfiguration _configuration;
    private readonly int _totalSpace;
    private List<IMapEntity> _entities;
    private int _totalFood;
    private int _totalPoison;

    static ListMapInspector()
    {
        Random = new Random((int) DateTime.Now.Ticks & 0x0000FFFF);
    }

    public ListMapInspector(IMapConfiguration configuration)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        _entities = new List<IMapEntity>();
        _totalSpace = _configuration.MapHeight * _configuration.MapWidth;
    }

    public IReadOnlyCollection<IMapEntity> Entities => _entities;

    public Location GetFreeLocation()
    {
        if (_entities.Count >= _totalSpace * MaxPlaceAvailable)
            throw new GeneticAlgoException("Too few free space available on map");
        
        var x = Random.Next(0, _configuration.MapWidth);
        var y = Random.Next(0, _configuration.MapHeight);

        while (IsFreeLocation(x, y))
        {
            x = Random.Next(0, _configuration.MapWidth);
            y = Random.Next(0, _configuration.MapHeight);
        }

        return new Location(x, y);
    }

    private bool IsFreeLocation(int x, int y)
    {
        foreach (var mapEntity in _entities)
        {
            if (mapEntity.Location.X == x && mapEntity.Location.Y == y)
                return true;
        }

        return false;
    }

    public IMapEntity? GetEntityFromMap(Location location)
    {
        foreach (var mapEntity in _entities)
        {
            if (mapEntity.Location.Equals(location))
                return mapEntity;
        }

        return null;
    }

    public void DeleteEntityFromMap(Location location)
    {
        var entity = GetEntityFromMap(location);
        _entities.Remove(entity!);
        
        if (entity is Food)
            _totalFood--;

        if (entity is Poison)
            _totalPoison--;
    }

    public void DeleteEntityFromMap(IMapEntity entity)
    {
        _entities.Remove(entity);
        
        if (entity is Food)
            _totalFood--;

        if (entity is Poison)
            _totalPoison--;
    }

    public void GenerateStarterFood()
    {
        _totalFood = (int) Math.Ceiling(_totalSpace * _configuration.StarterFoodPercentage);

        for (var i = 0; i < _totalFood; i++)
        {
            _entities.Add(new Food(GetFreeLocation()));
        }
    }

    public void GenerateStarterPoison()
    {
        _totalPoison = (int) Math.Floor(_totalSpace * _configuration.StarterPoisonPercentage);
        
        for (var i = 0; i < _totalPoison; i++)
            _entities.Add(new Poison(GetFreeLocation()));
    }

    public void RegenerateFoodAfterIteration()
    {
        var foodToRegenerate = Math.Floor(_totalSpace * _configuration.FoodPerRoundRegeneration);
        foodToRegenerate = foodToRegenerate + _totalFood <= Math.Floor(_totalSpace * _configuration.MaxFoodPercentage)
            ? foodToRegenerate
            : Math.Floor(_totalSpace * _configuration.MaxFoodPercentage) - _totalFood;

        for (var i = 0; i < foodToRegenerate; i++)
        {
            _entities.Add(new Food(GetFreeLocation()));
            _totalFood++;
        }
            
    }

    public void RegeneratePoisonAfterIteration()
    {
        var poisonToRegenerate = Math.Floor(_totalSpace * _configuration.PoisonPerRoundRegeneration);
        poisonToRegenerate = poisonToRegenerate + _totalPoison <= Math.Floor(_totalSpace * _configuration.MaxPoisonPercentage)
            ? poisonToRegenerate
            : Math.Floor(_totalSpace * _configuration.MaxPoisonPercentage) - _totalPoison;

        for (var i = 0; i < poisonToRegenerate; i++)
        {
            _entities.Add(new Poison(GetFreeLocation()));
            _totalPoison++;
        }
    }

    public void GeneratePopulationOnMap(Population population)
    {
        foreach (var creature in population.AllCreatures)
        {
            creature.Location = GetFreeLocation();
            _entities.Add(new CreatureEntity(creature, creature.Location));
        }
    }

    public void ConvertPoisonToFood(IMapEntity entity)
    {
        if (entity is not Poison) return;
        
        DeleteEntityFromMap(entity);
        _entities.Add(new Food(entity.Location));
        _totalFood++;
    }

    public void ClearMap()
    {
        _entities = new List<IMapEntity>();
    }
}