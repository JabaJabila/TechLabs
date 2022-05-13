using AlgorithmLogic.Evolution.EvolutionEntities;
using AlgorithmLogic.Tools.Exceptions;

namespace AlgorithmLogic.Genes.ActiveGenes;

public abstract class ActiveGene : IGene
{
    protected static readonly (int, int)[] MovePositions = 
        { (0, 1), (1, 1), (1, 0), (1, -1), (0, -1), (-1, -1), (-1, 0), (-1, 1) };

    protected ActiveGene(int subCode)
    {
        if (subCode < 0 || subCode >= MovePositions.Length)
            throw new GeneticAlgoException("Active gene modifier must be in range 0..7");
        
        SubCode = subCode;
    }

    protected abstract int DomainCode { get; }
    protected int SubCode { get; }
    public int Code => DomainCode * 10 + SubCode;
    public abstract int EnergyCost { get; }
    public abstract void Execute(Creature creature);
}