using DeliveryApp.Models;
using DeliveryApp.Strategies;

namespace DeliveryApp.Services;

public interface IOrderManagementService
{
    CustomerOrder CreateRegularOrder(string clientName, string deliveryAddress, string contactPhone);
    CustomerOrder CreatePersonalizedOrder(string clientName, string deliveryAddress, string contactPhone, decimal modFee = 2.5m);
    void AddStandardDish(string orderId, DishOption dish, int quantity = 1);
    void AddCustomDish(string orderId, DishOption dish, int quantity, string modifications, decimal modPrice = 0);
    void UpdateDeliveryAddress(string orderId, string newAddress);
    void RevertLastAction(string orderId);
    void StartProcessing(string orderId);
    void RejectOrder(string orderId);
    void FinishOrder(string orderId);
    CustomerOrder FindOrder(string orderId);
    IEnumerable<CustomerOrder> GetAllOrders();
    decimal GetOrderTotal(string orderId);
    IEnumerable<string> GetActionHistory(string orderId);
}
