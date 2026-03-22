namespace DeliveryApp.Strategies;

public class RegularOrderStrategy : IOrderTypeStrategy
{
    public decimal CalculateTotal(decimal subtotal)
    {
        var shippingCost = 3.5m;
        var serviceFee = subtotal * 0.08m;
        return subtotal + shippingCost + serviceFee;
    }

    public int GetPreparationTime(int basePreparationTime) => basePreparationTime + 25;
    public string GetOrderType() => "Regular";
    public bool CanAddCustomItems() => false;
}
