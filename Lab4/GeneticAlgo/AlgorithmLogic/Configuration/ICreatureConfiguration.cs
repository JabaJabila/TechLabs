namespace AlgorithmLogic.Configuration;

public interface ICreatureConfiguration
{
    uint GenesInChromosome { get; }
    float PartnerMaxDistance { get; }
    float ParthenogenesisMutationProbability { get; }
    float FertilizationMutationProbability { get; }
}