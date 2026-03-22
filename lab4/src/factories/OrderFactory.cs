using DeliveryApp.Strategies;

namespace DeliveryApp.Factories;

public class OrderFactory : IOrderFactory
{
    public OrderFactory() {}

    public CustomerOrder CreateRegularOrder(string clientName, string deliveryAddress, string contactPhone)
        => new CustomerOrder(new RegularOrderStrategy(), clientName, deliveryAddress, contactPhone);

    public CustomerOrder CreatePersonalizedOrder(string clientName, string deliveryAddress, string contactPhone, decimal modFee = 2.5m)
        => new CustomerOrder(new PersonalizedOrderStrategy(modFee), clientName, deliveryAddress, contactPhone);
}
