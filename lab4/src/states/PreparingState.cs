using DeliveryApp.Models;

namespace DeliveryApp.States;

public class CookingState : IOrderState
{
    public OrderState Status => OrderState.Cooking;

    public void ProcessOrder(CustomerOrder order) {}
    public void CancelOrder(CustomerOrder order) => order.ChangeState(new RejectedState());
    public void DeliverOrder(CustomerOrder order) => order.ChangeState(new OnTheWayState());
}
