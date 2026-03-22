namespace InventorySystem.Patterns.Strategy.Enhancement;

public interface IEnhancementStrategy
{
    bool CanImprove(int tier, Core.Enums.RarityLevel rarity);
    Core.Enums.RarityLevel TryGetNextRarity(int tier, Core.Enums.RarityLevel rarity);
}
