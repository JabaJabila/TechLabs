using AlgorithmLogic.Evolution.EvolutionEntities;

namespace AlgorithmLogic.Genes;

public interface IGene
{
    void Execute(Creature creature);
}