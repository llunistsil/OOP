using System.Text;
using InventorySystem.Core.Enums;
using InventorySystem.Core.Interfaces;

namespace InventorySystem.Core.Items.Potions;

public class BuffEffect : IBuff
{
    public string Id { get; private set; }
    public string Name { get; private set; }
    public BuffType BuffType { get; private set; }
    public int Strength { get; private set; }
    public string Description { get; private set; }

    public BuffEffect(string name, BuffType buffType, int strength, string description)
    {
        Id = Guid.NewGuid().ToString();
        Name = name;
        BuffType = buffType;
        Strength = strength;
        Description = description;
    }

    public string Trigger()
        => new StringBuilder()
            .AppendLine($"Triggering buff: {Name}")
            .AppendLine(Description)
            .AppendLine($"Buff executed successfully")
            .ToString();

    public string GetBuffDetails() => $"{Name} - {Description} (Strength: {Strength})";

    public bool IsCompatibleWith(IBuff other) => BuffType == other.BuffType;

    public void MergeWith(IBuff other)
    {
        if (!IsCompatibleWith(other))
            throw new Exception($"Cannot merge {Name} with {other.Name}");

        Strength += other.Strength;
        Name = $"{Name} + {other.Strength}";
        Description = GetDefaultDescription(BuffType, Strength);
    }

    private string GetDefaultDescription(BuffType buffType, int strength)
        => buffType switch
        {
            BuffType.HealthRestore => $"Restores {strength} health points",
            BuffType.ManaRestore => $"Restores {strength} mana points",
            BuffType.PowerIncrease => $"Increases power by {strength}",
            BuffType.DefenseIncrease => $"Increases defense by {strength}",
            BuffType.AgilityIncrease => $"Increases agility by {strength}",
            BuffType.FireProtection => $"Provides {strength}% fire protection",
            BuffType.FrostProtection => $"Provides {strength}% frost protection",
            BuffType.Stealth => $"Grants stealth for {strength} seconds",
            BuffType.Regen => $"Regenerates {strength} health per tick",
            _ => "Unknown buff effect"
        };
}
