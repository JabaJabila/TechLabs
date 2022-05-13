using AlgorithmLogic.Evolution.EvolutionEntities;

namespace AlgorithmLogic.Genes;

public interface IGene
{
    int Code { get; }
    int EnergyCost { get; }
    void Execute(Creature creature);
}