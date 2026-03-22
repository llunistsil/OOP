using Lab3.Tests;

namespace Lab3.Tests.Core.Items;

public class ConsumablePotionTests
{
    [Fact(DisplayName = "Potion has correct max charges")]
    public void Potion_MaxCharges_IsCorrect()
    {
        var potion = TestTools.CreateHealthPotion();

        Assert.Equal(3, potion.MaxCharges);
    }

    [Fact(DisplayName = "Potion starts with full charges")]
    public void Potion_InitialState_FullCharges()
    {
        var potion = TestTools.CreateHealthPotion();

        Assert.Equal(potion.MaxCharges, potion.Charges);
    }

    [Fact(DisplayName = "Activate reduces charges by one")]
    public void Potion_Activate_ReducesCharges()
    {
        var potion = TestTools.CreateHealthPotion();
        var initialCharges = potion.Charges;

        potion.Activate();

        Assert.Equal(initialCharges - 1, potion.Charges);
    }

    [Fact(DisplayName = "Activate returns buff trigger message")]
    public void Potion_Activate_ReturnsBuffMessage()
    {
        var potion = TestTools.CreateHealthPotion();

        var result = potion.Activate();

        Assert.Contains("Triggering buff", result);
        Assert.Contains("Healing", result);
    }

    [Fact(DisplayName = "Refill increases charges")]
    public void Potion_Refill_IncreasesCharges()
    {
        var potion = TestTools.CreateHealthPotion();
        potion.Activate();
        var afterUseCharges = potion.Charges;

        potion.Refill(1);

        Assert.Equal(afterUseCharges + 1, potion.Charges);
    }

    [Fact(DisplayName = "RefillToFull restores max charges")]
    public void Potion_RefillToFull_RestoresMaxCharges()
    {
        var potion = TestTools.CreateHealthPotion();
        potion.Activate();
        potion.Activate();

        potion.RefillToFull();

        Assert.Equal(potion.MaxCharges, potion.Charges);
    }

    [Fact(DisplayName = "Refill does not exceed max charges")]
    public void Potion_Refill_DoesNotExceedMax()
    {
        var potion = TestTools.CreateHealthPotion();

        potion.Refill(100);

        Assert.Equal(potion.MaxCharges, potion.Charges);
    }

    [Fact(DisplayName = "Activate with zero charges throws exception")]
    public void Potion_Activate_ZeroCharges_ThrowsException()
    {
        var potion = TestTools.CreateHealthPotion();
        potion.Activate();
        potion.Activate();
        potion.Activate();

        var ex = Assert.Throws<Exception>(() => potion.Activate());
        Assert.Contains("no remaining charges", ex.Message);
    }

    [Fact(DisplayName = "CanCombineWith returns true for same buff type")]
    public void Potion_CanCombineWith_SameBuffType_ReturnsTrue()
    {
        var potion1 = TestTools.CreateHealthPotion();
        var potion2 = TestTools.CreateHealthPotion();

        var canCombine = potion1.CanCombineWith(potion2);

        Assert.True(canCombine);
    }

    [Fact(DisplayName = "CombineWith merges buffs and removes second potion")]
    public void Potion_CombineWith_MergesBuffs()
    {
        var potion1 = TestTools.CreateHealthPotion();
        var potion2 = TestTools.CreateHealthPotion();
        var initialStrength = potion1.Buff.Strength;

        potion1.CombineWith(potion2);

        Assert.True(potion1.Buff.Strength > initialStrength);
    }

    [Fact(DisplayName = "GetDetails includes charges information")]
    public void Potion_GetDetails_IncludesCharges()
    {
        var potion = TestTools.CreateHealthPotion();

        var details = potion.GetDetails();

        Assert.Contains($"Charges: {potion.Charges}/{potion.MaxCharges}", details);
    }
}
