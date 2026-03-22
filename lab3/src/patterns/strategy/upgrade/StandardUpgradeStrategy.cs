using InventorySystem.Core.Enums;

namespace InventorySystem.Patterns.Strategy.Enhancement;

public class StandardEnhancementStrategy : IEnhancementStrategy
{
    public bool CanImprove(int tier, RarityLevel rarity) => tier >= GetMaxTierForRarity(rarity);

    public RarityLevel TryGetNextRarity(int tier, RarityLevel rarity)
    {
        if (!CanImprove(tier, rarity))
            return rarity;

        int nextValue = (int)rarity + 1;
        if (!Enum.IsDefined(typeof(RarityLevel), nextValue))
            return rarity;

        return (RarityLevel)nextValue;
    }

    protected virtual int GetMaxTierForRarity(RarityLevel rarity)
        => rarity switch
        {
            >= RarityLevel.Ordinary and <= RarityLevel.Mythic => (int)rarity * 5,
            _ => 5
        };
}
