using AlgorithmLogic.Map.MapEntities;
using AlgorithmLogic.Moves;

namespace AlgorithmLogic.Evolution.EvolutionEntities;

public class Creature
{
    public const int MaxHealthPoints = 100;
    private int _healthPoints;

    public Creature(Location location, Chromosome chromosome)
    {
        Location = location ?? throw new ArgumentNullException(nameof(location));
        Chromosome = chromosome ?? throw new ArgumentNullException(nameof(chromosome));
        IsAlive = true;
        _healthPoints = MaxHealthPoints;
    }

    public int Health
    {
        get => _healthPoints;
        set
        {
            if (!IsAlive) return;
            
            switch (value)
            {
                case < 0:
                    _healthPoints = 0;
                    IsAlive = false;
                    break;
                
                case > 100:
                    _healthPoints = MaxHealthPoints;
                    break;
                
                default:
                    _healthPoints = value;
                    break;
            }
        }
    }
    public bool IsAlive { get; private set; }
    
    public Location Location { get; set; }
    
    public Chromosome Chromosome { get; }
    
    public Move? Move { get; set; }

    public void MakeMove()
    {
        Chromosome.ExecuteGene(this);
    }
}