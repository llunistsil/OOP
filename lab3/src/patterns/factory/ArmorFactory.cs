using InventorySystem.Core.Enums;
using InventorySystem.Core.Items.Armors;
using InventorySystem.Patterns.Strategy.Enums;

namespace InventorySystem.Patterns.Factory;

public abstract class ArmorGearFactory
{
    protected string Name { get; init; }
    protected uint Weight { get; init; }
    protected RarityLevel Rarity { get; init; }
    protected int DefenseRating { get; init; }
    protected string Description { get; init; }
    protected EnhancementMode Mode { get; init; }

    public ArmorGearFactory(string name, uint weight, RarityLevel rarity, int defenseRating, string description, EnhancementMode mode = EnhancementMode.Standard)
    {
        Name = name;
        Weight = weight;
        Rarity = rarity;
        DefenseRating = defenseRating;
        Description = description;
        Mode = mode;
    }

    public abstract ArmorGear CreateArmor();
}

public abstract class HeadGearFactory(string name, uint weight, RarityLevel rarity, int defenseRating, string description, EnhancementMode mode = EnhancementMode.Standard)
    : ArmorGearFactory(name, weight, rarity, defenseRating, description, mode)
{
    public override ArmorGear CreateArmor()
        => new ArmorGear(Name, Weight, Rarity, DefenseRating, EquipmentSlot.Head, Description, Mode);
}

public abstract class ChestGearFactory(string name, uint weight, RarityLevel rarity, int defenseRating, string description, EnhancementMode mode = EnhancementMode.Standard)
    : ArmorGearFactory(name, weight, rarity, defenseRating, description, mode)
{
    public override ArmorGear CreateArmor()
        => new ArmorGear(Name, Weight, Rarity, DefenseRating, EquipmentSlot.Chest, Description, Mode);
}

public abstract class LegGearFactory(string name, uint weight, RarityLevel rarity, int defenseRating, string description, EnhancementMode mode = EnhancementMode.Standard)
    : ArmorGearFactory(name, weight, rarity, defenseRating, description, mode)
{
    public override ArmorGear CreateArmor()
        => new ArmorGear(Name, Weight, Rarity, DefenseRating, EquipmentSlot.Legs, Description, Mode);
}

public abstract class HandGearFactory(string name, uint weight, RarityLevel rarity, int defenseRating, string description, EnhancementMode mode = EnhancementMode.Standard)
    : ArmorGearFactory(name, weight, rarity, defenseRating, description, mode)
{
    public override ArmorGear CreateArmor()
        => new ArmorGear(Name, Weight, Rarity, DefenseRating, EquipmentSlot.Hands, Description, Mode);
}
