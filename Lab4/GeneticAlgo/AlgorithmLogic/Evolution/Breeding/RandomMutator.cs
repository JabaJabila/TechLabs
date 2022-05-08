using AlgorithmLogic.Configuration;
using AlgorithmLogic.Evolution.EvolutionEntities;
using AlgorithmLogic.Genes;

namespace AlgorithmLogic.Evolution.Breeding;

public class RandomMutator : IMutator
{
    private static readonly Random Random;

    static RandomMutator()
    {
        Random = new Random((int) DateTime.Now.Ticks & 0x0000FFFF);
    }
    
    public Chromosome MutateFertile(
        Chromosome chromosome,
        ICreatureConfiguration creatureConfiguration,
        IGeneFactory geneFactory)
    {
        var genes = chromosome.Genotype.ToArray();

        while (Random.NextDouble() * 100 < creatureConfiguration.FertilizationMutationProbability)
        {
            var position = Random.Next(0, (int) creatureConfiguration.GenesInChromosome);
            genes[position] = geneFactory.CreateRandomGene();
        }

        return new Chromosome(genes);
    }

    public Chromosome MutateLonely(
        Chromosome chromosome,
        ICreatureConfiguration creatureConfiguration,
        IGeneFactory geneFactory)
    {
        var genes = chromosome.Genotype.ToArray();

        while (Random.NextDouble() * 100 < creatureConfiguration.ParthenogenesisMutationProbability)
        {
            var position = Random.Next(0, (int) creatureConfiguration.GenesInChromosome);
            genes[position] = geneFactory.CreateRandomGene();
        }

        return new Chromosome(genes);
    }
}