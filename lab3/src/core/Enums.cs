namespace InventorySystem.Core.Enums;

public enum GearType
{
    Weapon,
    Armor,
    Consumable,
    QuestItem
}

public enum RarityLevel
{
    Ordinary = 1,
    Uncommon,
    Premium,
    Epic,
    Mythic
}

public enum EquipmentSlot
{
    Head,
    Chest,
    Hands,
    PrimaryHand,
    SecondaryHand,
    Legs
}

public enum BuffType
{
    HealthRestore,
    ManaRestore,
    PowerIncrease,
    DefenseIncrease,
    AgilityIncrease,
    Toxic,
    Regen,
    Stealth,
    FireProtection,
    FrostProtection
}

public enum DurationType
{
    Immediate,
    Timed,
    Permanent
}
