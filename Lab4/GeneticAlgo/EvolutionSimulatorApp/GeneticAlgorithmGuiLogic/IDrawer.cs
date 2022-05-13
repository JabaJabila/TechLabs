using System.Collections.Generic;
using AlgorithmLogic.Map.MapEntities;

namespace EvolutionSimulatorApp.GeneticAlgorithmGuiLogic;

public interface IDrawer
{
    void Draw(IReadOnlyCollection<IMapEntity> entities);
}