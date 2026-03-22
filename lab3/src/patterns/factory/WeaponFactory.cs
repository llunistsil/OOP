using InventorySystem.Core.Enums;
using InventorySystem.Core.Items.Weapons;
using InventorySystem.Patterns.Strategy.Enums;

namespace InventorySystem.Patterns.Factory;

public abstract class WeaponGearFactory
{
    protected string Name { get; init; }
    protected uint Weight { get; init; }
    protected RarityLevel Rarity { get; init; }
    protected int AttackPower { get; init; }
    protected string Description { get; init; }
    protected EnhancementMode Mode { get; init; }

    public WeaponGearFactory(string name, uint weight, RarityLevel rarity, int attackPower, string description, EnhancementMode mode = EnhancementMode.Standard)
    {
        Name = name;
        Weight = weight;
        Rarity = rarity;
        AttackPower = attackPower;
        Description = description;
        Mode = mode;
    }

    public abstract WeaponGear CreateWeapon();
}

public class PrimaryWeaponFactory(string name, uint weight, RarityLevel rarity, int attackPower, string description, EnhancementMode mode = EnhancementMode.Standard)
    : WeaponGearFactory(name, weight, rarity, attackPower, description, mode)
{
    public override WeaponGear CreateWeapon()
        => new WeaponGear(Name, Weight, Rarity, AttackPower, EquipmentSlot.PrimaryHand, Description, Mode);
}

public class SecondaryWeaponFactory(string name, uint weight, RarityLevel rarity, int attackPower, string description, EnhancementMode mode = EnhancementMode.Standard)
    : WeaponGearFactory(name, weight, rarity, attackPower, description, mode)
{
    public override WeaponGear CreateWeapon()
        => new WeaponGear(Name, Weight, Rarity, AttackPower, EquipmentSlot.SecondaryHand, Description, Mode);
}
