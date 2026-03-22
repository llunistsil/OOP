using InventorySystem.Core.Enums;
using InventorySystem.Patterns.Strategy.Enums;

namespace InventorySystem.Patterns.Strategy.Enhancement;

public class EnhancementStrategy
{
    public static IEnhancementStrategy Get(EnhancementMode mode)
    {
        return mode switch
        {
            EnhancementMode.Tier10 => new CustomEnhancementStrategy(GetTier10Threshold),
            EnhancementMode.Tier5 => new CustomEnhancementStrategy(GetTier5Threshold),
            EnhancementMode.Standard => new StandardEnhancementStrategy(),
            _ => new StandardEnhancementStrategy()
        };
    }

    private static int GetTier10Threshold(RarityLevel rarity)
        => rarity switch
        {
            >= RarityLevel.Ordinary and <= RarityLevel.Mythic => (int)rarity * (int)EnhancementMode.Tier10,
            _ => (int)EnhancementMode.Tier10
        };

    private static int GetTier5Threshold(RarityLevel rarity)
        => rarity switch
        {
            >= RarityLevel.Ordinary and <= RarityLevel.Mythic => (int)rarity * (int)EnhancementMode.Tier5,
            _ => (int)EnhancementMode.Tier5
        };
}
