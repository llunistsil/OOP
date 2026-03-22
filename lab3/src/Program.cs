using InventorySystem.Core.Enums;
using InventorySystem.Core.Items.Armors;
using InventorySystem.Core.Items.Potions;
using InventorySystem.Core.Items.Weapons;
using InventorySystem.Core.Items.Quests;
using InventorySystem.Patterns.Factory;
using InventorySystem.Storage;
using InventorySystem.Patterns.Strategy.Enums;

namespace InventorySystem;

public class EntryPoint
{
    public static void Main(string[] args)
    {
        var backpack = new Backpack(50);

        var sword = new WeaponGear("Dragon Slayer", 15, RarityLevel.Epic, 85, EquipmentSlot.PrimaryHand, "A legendary blade forged in dragon fire", EnhancementMode.Standard);
        var helmet = new ArmorGear("Iron Helm", 8, RarityLevel.Uncommon, 25, EquipmentSlot.Head, "Sturdy head protection", EnhancementMode.Standard);
        var healthPotion = new ConsumablePotion("Health Elixir", 2, RarityLevel.Ordinary, new BuffEffect("Minor Healing", BuffType.HealthRestore, 50, "Restores 50 HP"), "A red potion that heals wounds", 3);

        backpack.AddItem(sword);
        backpack.AddItem(helmet);
        backpack.AddItem(healthPotion);

        Console.WriteLine("=== Inventory System Demo ===");
        Console.WriteLine(backpack.GetStatus());
        Console.WriteLine();

        backpack.EquipGear(sword);
        backpack.EquipGear(helmet);

        Console.WriteLine("Equipped:");
        Console.WriteLine(backpack.DisplayEquipped());
        Console.WriteLine();

        Console.WriteLine("Using potion:");
        backpack.UseItem(healthPotion);
        Console.WriteLine();

        var questItem = new QuestItem("Ancient Key", 1, RarityLevel.Premium, "Opens the sealed dungeon door", "QUEST-001", true);
        backpack.AddItem(questItem);
        Console.WriteLine("Quest item added:");
        Console.WriteLine(questItem.GetDetails());

        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey();
    }
}
