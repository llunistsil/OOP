using System.Text;
using InventorySystem.Core.Enums;
using InventorySystem.Core.Interfaces;
using InventorySystem.Patterns.Factory;

namespace InventorySystem.Core.Items.Potions;

public class ConsumablePotion : GearBase
{
    public IBuff Buff { get; private set; }
    public int MaxCharges { get; init; }
    public int Charges { get; private set; }

    public ConsumablePotion(string name, uint weight, RarityLevel rarity, IBuff buff, string description, int maxCharges = 1)
        : base(name, weight, GearType.Consumable, rarity, description)
    {
        Buff = buff;
        MaxCharges = maxCharges;
        Charges = maxCharges;
    }

    public ConsumablePotion(string name, uint weight, RarityLevel rarity, string description, int maxCharges, BuffEffectFactory factory)
        : this(name, weight, rarity, factory.CreateBuff(), description, maxCharges) {}

    public override string GetDetails() => $"{base.GetDetails()} | {Buff.GetBuffDetails()} | Charges: {Charges}/{MaxCharges}";

    public bool CanCombineWith(ConsumablePotion otherPotion) => Buff.IsCompatibleWith(otherPotion.Buff);

    public void Refill(int amount) => Charges = Math.Min(Charges + amount, MaxCharges);

    public void RefillToFull() => Charges = MaxCharges;

    public override string Activate()
    {
        if (Charges <= 0)
            throw new Exception($"{Name} has no remaining charges");

        var result = new StringBuilder();
        result.AppendLine(Buff.Trigger());
        Charges--;

        if (Charges <= 0)
            result.AppendLine($"{Name} is now empty");
        else
            result.AppendLine($"{Name} has {Charges} charges remaining");

        return result.ToString();
    }

    public void CombineWith(ConsumablePotion otherPotion)
    {
        if (!CanCombineWith(otherPotion))
            throw new Exception($"Cannot combine {Name} with {otherPotion.Name}");

        Buff.MergeWith(otherPotion.Buff);
        Charges += otherPotion.Charges;
        Rarity = ImproveRarity();
        Description = $"{Description}\nCombined with {Buff.Name} effect";
    }

    private RarityLevel ImproveRarity() => (RarityLevel)Math.Min((int)Rarity + 1, (int)RarityLevel.Mythic);
}
