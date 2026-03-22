using InventorySystem.Core.Enums;

namespace InventorySystem.Core.Items.Quests;

public class QuestItem : GearBase, Interfaces.IQuestItem
{
    public string QuestId { get; private set; }
    public bool IsInteractable { get; private set; }

    public QuestItem(string name, uint weight, RarityLevel rarity, string description, string questId, bool isInteractable = false)
        : base(name, weight, GearType.QuestItem, rarity, description)
    {
        QuestId = questId;
        IsInteractable = isInteractable;
    }

    public override string Activate()
    {
        if (!IsInteractable)
            return $"{Name} cannot be used: quest item./nRequired for quest: {QuestId}";
        return $"Using quest item: {Name}/n{Description}";
    }

    public override string GetDetails()
    {
        var interactStatus = IsInteractable ? "Usable" : "Not usable";
        return $"{base.GetDetails()} | Quest ID: {QuestId} | {interactStatus}";
    }
}
