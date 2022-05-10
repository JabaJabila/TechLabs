using AlgorithmLogic.Evolution.EvolutionEntities;
using AlgorithmLogic.Moves;
using Action = AlgorithmLogic.Moves.Action;

namespace AlgorithmLogic.Genes.ActiveGenes;

public class ConditionalTransitionGene : ActiveGene
{
    public ConditionalTransitionGene(int subCode) : base(subCode)
    {
    }
    
    protected override int DomainCode { get; } = 5;
    public int Code => DomainCode * 10 + SubCode;
    public override int EnergyCost { get; } = 4;
    public override void Execute(Creature creature)
    {
        creature.Health -= EnergyCost;
        var (x, y) = MovePositions[SubCode];
        creature.Move = new Move(creature.Location.MoveOn(x, y), Action.Inspect);
    }

    
}