namespace DeliveryApp.Decorators;

public class PriorityDeliveryDecorator : IOrderDecorator
{
    private IOrderDecorator _wrappedOrder;
    private decimal _priorityFee;

    public PriorityDeliveryDecorator(IOrderDecorator order, decimal priorityFee = 5.0m)
    {
        _wrappedOrder = order;
        _priorityFee = priorityFee;
    }

    public string GetOrderDescription() => $"{_wrappedOrder.GetOrderDescription()} [Priority Delivery]";
    public decimal CalculateFinalCost() => _wrappedOrder.CalculateFinalCost() + _priorityFee;
    public int GetTotalCookTime() => Math.Max(10, _wrappedOrder.GetTotalCookTime() / 2);
}
