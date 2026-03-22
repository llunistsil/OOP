using DeliveryApp.Data;
using DeliveryApp.Decorators;
using DeliveryApp.Factories;
using DeliveryApp.Services;

namespace DeliveryApp;

public class Startup
{
    public static void Main(string[] args)
    {
        var catalog = new Catalog();
        var orderService = new OrderManagementService(catalog);
        var factory = new OrderFactory();

        Console.WriteLine("=== Food Delivery System Demo ===\n");

        var order1 = orderService.CreateRegularOrder("Alex Johnson", "123 Main Street, Apt 4B", "+1-555-0101");
        Console.WriteLine($"Created order: {order1.OrderCategory} #{order1.OrderId}");

        var pizza = catalog.FindDish("PZ001");
        var salad = catalog.FindDish("SL001");
        var drink = catalog.FindDish("DR001");

        if (pizza != null) orderService.AddStandardDish(order1.OrderId, pizza, 2);
        if (salad != null) orderService.AddStandardDish(order1.OrderId, salad, 1);
        if (drink != null) orderService.AddStandardDish(order1.OrderId, drink, 2);

        orderService.StartProcessing(order1.OrderId);
        Console.WriteLine($"Order status: {order1.CurrentState}");

        var decoratedOrder = new PriorityDeliveryDecorator(order1);
        Console.WriteLine($"\n{decoratedOrder.GetOrderDescription()}");
        Console.WriteLine($"Total: ${decoratedOrder.CalculateFinalCost():F2}");
        Console.WriteLine($"Estimated time: {decoratedOrder.GetTotalCookTime()} minutes\n");

        var order2 = orderService.CreatePersonalizedOrder("Maria Garcia", "456 Oak Avenue, House 12", "+1-555-0202");
        Console.WriteLine($"Created order: {order2.OrderCategory} #{order2.OrderId}");

        var burger = catalog.FindDish("BG001");
        if (burger != null) orderService.AddCustomDish(order2.OrderId, burger, 1, "No onions, extra cheese", 1.5m);

        orderService.StartProcessing(order2.OrderId);
        Console.WriteLine($"Order status: {order2.CurrentState}");

        var specialOrder = new SpecialRequestDecorator(order2, "Allergic to peanuts - use separate utensils", 0, 5);
        Console.WriteLine($"\n{specialOrder.GetOrderDescription()}");
        Console.WriteLine($"Total: ${specialOrder.CalculateFinalCost():F2}");
        Console.WriteLine($"Estimated time: {specialOrder.GetTotalCookTime()} minutes\n");

        Console.WriteLine("=== Order Summary ===");
        foreach (var order in orderService.GetAllOrders())
        {
            Console.WriteLine(order.GetSummary());
        }

        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey();
    }
}
