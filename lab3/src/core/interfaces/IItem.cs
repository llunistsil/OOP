using InventorySystem.Core.Enums;

namespace InventorySystem.Core.Interfaces;

public interface IGear : INamable
{
    string Id { get; }
    uint Weight { get; }
    GearType GearType { get; }
    RarityLevel Rarity { get; }
    string Description { get; }

    string Activate();
    string GetDetails();
}
