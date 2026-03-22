using InventorySystem.Core.Enums;
using InventorySystem.Core.Interfaces;
using InventorySystem.Core.Items.Potions;

namespace InventorySystem.Patterns.Factory;

public abstract class ConsumablePotionFactory
{
    protected string Name { get; init; }
    protected uint Weight { get; init; }
    protected RarityLevel Rarity { get; init; }
    protected string Description { get; init; }
    protected IBuff Buff { get; init; }
    protected int MaxCharges { get; init; }

    public ConsumablePotionFactory(string name, uint weight, RarityLevel rarity, IBuff buff, string description, int maxCharges = 1)
    {
        Name = name;
        Weight = weight;
        Rarity = rarity;
        Description = description;
        Buff = buff;
        MaxCharges = maxCharges;
    }

    public ConsumablePotionFactory(string name, uint weight, RarityLevel rarity, BuffEffectFactory factory, string description, int maxCharges = 1)
        : this(name, weight, rarity, factory.CreateBuff(), description, maxCharges) {}

    public abstract ConsumablePotion CreatePotion();
}

public class HealthPotionFactory(string name, uint weight, RarityLevel rarity, string description, int maxCharges, string nameBuff, int strengthBuff, string descriptionBuff)
    : ConsumablePotionFactory(name, weight, rarity, new HealthRestoreFactory(nameBuff, strengthBuff, descriptionBuff), description, maxCharges)
{
    public override ConsumablePotion CreatePotion()
        => new ConsumablePotion(Name, Weight, Rarity, Buff, Description, MaxCharges);
}

public class ToxicPotionFactory(string name, uint weight, RarityLevel rarity, string description, int maxCharges, string nameBuff, int strengthBuff, string descriptionBuff)
    : ConsumablePotionFactory(name, weight, rarity, new ToxicFactory(nameBuff, strengthBuff, descriptionBuff), description, maxCharges)
{
    public override ConsumablePotion CreatePotion()
        => new ConsumablePotion(Name, Weight, Rarity, Buff, Description, MaxCharges);
}
