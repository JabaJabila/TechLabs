using System.Text;
using AlgorithmLogic.Evolution.EvolutionEntities;

namespace AlgorithmLogic.Tools.Extensions;

public static class CreatureExtension
{
    public static string CreatureGenotypeString(this Creature creature)
    {
        var sb = new StringBuilder("[");
        var genes = creature.Chromosome.Genotype;

        sb.Append(genes.First().Code);
        
        foreach (var gene in genes.Skip(1))
        {
            sb.Append($", {gene.Code}");
        }

        return sb.ToString();
    }
}