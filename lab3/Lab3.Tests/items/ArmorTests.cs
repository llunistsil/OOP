using Lab3.Tests;

namespace Lab3.Tests.Core.Items;

public class ArmorGearTests
{
    [Fact(DisplayName = "Armor has correct defense rating")]
    public void Armor_DefenseRating_IsCorrect()
    {
        var helm = TestTools.CreateHelm();

        Assert.Equal(12, helm.DefenseRating);
    }

    [Fact(DisplayName = "Armor has correct equipment slot")]
    public void Armor_EquipSlot_IsCorrect()
    {
        var helm = TestTools.CreateHelm();

        Assert.Equal(EquipmentSlot.Head, helm.EquipSlot);
    }

    [Fact(DisplayName = "Armor starts unequipped")]
    public void Armor_InitialState_IsUnequipped()
    {
        var helm = TestTools.CreateHelm();

        Assert.False(helm.IsEquippedState);
    }

    [Fact(DisplayName = "Wear method equips armor")]
    public void Armor_Wear_EquipsArmor()
    {
        var helm = TestTools.CreateHelm();

        helm.Wear();

        Assert.True(helm.IsEquippedState);
    }

    [Fact(DisplayName = "Remove method unequips armor")]
    public void Armor_Remove_UnequipsArmor()
    {
        var helm = TestTools.CreateHelm();
        helm.Wear();

        helm.Remove();

        Assert.False(helm.IsEquippedState);
    }

    [Fact(DisplayName = "Armor GetDetails includes defense rating")]
    public void Armor_GetDetails_IncludesDefenseRating()
    {
        var helm = TestTools.CreateHelm();

        var details = helm.GetDetails();

        Assert.Contains("Defense: 12", details);
    }

    [Fact(DisplayName = "Armor Activate when unequipped returns error message")]
    public void Armor_Activate_Unequipped_ReturnsErrorMessage()
    {
        var helm = TestTools.CreateHelm();

        var result = helm.Activate();

        Assert.Contains("Must be equipped first", result);
    }

    [Fact(DisplayName = "Armor Activate when equipped returns passive message")]
    public void Armor_Activate_Equipped_ReturnsPassiveMessage()
    {
        var helm = TestTools.CreateHelm();
        helm.Wear();

        var result = helm.Activate();

        Assert.Contains("passive defense", result);
    }

    [Fact(DisplayName = "Enhance increases tier and defense")]
    public void Armor_Enhance_IncreasesTierAndDefense()
    {
        var plate = TestTools.CreatePlate();
        var initialDefense = plate.DefenseRating;

        plate.Enhance();

        Assert.Equal(2, plate.Tier);
        Assert.True(plate.DefenseRating > initialDefense);
    }

    [Fact(DisplayName = "CanImproveRarity returns false at low tier")]
    public void Armor_CanImproveRarity_AtLowTier_ReturnsFalse()
    {
        var helm = TestTools.CreateHelm();

        var canImprove = helm.CanImproveRarity();

        Assert.False(canImprove);
    }
}
