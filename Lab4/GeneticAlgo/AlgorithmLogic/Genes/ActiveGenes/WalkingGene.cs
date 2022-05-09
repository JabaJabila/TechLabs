using AlgorithmLogic.Evolution.EvolutionEntities;

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
        // TODO: Move
    }
}