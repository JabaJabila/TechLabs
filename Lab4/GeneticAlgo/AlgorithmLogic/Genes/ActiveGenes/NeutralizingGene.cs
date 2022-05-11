using AlgorithmLogic.Evolution.EvolutionEntities;
using AlgorithmLogic.Moves;
using Action = AlgorithmLogic.Moves.Action;

namespace AlgorithmLogic.Genes.ActiveGenes;

public class NeutralizingGene : ActiveGene
{
    public NeutralizingGene(int subCode) : base(subCode)
    {
    }

    protected override int DomainCode { get; } = 3;
    public override int EnergyCost { get; } = 6;
    public override void Execute(Creature creature)
    {
        creature.Health -= EnergyCost;
        var (x, y) = MovePositions[SubCode];
        creature.Move = new Move(creature.Location.MoveOn(x, y), Action.SafeInteract);
        creature.Chromosome.CurrentGenePosition++;
    }
}