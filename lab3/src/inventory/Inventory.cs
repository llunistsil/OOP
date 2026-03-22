using System.Text;
using InventorySystem.Core.Enums;
using InventorySystem.Core.Interfaces;
using InventorySystem.Core.Items.Potions;

namespace InventorySystem.Storage;

public class Backpack(uint capacity)
{
    private List<IGear> _items = new List<IGear>();
    private Dictionary<EquipmentSlot, IEquippable> _gearSlots = new Dictionary<EquipmentSlot, IEquippable>();

    public uint Capacity { get; init; } = capacity;
    public uint CurrentWeight { get; private set; } = 0;

    public void UnequipGear(IEquippable item) => UnequipFromSlot(item.EquipSlot);

    public string GetStatus() => $"Items: {_items.Count} | Equipped: {_gearSlots.Count} | Weight: {CurrentWeight}/{Capacity}";

    public bool CanFitItem(IGear item) => CurrentWeight + item.Weight <= Capacity;

    public void AddItem(IGear item)
    {
        if (!CanFitItem(item))
            throw new Exception($"Cannot add {item.Name}: inventory is full! ({CurrentWeight}/{Capacity})");

        _items.Add(item);
        CurrentWeight += item.Weight;
    }

    public void RemoveItem(IGear item)
    {
        if (!_items.Remove(item))
            throw new Exception($"Item {item.Name} not found in inventory");

        CurrentWeight -= item.Weight;

        if (item is IEquippable equippable && equippable.IsEquippedState)
            _gearSlots.Remove(equippable.EquipSlot);
    }

    public void EquipGear(IEquippable item)
    {
        if (!_items.Contains((IGear)item))
            throw new Exception($"Cannot equip {item.Name}: item not in inventory");

        if (item.IsEquippedState)
            throw new Exception($"{item.Name} is already equipped");

        if (_gearSlots.ContainsKey(item.EquipSlot))
        {
            var currentItem = _gearSlots[item.EquipSlot];
            currentItem.Remove();
            _gearSlots.Remove(item.EquipSlot);
        }

        item.Wear();
        _gearSlots[item.EquipSlot] = item;
    }

    public void UnequipFromSlot(EquipmentSlot slot)
    {
        if (!_gearSlots.ContainsKey(slot))
            throw new Exception($"Nothing equipped in slot {slot}");

        var item = _gearSlots[slot];
        item.Remove();
        _gearSlots.Remove(slot);
    }

    public void UseItem(IGear item)
    {
        if (!_items.Contains(item))
            throw new Exception($"Cannot use {item.Name}: item not in inventory");

        item.Activate();
    }

    public void EnhanceItem(IEnhanceable item)
    {
        if (!_items.Contains((IGear)item))
            throw new Exception($"Cannot enhance {item.Name}: item not in inventory");

        item.Enhance();
    }

    public void CombinePotions(ConsumablePotion first, ConsumablePotion second)
    {
        if (!_items.Contains(first))
            throw new Exception($"Potion {first.Name} not found in inventory");
        if (!_items.Contains(second))
            throw new Exception($"Potion {second.Name} not found in inventory");

        if (first == second)
            throw new Exception("Cannot combine potion with itself");

        if (!first.CanCombineWith(second))
            throw new Exception($"Cannot combine {first.Name} with {second.Name}");

        first.CombineWith(second);
        RemoveItem(second);
    }

    public void DisplayInventory()
        => new StringBuilder()
            .AppendLine(GetStatus())
            .AppendLine("Bag contents:")
            .AppendLine(DisplayBag())
            .AppendLine("Equipped gear:")
            .AppendLine(DisplayEquipped());

    public string DisplayBag()
    {
        if (!_items.Any())
            return "Bag is empty";

        var result = new StringBuilder();
        foreach (var item in _items)
            result.AppendLine(item.GetDetails());
        return result.ToString();
    }

    public string DisplayEquipped()
    {
        if (!_gearSlots.Any())
            return "No gear equipped";

        var result = new StringBuilder();
        foreach (var slot in _gearSlots)
            result.AppendLine($"{slot.Key}: {slot.Value.Name}");
        return result.ToString();
    }

    public void Clear()
    {
        _items.Clear();
        _gearSlots.Clear();
        CurrentWeight = 0;
    }
}
