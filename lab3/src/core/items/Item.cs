using InventorySystem.Core.Enums;
using InventorySystem.Core.Interfaces;

namespace InventorySystem.Core.Items;

public abstract class GearBase : IGear
{
    public string Id { get; protected set; }
    public string Name { get; protected set; }
    public uint Weight { get; protected set; }
    public GearType GearType { get; protected set; }
    public RarityLevel Rarity { get; protected set; }
    public string Description { get; protected set; }

    protected GearBase(string name, uint weight, GearType gearType, RarityLevel rarity, string description)
    {
        Id = Guid.NewGuid().ToString();
        Name = name;
        Weight = weight;
        GearType = gearType;
        Rarity = rarity;
        Description = description;
    }

    public abstract string Activate();

    public virtual string GetDetails() => $"{Name} [{Rarity}] - {Description} | Mass: {Weight}kg";
}
