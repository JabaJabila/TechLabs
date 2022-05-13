using AlgorithmLogic.Evolution.EvolutionEntities;
using AlgorithmLogic.Moves;
using Action = AlgorithmLogic.Moves.Action;

namespace AlgorithmLogic.Genes.ActiveGenes;

public class WalkingGene : ActiveGene
{
    protected override int DomainCode { get; } = 1;
    public override int EnergyCost { get; } = 8;

    public WalkingGene(int subCode) : base(subCode)
    {
    }

    public override void Execute(Creature creature)
    {
        creature.Health -= EnergyCost;
        var (x, y) = MovePositions[SubCode];
        creature.Move = new Move(creature.Location.MoveOn(x, y), Action.Walk);
        creature.Chromosome.CurrentGenePosition++;
    }
}