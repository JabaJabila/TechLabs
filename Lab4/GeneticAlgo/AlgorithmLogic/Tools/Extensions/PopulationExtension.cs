using System.Text;
using AlgorithmLogic.Evolution.EvolutionEntities;

namespace AlgorithmLogic.Tools.Extensions;

public static class PopulationExtension
{
    public static string GenerationInfo(this Population population)
    {
        var sb = new StringBuilder("Population info:\n");
        var aliveCreatures = population.AliveCreatures;

        if (population.IsInBreedZone)
        {
            sb.Append("> lifecycle finished!\n");
            sb.Append($"> survived creatures ({aliveCreatures.Count}):\n");
            
            foreach (var creature in aliveCreatures)
            {
                sb.Append($">> {creature.CreatureGenotypeString()}\n");
            }

            return sb.ToString();
        }
        
        var deadCreatures = population.DeadCreatures;
        
        sb.Append("> lifecycle is active!\n");
        sb.Append($"> survived creatures ({aliveCreatures.Count}):\n");
            
        foreach (var creature in aliveCreatures)
        {
            sb.Append($">> {creature.CreatureGenotypeString()}\n");
        }
        
        sb.Append($"> dead creatures ({deadCreatures}):\n");
            
        foreach (var creature in deadCreatures)
        {
            sb.Append($">> {creature.CreatureGenotypeString()}\n");
        }

        return sb.ToString();
    }
}