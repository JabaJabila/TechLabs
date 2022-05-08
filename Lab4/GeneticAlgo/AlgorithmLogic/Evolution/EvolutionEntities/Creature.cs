using AlgorithmLogic.Evolution.Moves;
using AlgorithmLogic.Map;

namespace AlgorithmLogic.Evolution.EvolutionEntities;

public class Creature
{
    private const int MaxHealthPoints = 100;
    private int _healthPoints;

    public Creature(Location position, Chromosome chromosome)
    {
        Position = position ?? throw new ArgumentNullException(nameof(position));
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
    
    public Location Position { get; set; }
    
    public Chromosome Chromosome { get; }
    
    public Move? Move { get; private set; }

    public void MakeMove()
    {
        Chromosome.ExecuteGene(this);
    }
}