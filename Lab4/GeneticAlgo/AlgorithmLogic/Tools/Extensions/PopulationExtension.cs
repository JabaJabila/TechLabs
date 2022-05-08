using System.Text;
using AlgorithmLogic.Evolution.EvolutionEntities;

namespace AlgorithmLogic.Tools.Extensions;

public static class PopulationExtension
{
    public static string GenerationInfo(this Population population)
    {
        var sb = new StringBuilder($"Generation #{population.GenerationNumber}:\n");
        var aliveCreatures = population.AliveCreatures;

        if (population.IsInBreedZone)
        {
            sb.Append("> lifecycle finished!");
            sb.Append($"> survived creatures ({aliveCreatures.Count}):\n");
            
            foreach (var creature in aliveCreatures)
            {
                sb.Append($">> {creature.ToString()}");
            }

            return sb.ToString();
        }
        
        var deadCreatures = population.DeadCreatures;
        
        sb.Append("> lifecycle is active!");
        sb.Append($"> survived creatures ({aliveCreatures.Count}):\n");
            
        foreach (var creature in aliveCreatures)
        {
            sb.Append($">> {creature.ToString()}");
        }
        
        sb.Append($"> dead creatures ({deadCreatures}):\n");
            
        foreach (var creature in deadCreatures)
        {
            sb.Append($">> {creature.ToString()}");
        }

        return sb.ToString();
    }
}