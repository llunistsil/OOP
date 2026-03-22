using InventorySystem.Core.Enums;
using InventorySystem.Patterns.Strategy.Enums;
using InventorySystem.Patterns.Strategy.Enhancement;

namespace InventorySystem.Core.Items.Armors;

public class ArmorGear : GearBase, Interfaces.IEquippable, Interfaces.IEnhanceable
{
    public int DefenseRating { get; protected set; }
    public EquipmentSlot EquipSlot { get; protected set; }
    public bool IsEquippedState { get; protected set; }
    public int Tier { get; protected set; }
    public int MaxTier { get; protected set; }

    private readonly IEnhancementStrategy _enhancementStrategy;

    public ArmorGear(string name, uint weight, RarityLevel rarity, int defenseRating, EquipmentSlot slot, string description, EnhancementMode mode = EnhancementMode.Standard, int maxTier = 30)
        : base(name, weight, GearType.Armor, rarity, description)
    {
        DefenseRating = defenseRating;
        EquipSlot = slot;
        IsEquippedState = false;
        Tier = 1;
        MaxTier = maxTier;
        _enhancementStrategy = EnhancementStrategy.Get(mode);
    }

    public bool CanImproveRarity() => _enhancementStrategy.CanImprove(Tier, Rarity);

    public override string GetDetails() => base.GetDetails() + $" | Defense: {DefenseRating} | Tier: {Tier}";

    public override string Activate()
    {
        if (!IsEquippedState)
            return "Must be equipped first";
        return "Cannot be used directly - provides passive defense";
    }

    public void Wear()
    {
        if (!IsEquippedState)
            IsEquippedState = true;
    }

    public void Remove()
    {
        if (IsEquippedState)
            IsEquippedState = false;
    }

    public void Enhance()
    {
        Tier++;
        DefenseRating = (int)(DefenseRating * 1.12);
        Rarity = _enhancementStrategy.TryGetNextRarity(Tier, Rarity);
    }
}
