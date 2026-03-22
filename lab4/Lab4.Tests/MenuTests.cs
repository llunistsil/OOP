using DeliveryApp.Data;
using DeliveryApp.Models;

namespace Lab4.Tests.Data;

public class CatalogTests
{
    [Fact(DisplayName = "Catalog loads sample dishes on initialization")]
    public void Catalog_Initialization_LoadsSampleDishes()
    {
        var catalog = new Catalog();

        var dishes = catalog.GetAllDishes();

        Assert.NotEmpty(dishes);
        Assert.True(dishes.Count() >= 10);
    }

    [Fact(DisplayName = "Catalog FindDish with valid code returns dish")]
    public void Catalog_FindDish_ValidCode_ReturnsDish()
    {
        var catalog = new Catalog();

        var dish = catalog.FindDish("PZ001");

        Assert.NotNull(dish);
        Assert.Equal("PZ001", dish.Code);
        Assert.Equal("Classic Margherita", dish.Title);
    }

    [Fact(DisplayName = "Catalog FindDish with invalid code returns null")]
    public void Catalog_FindDish_InvalidCode_ReturnsNull()
    {
        var catalog = new Catalog();

        var dish = catalog.FindDish("INVALID-CODE");

        Assert.Null(dish);
    }

    [Fact(DisplayName = "Catalog AddDish adds new dish to collection")]
    public void Catalog_AddDish_NewDish_AddsToCollection()
    {
        var catalog = new Catalog();
        var newDish = new DishOption("TEST001", "Test Special", 15.99m, "Specials", 25);

        catalog.AddDish(newDish);

        var retrieved = catalog.FindDish("TEST001");
        Assert.NotNull(retrieved);
        Assert.Equal("Test Special", retrieved.Title);
    }

    [Fact(DisplayName = "Catalog GetModifiableDishes returns only modifiable items")]
    public void Catalog_GetModifiableDishes_ReturnsModifiableOnly()
    {
        var catalog = new Catalog();

        var modifiable = catalog.GetModifiableDishes();

        Assert.All(modifiable, dish => Assert.True(dish.CanModify));
    }

    [Fact(DisplayName = "Catalog GetDishesByCategory returns items from specified category")]
    public void Catalog_GetDishesByCategory_PizzaCategory_ReturnsPizzaOnly()
    {
        var catalog = new Catalog();

        var pizzaDishes = catalog.GetDishesByCategory("Pizza");

        Assert.NotEmpty(pizzaDishes);
        Assert.All(pizzaDishes, dish => Assert.Equal("Pizza", dish.Group));
    }
}
