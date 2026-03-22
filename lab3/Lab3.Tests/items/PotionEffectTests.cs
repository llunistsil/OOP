using Lab3.Tests;

namespace Lab3.Tests.Core.Items;

public class BuffEffectTests
{
    [Fact(DisplayName = "Buff Id is auto-generated and not empty")]
    public void Buff_Id_IsNotEmpty()
    {
        var potion = TestTools.CreateHealthPotion();

        Assert.NotNull(potion.Buff.Id);
        Assert.NotEmpty(potion.Buff.Id);
    }

    [Fact(DisplayName = "Buff Name matches specified value")]
    public void Buff_Name_IsCorrect()
    {
        var potion = TestTools.CreateHealthPotion();

        Assert.Equal("Healing", potion.Buff.Name);
    }

    [Fact(DisplayName = "Buff Type matches specified value")]
    public void Buff_BuffType_IsCorrect()
    {
        var potion = TestTools.CreateHealthPotion();

        Assert.Equal(BuffType.HealthRestore, potion.Buff.BuffType);
    }

    [Fact(DisplayName = "Buff Strength matches specified value")]
    public void Buff_Strength_IsCorrect()
    {
        var potion = TestTools.CreateHealthPotion();

        Assert.Equal(25, potion.Buff.Strength);
    }

    [Fact(DisplayName = "Trigger returns buff execution message")]
    public void Buff_Trigger_ReturnsExecutionMessage()
    {
        var potion = TestTools.CreateHealthPotion();

        var result = potion.Buff.Trigger();

        Assert.Contains("Triggering buff", result);
        Assert.Contains("Healing", result);
        Assert.Contains("executed successfully", result);
    }

    [Fact(DisplayName = "GetBuffDetails returns formatted string")]
    public void Buff_GetBuffDetails_ReturnsFormattedString()
    {
        var potion = TestTools.CreateHealthPotion();

        var details = potion.Buff.GetBuffDetails();

        Assert.Contains("Healing", details);
        Assert.Contains("Restores 25 health points", details);
        Assert.Contains("Strength: 25", details);
    }

    [Fact(DisplayName = "IsCompatibleWith returns true for same buff type")]
    public void Buff_IsCompatibleWith_SameType_ReturnsTrue()
    {
        var potion1 = TestTools.CreateHealthPotion();
        var potion2 = TestTools.CreateHealthPotion();

        var compatible = potion1.Buff.IsCompatibleWith(potion2.Buff);

        Assert.True(compatible);
    }

    [Fact(DisplayName = "IsCompatibleWith returns false for different buff types")]
    public void Buff_IsCompatibleWith_DifferentType_ReturnsFalse()
    {
        var healthPotion = TestTools.CreateHealthPotion();
        var toxicPotion = TestTools.CreateToxicPotion();

        var compatible = healthPotion.Buff.IsCompatibleWith(toxicPotion.Buff);

        Assert.False(compatible);
    }

    [Fact(DisplayName = "MergeWith increases strength")]
    public void Buff_MergeWith_IncreasesStrength()
    {
        var potion1 = TestTools.CreateHealthPotion();
        var potion2 = TestTools.CreateHealthPotion();
        var initialStrength = potion1.Buff.Strength;

        potion1.Buff.MergeWith(potion2.Buff);

        Assert.True(potion1.Buff.Strength > initialStrength);
    }

    [Fact(DisplayName = "MergeWith incompatible buffs throws exception")]
    public void Buff_MergeWith_Incompatible_ThrowsException()
    {
        var healthPotion = TestTools.CreateHealthPotion();
        var toxicPotion = TestTools.CreateToxicPotion();

        var ex = Assert.Throws<Exception>(() => healthPotion.Buff.MergeWith(toxicPotion.Buff));
        Assert.Contains("Cannot merge", ex.Message);
    }
}
