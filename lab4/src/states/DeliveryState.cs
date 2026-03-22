using DeliveryApp.Models;

namespace DeliveryApp.States;

public class CompletedState : IOrderState
{
    public OrderState Status => OrderState.Completed;

    public void ProcessOrder(CustomerOrder order) => throw new Exception("Order already completed");
    public void CancelOrder(CustomerOrder order) => throw new Exception("Cannot cancel completed order");
    public void DeliverOrder(CustomerOrder order) => throw new Exception("Order already completed");
}
