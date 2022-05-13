namespace AlgorithmLogic.Configuration;

public interface IMapConfiguration
{
    int MapHeight { get; }
    int MapWidth { get; }
    
    float StarterFoodPercentage { get; }
    float StarterPoisonPercentage { get; }
    float FoodPerRoundRegeneration { get; }
    float PoisonPerRoundRegeneration { get; }
    float MaxFoodPercentage { get; }
    float MaxPoisonPercentage { get; }
}