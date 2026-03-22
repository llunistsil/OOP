using Lab3.Tests;

namespace Lab3.Tests.Core.Items;

public class WeaponGearTests
{
    [Fact(DisplayName = "Weapon has correct attack power")]
    public void Weapon_AttackPower_IsCorrect()
    {
        var blade = TestTools.CreateBlade();

        Assert.Equal(15, blade.AttackPower);
    }

    [Fact(DisplayName = "Weapon has correct equipment slot")]
    public void Weapon_EquipSlot_IsCorrect()
    {
        var blade = TestTools.CreateBlade();

        Assert.Equal(EquipmentSlot.PrimaryHand, blade.EquipSlot);
    }

    [Fact(DisplayName = "Weapon starts unequipped")]
    public void Weapon_InitialState_IsUnequipped()
    {
        var blade = TestTools.CreateBlade();

        Assert.False(blade.IsEquippedState);
    }

    [Fact(DisplayName = "Wear method equips weapon")]
    public void Weapon_Wear_EquipsWeapon()
    {
        var blade = TestTools.CreateBlade();

        blade.Wear();

        Assert.True(blade.IsEquippedState);
    }

    [Fact(DisplayName = "Remove method unequips weapon")]
    public void Weapon_Remove_UnequipsWeapon()
    {
        var blade = TestTools.CreateBlade();
        blade.Wear();

        blade.Remove();

        Assert.False(blade.IsEquippedState);
    }

    [Fact(DisplayName = "Weapon GetDetails includes attack power")]
    public void Weapon_GetDetails_IncludesAttackPower()
    {
        var blade = TestTools.CreateBlade();

        var details = blade.GetDetails();

        Assert.Contains("Attack: 15", details);
    }

    [Fact(DisplayName = "Weapon Activate when unequipped returns error message")]
    public void Weapon_Activate_Unequipped_ReturnsErrorMessage()
    {
        var blade = TestTools.CreateBlade();

        var result = blade.Activate();

        Assert.Contains("Must be equipped first", result);
    }

    [Fact(DisplayName = "Weapon Activate when equipped returns damage message")]
    public void Weapon_Activate_Equipped_ReturnsDamageMessage()
    {
        var blade = TestTools.CreateBlade();
        blade.Wear();

        var result = blade.Activate();

        Assert.Contains("damage", result);
    }

    [Fact(DisplayName = "Enhance increases tier and attack power")]
    public void Weapon_Enhance_IncreasesTierAndAttack()
    {
        var blade = TestTools.CreateBlade();
        var initialAttack = blade.AttackPower;

        blade.Enhance();

        Assert.Equal(2, blade.Tier);
        Assert.True(blade.AttackPower > initialAttack);
    }

    [Fact(DisplayName = "CanImproveRarity returns false at low tier")]
    public void Weapon_CanImproveRarity_AtLowTier_ReturnsFalse()
    {
        var blade = TestTools.CreateBlade();

        var canImprove = blade.CanImproveRarity();

        Assert.False(canImprove);
    }
}
