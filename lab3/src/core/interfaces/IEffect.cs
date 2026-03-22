using InventorySystem.Core.Enums;

namespace InventorySystem.Core.Interfaces;

public interface IBuff
{
    string Id { get; }
    string Name { get; }
    BuffType BuffType { get; }
    int Strength { get; }
    string Description { get; }

    string Trigger();
    string GetBuffDetails();
    bool IsCompatibleWith(IBuff other);
    void MergeWith(IBuff other);
}
