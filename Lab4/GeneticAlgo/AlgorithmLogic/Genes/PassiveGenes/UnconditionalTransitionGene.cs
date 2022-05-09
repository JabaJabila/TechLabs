using AlgorithmLogic.Evolution.EvolutionEntities;

using AlgorithmLogic.Tools.Exceptions;
using AlgorithmLogic.Moves;
using Action = AlgorithmLogic.Moves.Action;

namespace AlgorithmLogic.Genes.PassiveGenes;

public class UnconditionalTransitionGene : IGene
{
    private const int TransitionFactor = 5;
    private const int MaxStep = 7;
    private readonly int _step;
    
    public UnconditionalTransitionGene(int step)
    {
        if (step is < 0 or >= MaxStep)
            throw new GeneticAlgoException("Active gene modifier must be in range 0..7");

        _step = step;
    }

    public int Code => 7 * 10 + _step;
    public int EnergyCost { get; } = 3;

    public void Execute(Creature creature)
    {
        creature.Health -= EnergyCost;
        creature.Move = new Move(creature.Location, Action.Idle);
        creature.Chromosome.CurrentGenePosition += _step * TransitionFactor;
    }
}