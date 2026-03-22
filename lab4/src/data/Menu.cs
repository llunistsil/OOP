using DeliveryApp.Models;

namespace DeliveryApp.Data;

public class Catalog
{
    private Dictionary<string, DishOption> _dishOptions;

    public Catalog()
    {
        _dishOptions = new Dictionary<string, DishOption>();
        LoadSampleDishes();
    }

    private void LoadSampleDishes()
    {
        AddDish(new DishOption("PZ001", "Classic Margherita", 11.99m, "Pizza", 18, true));
        AddDish(new DishOption("PZ002", "Spicy Pepperoni", 13.99m, "Pizza", 20, true));
        AddDish(new DishOption("SL001", "Caesar with Chicken", 7.99m, "Salads", 8, true));
        AddDish(new DishOption("SL002", "Mediterranean Mix", 8.49m, "Salads", 10, true));
        AddDish(new DishOption("BG001", "Beef Deluxe Burger", 10.49m, "Burgers", 14, true));
        AddDish(new DishOption("BG002", "Mushroom Swiss Burger", 9.99m, "Burgers", 12, true));
        AddDish(new DishOption("DS001", "Tiramisu", 5.99m, "Desserts", 3, false));
        AddDish(new DishOption("DS002", "Brownie with Ice Cream", 6.49m, "Desserts", 4, false));
        AddDish(new DishOption("DR001", "Coca-Cola", 2.49m, "Beverages", 1, true));
        AddDish(new DishOption("DR002", "Fresh Lemonade", 3.49m, "Beverages", 2, true));
    }

    public void AddDish(DishOption dish) => _dishOptions[dish.Code] = dish;
    public IEnumerable<DishOption> GetAllDishes() => _dishOptions.Values;
    public DishOption? FindDish(string code) => _dishOptions.ContainsKey(code) ? _dishOptions[code] : null;
    
    public IEnumerable<DishOption> GetModifiableDishes()
        => _dishOptions.Values.Where(dish => dish.CanModify);
    
    public IEnumerable<DishOption> GetDishesByCategory(string category)
        => _dishOptions.Values.Where(dish => dish.Group.Equals(category, StringComparison.OrdinalIgnoreCase));
}
