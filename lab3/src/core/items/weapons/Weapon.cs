using InventorySystem.Core.Enums;
using InventorySystem.Patterns.Strategy.Enums;
using InventorySystem.Patterns.Strategy.Enhancement;

namespace InventorySystem.Core.Items.Weapons;

public class WeaponGear : GearBase, Interfaces.IEquippable, Interfaces.IEnhanceable
{
    public int AttackPower { get; private set; }
    public EquipmentSlot EquipSlot { get; private set; }
    public bool IsEquippedState { get; private set; }
    public int Tier { get; private set; }
    public int MaxTier { get; private set; }

    private readonly IEnhancementStrategy _enhancementStrategy;

    public WeaponGear(string name, uint weight, RarityLevel rarity, int attackPower, EquipmentSlot slot, string description, EnhancementMode mode = EnhancementMode.Standard, int maxTier = 30)
        : base(name, weight, GearType.Weapon, rarity, description)
    {
        AttackPower = attackPower;
        EquipSlot = slot;
        IsEquippedState = false;
        Tier = 1;
        MaxTier = maxTier;
        _enhancementStrategy = EnhancementStrategy.Get(mode);
    }

    public bool CanImproveRarity() => _enhancementStrategy.CanImprove(Tier, Rarity);

    public override string GetDetails() => base.GetDetails() + $" | Attack: {AttackPower} | Tier: {Tier}";

    public override string Activate()
    {
        if (!IsEquippedState)
            return "Must be equipped first";
        return $"Dealt {AttackPower} damage to target";
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
        AttackPower = (int)(AttackPower * 1.15);
        Rarity = _enhancementStrategy.TryGetNextRarity(Tier, Rarity);
    }
}
