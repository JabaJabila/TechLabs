using AlgorithmLogic.Evolution.EvolutionEntities;

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
        // TODO: Move
    }
}