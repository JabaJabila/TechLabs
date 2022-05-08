﻿using AlgorithmLogic.Configuration;
using AlgorithmLogic.Evolution.EvolutionEntities;
using AlgorithmLogic.Tools.Exceptions;

namespace AlgorithmLogic.Map.MapEntities;

public class ListMapInspector : IMapInspector
{
    private const float MinPlaceAvailable = .05f;
    private static readonly Random Random;
    private readonly IMapConfiguration _configuration;
    private readonly uint _totalSpace;
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

    public Location GetFreeLocation()
    {
        if (_entities.Count >= _totalSpace * MinPlaceAvailable)
            throw new GeneticAlgoException("Too few free space available on map");
        
        var x = (uint) Random.Next(0, (int) _configuration.MapWidth);
        var y = (uint) Random.Next(0, (int) _configuration.MapHeight);
        var freeLocation = new Location(x, y);

        while (_entities.Select(e => e.Location).Contains(freeLocation))
        {
            x = (uint) Random.Next(0, (int) _configuration.MapWidth);
            y = (uint) Random.Next(0, (int) _configuration.MapHeight);
            freeLocation = new Location(x, y);
        }

        return freeLocation;
    }
    
    public IMapEntity? GetEntityFromMap(Location location)
    {
        return _entities.FirstOrDefault(e => e.Location.Equals(location));
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
            _entities.Add(new Food(GetFreeLocation()));
    }

    public void GenerateStarterPoison()
    {
        _totalPoison = (int) Math.Floor(_totalSpace * _configuration.StarterFoodPercentage);
        
        for (var i = 0; i < _totalPoison; i++)
            _entities.Add(new Poison(GetFreeLocation()));
    }

    public void RegenerateFoodAfterIteration()
    {
        var foodToRegenerate = Math.Floor(_totalFood * _configuration.FoodPerRoundRegeneration);
        foodToRegenerate = foodToRegenerate + _totalFood <= Math.Floor(_totalFood * _configuration.MaxFoodPercentage)
            ? foodToRegenerate
            : _totalFood - Math.Floor(_totalFood * _configuration.MaxFoodPercentage);
        
        for (var i = 0; i < foodToRegenerate; i++)
            _entities.Add(new Food(GetFreeLocation()));
    }

    public void RegeneratePoisonAfterIteration()
    {
        var poisonToRegenerate = Math.Floor(_totalPoison * _configuration.PoisonPerRoundRegeneration);
        poisonToRegenerate = poisonToRegenerate + _totalPoison <= Math.Floor(_totalPoison * _configuration.MaxPoisonPercentage)
            ? poisonToRegenerate
            : _totalPoison - Math.Floor(_totalPoison * _configuration.MaxPoisonPercentage);
        
        for (var i = 0; i < poisonToRegenerate; i++)
            _entities.Add(new Food(GetFreeLocation()));
    }

    public void GeneratePopulationOnMap(Population population)
    {
        foreach (var creature in population.AllCreatures)
            creature.Location = GetFreeLocation();
    }

    public void ClearMap()
    {
        _entities = new List<IMapEntity>();
    }
}