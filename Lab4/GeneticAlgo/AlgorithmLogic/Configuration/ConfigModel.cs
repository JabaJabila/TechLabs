namespace AlgorithmLogic.Configuration;

public class ConfigModel : IConfiguration
{
    public ConfigModel(
        int mapHeight,
        int mapWidth,
        float starterFoodPercentage,
        float starterPoisonPercentage,
        float foodPerRoundRegeneration, 
        float poisonPerRoundRegeneration, 
        float maxFoodPercentage, 
        float maxPoisonPercentage,
        uint populationAmount,
        uint populationBreedZone, 
        uint genesInChromosome, 
        float partnerMaxDistance, 
        float parthenogenesisMutationProbability,
        float fertilizationMutationProbability)
    {
        MapHeight = mapHeight;
        MapWidth = mapWidth;
        StarterFoodPercentage = starterFoodPercentage;
        StarterPoisonPercentage = starterPoisonPercentage;
        FoodPerRoundRegeneration = foodPerRoundRegeneration;
        PoisonPerRoundRegeneration = poisonPerRoundRegeneration;
        MaxFoodPercentage = maxFoodPercentage;
        MaxPoisonPercentage = maxPoisonPercentage;
        PopulationAmount = populationAmount;
        PopulationBreedZone = populationBreedZone;
        GenesInChromosome = genesInChromosome;
        PartnerMaxDistance = partnerMaxDistance;
        ParthenogenesisMutationProbability = parthenogenesisMutationProbability;
        FertilizationMutationProbability = fertilizationMutationProbability;
    }

    public int MapHeight { get; }
    public int MapWidth { get; }
    public float StarterFoodPercentage { get; }
    public float StarterPoisonPercentage { get; }
    public float FoodPerRoundRegeneration { get; }
    public float PoisonPerRoundRegeneration { get; }
    public float MaxFoodPercentage { get; }
    public float MaxPoisonPercentage { get; }
    public uint PopulationAmount { get; }
    public uint PopulationBreedZone { get; }
    public uint GenesInChromosome { get; }
    public float PartnerMaxDistance { get; }
    public float ParthenogenesisMutationProbability { get; }
    public float FertilizationMutationProbability { get; }
}