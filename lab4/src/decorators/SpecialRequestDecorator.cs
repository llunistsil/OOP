namespace DeliveryApp.Decorators;

public class SpecialRequestDecorator : IOrderDecorator
{
    private IOrderDecorator _wrappedOrder;
    private string _requestNotes;
    private decimal _extraCharge;
    private int _extraTime;

    public SpecialRequestDecorator(IOrderDecorator order, string requestNotes, decimal extraCharge = 0, int extraTime = 3)
    {
        _wrappedOrder = order;
        _requestNotes = requestNotes;
        _extraCharge = extraCharge;
        _extraTime = extraTime;
    }

    public string GetOrderDescription() => $"{_wrappedOrder.GetOrderDescription()} [Special: {_requestNotes}]";
    public decimal CalculateFinalCost() => _wrappedOrder.CalculateFinalCost() + _extraCharge;
    public int GetTotalCookTime() => _wrappedOrder.GetTotalCookTime() + _extraTime;
}
