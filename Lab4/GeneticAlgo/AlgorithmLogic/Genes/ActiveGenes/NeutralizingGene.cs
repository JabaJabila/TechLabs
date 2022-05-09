using AlgorithmLogic.Evolution.EvolutionEntities;

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
        // TODO: Move
    }
}