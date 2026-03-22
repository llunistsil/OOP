using DeliveryApp.Commands;
using DeliveryApp.Data;
using DeliveryApp.Models;
using DeliveryApp.Strategies;

namespace DeliveryApp.Services;

public class OrderManagementService : IOrderManagementService
{
    private Dictionary<string, CustomerOrder> _orders;
    private Catalog _catalog;

    public OrderManagementService(Catalog catalog)
    {
        _orders = new Dictionary<string, CustomerOrder>();
        _catalog = catalog;
    }

    public IEnumerable<CustomerOrder> GetAllOrders() => _orders.Values;

    public decimal GetOrderTotal(string orderId)
        => _orders.ContainsKey(orderId) ? _orders[orderId].CalculateFinalCost() : 0;

    public IEnumerable<string> GetActionHistory(string orderId)
        => _orders.ContainsKey(orderId) ? _orders[orderId].GetHistory() : Enumerable.Empty<string>();

    public CustomerOrder CreateRegularOrder(string clientName, string deliveryAddress, string contactPhone)
    {
        var strategy = new RegularOrderStrategy();
        var order = new CustomerOrder(strategy, clientName, deliveryAddress, contactPhone);
        _orders[order.OrderId] = order;
        return order;
    }

    public CustomerOrder CreatePersonalizedOrder(string clientName, string deliveryAddress, string contactPhone, decimal modFee = 2.5m)
    {
        var strategy = new PersonalizedOrderStrategy(modFee);
        var order = new CustomerOrder(strategy, clientName, deliveryAddress, contactPhone);
        _orders[order.OrderId] = order;
        return order;
    }

    public CustomerOrder FindOrder(string orderId)
    {
        if (!_orders.ContainsKey(orderId))
            throw new Exception($"Order #{orderId} not found in system");
        return _orders[orderId];
    }

    public void AddStandardDish(string orderId, DishOption dish, int quantity = 1)
    {
        if (!_orders.ContainsKey(orderId))
            throw new Exception($"Order #{orderId} not found in system");

        var command = new AddStandardDishCommand(_orders[orderId], dish, quantity);
        _orders[orderId].RunCommand(command);
    }

    public void AddCustomDish(string orderId, DishOption dish, int quantity, string modifications, decimal modPrice = 0)
    {
        if (!_orders.ContainsKey(orderId))
            throw new Exception($"Order #{orderId} not found in system");

        var command = new AddCustomDishCommand(_orders[orderId], dish, quantity, modifications, modPrice);
        _orders[orderId].RunCommand(command);
    }

    public void UpdateDeliveryAddress(string orderId, string newAddress)
    {
        if (!_orders.ContainsKey(orderId))
            throw new Exception($"Order #{orderId} not found in system");

        var command = new UpdateDeliveryAddressCommand(_orders[orderId], newAddress);
        _orders[orderId].RunCommand(command);
    }

    public void RevertLastAction(string orderId)
    {
        if (_orders.ContainsKey(orderId))
            _orders[orderId].RevertLastCommand();
    }

    public void StartProcessing(string orderId)
    {
        if (_orders.ContainsKey(orderId))
            _orders[orderId].StartProcessing();
    }

    public void RejectOrder(string orderId)
    {
        if (_orders.ContainsKey(orderId))
            _orders[orderId].RejectOrder();
    }

    public void FinishOrder(string orderId)
    {
        if (_orders.ContainsKey(orderId))
            _orders[orderId].CompleteOrder();
    }
}
