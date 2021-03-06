using System.Text;
using AlgorithmLogic.Evolution.EvolutionEntities;

namespace AlgorithmLogic.Tools.Extensions;

public static class PopulationExtension
{
    public static string GenerationInfo(this Population population)
    {
        var sb = new StringBuilder("Population info:\n");
        var aliveCreatures = population.AllCreatures.Where(c => c.IsAlive).ToArray();

        sb.Append(population.IsInBreedZone ? "> lifecycle finished!\n" : "> lifecycle is active!\n");

        sb.Append($"> survived creatures ({aliveCreatures.Length}):\n");
            
        foreach (var creature in aliveCreatures)
        {
            sb.Append($">> {creature.CreatureGenotypeString()}\n");
        }

        return sb.ToString();
    }
}