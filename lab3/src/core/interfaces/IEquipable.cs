using InventorySystem.Core.Enums;

namespace InventorySystem.Core.Interfaces;

public interface IEquippable : INamable
{
    EquipmentSlot EquipSlot { get; }
    bool IsEquippedState { get; }
    void Wear();
    void Remove();
}
