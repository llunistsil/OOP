using InventorySystem.Core.Enums;

namespace InventorySystem.Patterns.Strategy.Enhancement;

public class CustomEnhancementStrategy : StandardEnhancementStrategy
{
    private Func<RarityLevel, int> _thresholdFunc;

    public CustomEnhancementStrategy(Func<RarityLevel, int> thresholdFunc) => _thresholdFunc = thresholdFunc;

    protected override int GetMaxTierForRarity(RarityLevel rarity)
        => _thresholdFunc(rarity);
}
