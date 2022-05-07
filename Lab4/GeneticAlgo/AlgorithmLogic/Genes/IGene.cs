using AlgorithmLogic.Evolution.EvolutionEntities;

namespace AlgorithmLogic.Genes;

public interface IGene
{
    int Code { get; }
    void Execute(Creature creature);
}