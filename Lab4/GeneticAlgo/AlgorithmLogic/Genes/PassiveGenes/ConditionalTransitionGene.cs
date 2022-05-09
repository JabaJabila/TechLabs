using AlgorithmLogic.Evolution.EvolutionEntities;
using AlgorithmLogic.Moves;
using Action = AlgorithmLogic.Moves.Action;

namespace AlgorithmLogic.Genes.PassiveGenes;

public class ConditionalTransitionGene : IGene
{
    public int Code { get; } = 0;
    public int EnergyCost { get; } = 4;
    public void Execute(Creature creature)
    {
        creature.Health -= EnergyCost;
        creature.Move = new Move(creature.Location, Action.Inspect);
    }
}