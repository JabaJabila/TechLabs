namespace AlgorithmLogic.Configuration;

public interface ICreatureConfiguration
{
    uint GenesInChromosome { get; }
    float PartnerSimilarity { get; }
    float ParthenogenesisMutationProbability { get; }
    float FertilizationMutationProbability { get; }
}