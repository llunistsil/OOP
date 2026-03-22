using Lab3.Tests;

namespace Lab3.Tests.Storage;

public class BackpackTests
{
    [Fact(DisplayName = "Add health potion increases count and weight")]
    public void AddHealthPotion_IncreasesCountAndWeight()
    {
        var backpack = new Backpack(TestTools.DefaultBackpackCapacity);
        var potion = TestTools.CreateHealthPotion();

        backpack.AddItem(potion);

        Assert.Contains("Items: 1", backpack.GetStatus());
        Assert.Contains($"Weight: {potion.Weight}/{TestTools.DefaultBackpackCapacity}", backpack.GetStatus());
        Assert.Contains(TestTools.HealthPotionName, backpack.DisplayBag());
    }

    [Fact(DisplayName = "Add blade increases count and weight")]
    public void AddBlade_IncreasesCountAndWeight()
    {
        var backpack = new Backpack(TestTools.DefaultBackpackCapacity);
        var blade = TestTools.CreateBlade();

        backpack.AddItem(blade);

        Assert.Contains("Items: 1", backpack.GetStatus());
        Assert.Contains($"Weight: {blade.Weight}/{TestTools.DefaultBackpackCapacity}", backpack.GetStatus());
        Assert.Contains(TestTools.BladeName, backpack.DisplayBag());
    }

    [Fact(DisplayName = "Add helm increases count and weight")]
    public void AddHelm_IncreasesCountAndWeight()
    {
        var backpack = new Backpack(TestTools.DefaultBackpackCapacity);
        var helm = TestTools.CreateHelm();

        backpack.AddItem(helm);

        Assert.Contains("Items: 1", backpack.GetStatus());
        Assert.Contains($"Weight: {helm.Weight}/{TestTools.DefaultBackpackCapacity}", backpack.GetStatus());
        Assert.Contains(TestTools.HelmName, backpack.DisplayBag());
    }

    [Fact(DisplayName = "Add item to full backpack throws exception")]
    public void AddItem_ToFullBackpack_ThrowsException()
    {
        var backpack = new Backpack(TestTools.SmallBackpackCapacity);
        var heavyPlate = TestTools.CreateHeavyPlate();

        var ex = Assert.Throws<Exception>(() => backpack.AddItem(heavyPlate));
        Assert.Contains("inventory is full", ex.Message);
    }

    [Fact(DisplayName = "Remove potion decreases count and weight")]
    public void RemovePotion_DecreasesCountAndWeight()
    {
        var backpack = new Backpack(TestTools.DefaultBackpackCapacity);
        var potion = TestTools.CreateHealthPotion();
        backpack.AddItem(potion);

        backpack.RemoveItem(potion);

        Assert.Contains("Items: 0", backpack.GetStatus());
        Assert.Contains($"Weight: 0/{TestTools.DefaultBackpackCapacity}", backpack.GetStatus());
        Assert.Contains("Bag is empty", backpack.DisplayBag());
    }

    [Fact(DisplayName = "Equip helm adds to equipment")]
    public void EquipHelm_AddsToEquipment()
    {
        var backpack = new Backpack(TestTools.DefaultBackpackCapacity);
        var helm = TestTools.CreateHelm();
        backpack.AddItem(helm);

        backpack.EquipGear(helm);

        Assert.Contains("Equipped: 1", backpack.GetStatus());
        Assert.Contains($"{EquipmentSlot.Head}: {TestTools.HelmName}", backpack.DisplayEquipped());
    }

    [Fact(DisplayName = "Equip blade adds to equipment")]
    public void EquipBlade_AddsToEquipment()
    {
        var backpack = new Backpack(TestTools.DefaultBackpackCapacity);
        var blade = TestTools.CreateBlade();
        backpack.AddItem(blade);

        backpack.EquipGear(blade);

        Assert.Contains("Equipped: 1", backpack.GetStatus());
        Assert.Contains($"{EquipmentSlot.PrimaryHand}: {TestTools.BladeName}", backpack.DisplayEquipped());
    }

    [Fact(DisplayName = "Unequip removes from equipment")]
    public void Unequip_RemovesFromEquipment()
    {
        var backpack = new Backpack(TestTools.DefaultBackpackCapacity);
        var helm = TestTools.CreateHelm();
        backpack.AddItem(helm);
        backpack.EquipGear(helm);

        backpack.UnequipFromSlot(EquipmentSlot.Head);

        Assert.Contains("Equipped: 0", backpack.GetStatus());
        Assert.Contains("No gear equipped", backpack.DisplayEquipped());
    }

    [Fact(DisplayName = "Use potion calls Activate method")]
    public void UsePotion_CallsActivateMethod()
    {
        var backpack = new Backpack(TestTools.DefaultBackpackCapacity);
        var potion = TestTools.CreateHealthPotion();
        backpack.AddItem(potion);

        backpack.UseItem(potion);

        var bagContents = backpack.DisplayBag();
        Assert.Contains(TestTools.HealthPotionName, bagContents);
    }

    [Fact(DisplayName = "Combine potions removes second potion")]
    public void CombinePotions_RemovesSecondPotion()
    {
        var backpack = new Backpack(TestTools.DefaultBackpackCapacity);
        var potion1 = TestTools.CreateHealthPotion();
        var potion2 = TestTools.CreateHealthPotion();
        backpack.AddItem(potion1);
        backpack.AddItem(potion2);

        backpack.CombinePotions(potion1, potion2);

        var lines = backpack.DisplayBag().Split('\n').Count(line => line.Contains(TestTools.HealthPotionName));
        Assert.Equal(1, lines);
    }

    [Fact(DisplayName = "Combine same potion throws exception")]
    public void CombinePotions_SamePotion_ThrowsException()
    {
        var backpack = new Backpack(TestTools.DefaultBackpackCapacity);
        var potion = TestTools.CreateHealthPotion();
        backpack.AddItem(potion);

        var ex = Assert.Throws<Exception>(() => backpack.CombinePotions(potion, potion));
        Assert.Contains("Cannot combine potion with itself", ex.Message);
    }

    [Fact(DisplayName = "Get status returns correct string")]
    public void GetStatus_ReturnsCorrectString()
    {
        var backpack = new Backpack(TestTools.DefaultBackpackCapacity);
        var potion = TestTools.CreateHealthPotion();
        backpack.AddItem(potion);

        var status = backpack.GetStatus();

        Assert.Contains("Items: 1", status);
        Assert.Contains($"Weight: {potion.Weight}/{TestTools.DefaultBackpackCapacity}", status);
    }
}
