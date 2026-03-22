namespace DeliveryApp.Strategies;

public class PersonalizedOrderStrategy : IOrderTypeStrategy
{
    private decimal _modificationFee;

    public PersonalizedOrderStrategy(decimal modificationFee = 2.5m)
        => _modificationFee = modificationFee;

    public int GetPreparationTime(int basePreparationTime) => basePreparationTime + 40;
    public string GetOrderType() => "Personalized";
    public bool CanAddCustomItems() => true;

    public decimal CalculateTotal(decimal subtotal)
    {
        var shippingCost = 4.0m;
        var serviceFee = subtotal * 0.12m;
        return subtotal + shippingCost + serviceFee + _modificationFee;
    }
}
