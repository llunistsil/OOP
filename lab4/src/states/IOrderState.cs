using DeliveryApp.Models;

namespace DeliveryApp.States;

public interface IOrderState
{
    OrderState Status { get; }
    void ProcessOrder(CustomerOrder order);
    void CancelOrder(CustomerOrder order);
    void DeliverOrder(CustomerOrder order);
}
