using DeliveryApp.Strategies;

namespace DeliveryApp.Factories;

public interface IOrderFactory
{
    CustomerOrder CreateRegularOrder(string clientName, string deliveryAddress, string contactPhone);
    CustomerOrder CreatePersonalizedOrder(string clientName, string deliveryAddress, string contactPhone, decimal modFee = 2.5m);
}
