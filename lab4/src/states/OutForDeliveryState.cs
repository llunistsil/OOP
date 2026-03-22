using DeliveryApp.Models;

namespace DeliveryApp.States;

public class OnTheWayState : IOrderState
{
    public OrderState Status => OrderState.OnTheWay;

    public void ProcessOrder(CustomerOrder order) {}
    public void CancelOrder(CustomerOrder order) => throw new Exception("Cannot cancel order that is on the way");
    public void DeliverOrder(CustomerOrder order) => order.ChangeState(new CompletedState());
}
