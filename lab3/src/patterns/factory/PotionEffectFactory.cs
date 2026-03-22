using InventorySystem.Core.Enums;
using InventorySystem.Core.Items.Potions;

namespace InventorySystem.Patterns.Factory;

public abstract class BuffEffectFactory
{
    protected string Name { get; init; }
    protected int Strength { get; init; }
    protected string Description { get; init; }

    public BuffEffectFactory(string name, int strength, string description)
    {
        Name = name;
        Strength = strength;
        Description = description;
    }

    public abstract BuffEffect CreateBuff();
}

public class HealthRestoreFactory(string name, int strength, string description)
    : BuffEffectFactory(name, strength, description)
{
    public override BuffEffect CreateBuff()
        => new BuffEffect(Name, BuffType.HealthRestore, Strength, Description);
}

public class ToxicFactory(string name, int strength, string description)
    : BuffEffectFactory(name, strength, description)
{
    public override BuffEffect CreateBuff()
        => new BuffEffect(Name, BuffType.Toxic, Strength, Description);
}

public class DefenseIncreaseFactory(string name, int strength, string description)
    : BuffEffectFactory(name, strength, description)
{
    public override BuffEffect CreateBuff()
        => new BuffEffect(Name, BuffType.DefenseIncrease, Strength, Description);
}

public class RegenFactory(string name, int strength, string description)
    : BuffEffectFactory(name, strength, description)
{
    public override BuffEffect CreateBuff()
        => new BuffEffect(Name, BuffType.Regen, Strength, Description);
}
