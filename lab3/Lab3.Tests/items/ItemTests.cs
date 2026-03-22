using Lab3.Tests;

namespace Lab3.Tests.Core.Items;

public class GearBaseTests
{
    [Fact(DisplayName = "Gear Id is auto-generated and not empty")]
    public void Gear_Id_IsNotEmpty()
    {
        var potion = TestTools.CreateHealthPotion();

        Assert.NotNull(potion.Id);
        Assert.NotEmpty(potion.Id);
    }

    [Fact(DisplayName = "Gear Name matches specified value")]
    public void Gear_Name_IsCorrect()
    {
        var potion = TestTools.CreateHealthPotion();

        Assert.Equal(TestTools.HealthPotionName, potion.Name);
    }

    [Fact(DisplayName = "Gear Weight matches specified value")]
    public void Gear_Weight_IsCorrect()
    {
        var potion = TestTools.CreateHealthPotion();

        Assert.Equal(1u, potion.Weight);
    }

    [Fact(DisplayName = "Gear Rarity matches specified value")]
    public void Gear_Rarity_IsCorrect()
    {
        var potion = TestTools.CreateHealthPotion();

        Assert.Equal(RarityLevel.Ordinary, potion.Rarity);
    }

    [Fact(DisplayName = "Gear GetDetails returns correct string")]
    public void Gear_GetDetails_ReturnsCorrectString()
    {
        var potion = TestTools.CreateHealthPotion();

        var details = potion.GetDetails();

        Assert.Contains(TestTools.HealthPotionName, details);
        Assert.Contains(RarityLevel.Ordinary.ToString(), details);
        Assert.Contains("Restores 25 health points", details);
        Assert.Contains("Mass: 1kg", details);
    }

    [Fact(DisplayName = "Gear Activate returns non-empty string for Potion")]
    public void Gear_Activate_ReturnsNonEmptyString_Potion()
    {
        var potion = TestTools.CreateHealthPotion();

        var result = potion.Activate();

        Assert.NotNull(result);
        Assert.NotEmpty(result);
    }

    [Fact(DisplayName = "Gear Activate returns non-empty string for Weapon")]
    public void Gear_Activate_ReturnsNonEmptyString_Weapon()
    {
        var blade = TestTools.CreateBlade();

        var result = blade.Activate();

        Assert.NotNull(result);
        Assert.NotEmpty(result);
    }

    [Fact(DisplayName = "Gear Activate returns non-empty string for Armor")]
    public void Gear_Activate_ReturnsNonEmptyString_Armor()
    {
        var helm = TestTools.CreateHelm();

        var result = helm.Activate();

        Assert.NotNull(result);
        Assert.NotEmpty(result);
    }
}
