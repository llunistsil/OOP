namespace InventorySystem.Core.Interfaces;

public interface IQuestItem
{
    string QuestId { get; }
    bool IsInteractable { get; }
}
