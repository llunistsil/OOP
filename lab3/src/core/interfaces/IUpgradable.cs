namespace InventorySystem.Core.Interfaces;

public interface IEnhanceable : INamable
{
    int Tier { get; }
    int MaxTier { get; }
    void Enhance();
    bool CanImproveRarity();
}
