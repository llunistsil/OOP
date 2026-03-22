namespace DeliveryApp.Commands;

public class UpdateDeliveryAddressCommand : IOrderCommand
{
    private CustomerOrder _order;
    private string _previousAddress;
    private string _updatedAddress;

    public string Description => $"Update delivery address from '{_previousAddress}' to '{_updatedAddress}'";

    public UpdateDeliveryAddressCommand(CustomerOrder order, string updatedAddress)
    {
        _order = order;
        _previousAddress = order.DeliveryAddress;
        _updatedAddress = updatedAddress;
    }

    public void Execute() => _order.DeliveryAddress = _updatedAddress;
    public void Undo() => _order.DeliveryAddress = _previousAddress;
}
