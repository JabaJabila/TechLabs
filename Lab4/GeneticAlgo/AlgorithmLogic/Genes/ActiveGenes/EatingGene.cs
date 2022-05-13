using AlgorithmLogic.Evolution.EvolutionEntities;
using AlgorithmLogic.Moves;
using Action = AlgorithmLogic.Moves.Action;

namespace AlgorithmLogic.Genes.ActiveGenes;

public class EatingGene : ActiveGene
{
    public EatingGene(int subCode) : base(subCode)
    {
    }

    protected override int DomainCode { get; } = 2;
    public override int EnergyCost { get; } = 5;
    
    public override void Execute(Creature creature)
    {
        creature.Health -= EnergyCost;
        var (x, y) = MovePositions[SubCode];
        creature.Move = new Move(creature.Location.MoveOn(x, y), Action.UnsafeInteract);
        creature.Chromosome.CurrentGenePosition++;
    }
}