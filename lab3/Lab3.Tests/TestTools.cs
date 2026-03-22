using InventorySystem.Core.Enums;
using InventorySystem.Core.Items.Armors;
using InventorySystem.Core.Items.Potions;
using InventorySystem.Core.Items.Weapons;
using InventorySystem.Patterns.Factory;

namespace Lab3.Tests;

public static class TestTools
{
    public const uint DefaultBackpackCapacity = 100;
    public const uint SmallBackpackCapacity = 5;
    public const uint HeavyItemWeight = 1000;

    public const string HealthPotionName = "Health Elixir";
    public const string ToxicPotionName = "Venom Vial";
    public const string BladeName = "Dragon Blade";
    public const string BucklerName = "Iron Buckler";
    public const string HelmName = "Steel Helm";
    public const string PlateName = "Knight Plate";

    public static ConsumablePotion CreateHealthPotion()
    {
        return new HealthPotionFactory(
            HealthPotionName,
            1,
            RarityLevel.Ordinary,
            "Restores 25 HP",
            3,
            "Healing",
            25,
            "Restores 25 health points"
        ).CreatePotion();
    }

    public static ConsumablePotion CreateToxicPotion()
    {
        return new ToxicPotionFactory(
            ToxicPotionName,
            1,
            RarityLevel.Uncommon,
            "Deals 8 damage per second",
            1,
            "Toxic",
            8,
            "Deals 8 damage per second"
        ).CreatePotion();
    }

    public static WeaponGear CreateBlade()
    {
        return new PrimaryWeaponFactory(
            BladeName,
            5,
            RarityLevel.Ordinary,
            15,
            "Sharp dragon blade"
        ).CreateWeapon();
    }

    public static WeaponGear CreateBuckler()
    {
        return new SecondaryWeaponFactory(
            BucklerName,
            8,
            RarityLevel.Premium,
            0,
            "Sturdy iron buckler"
        ).CreateWeapon();
    }

    public static ArmorGear CreateHelm()
    {
        return new TestHeadGearFactory(
            HelmName,
            5,
            RarityLevel.Ordinary,
            12,
            "Simple steel helm"
        ).CreateArmor();
    }

    public static ArmorGear CreatePlate()
    {
        return new TestChestGearFactory(
            PlateName,
            20,
            RarityLevel.Premium,
            35,
            "Strong knight plate"
        ).CreateArmor();
    }

    public static ArmorGear CreateHeavyPlate()
    {
        return new TestChestGearFactory(
            "Heavy Plate",
            HeavyItemWeight,
            RarityLevel.Mythic,
            100,
            "Very heavy plate armor"
        ).CreateArmor();
    }

    public class TestHeadGearFactory : HeadGearFactory
    {
        public TestHeadGearFactory(string name, uint weight, RarityLevel rarity, int defenseRating, string description)
            : base(name, weight, rarity, defenseRating, description) { }
    }

    public class TestChestGearFactory : ChestGearFactory
    {
        public TestChestGearFactory(string name, uint weight, RarityLevel rarity, int defenseRating, string description)
            : base(name, weight, rarity, defenseRating, description) { }
    }
}
