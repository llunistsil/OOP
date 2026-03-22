using DeliveryApp.Models;

namespace DeliveryApp.States;

public class RejectedState : IOrderState
{
    public OrderState Status => OrderState.Rejected;

    public void ProcessOrder(CustomerOrder order) => throw new Exception("Cannot process rejected order");
    public void CancelOrder(CustomerOrder order) {}
    public void DeliverOrder(CustomerOrder order) => throw new Exception("Cannot deliver rejected order");
}
