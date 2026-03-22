using DeliveryApp.Models;

namespace DeliveryApp.Commands;

public class AddCustomDishCommand : IOrderCommand
{
    private CustomerOrder _order;
    private DishOption _dish;
    private int _amount;
    private string _modifications;
    private decimal _modCost;
    private OrderLine? _addedLine;

    public string Description => $"Add {_amount}x {_dish.Title} with {_modifications} to order";

    public AddCustomDishCommand(CustomerOrder order, DishOption dish, int amount, string modifications, decimal modCost)
    {
        _order = order;
        _dish = dish;
        _amount = amount;
        _modifications = modifications;
        _modCost = modCost;
        _addedLine = null;
    }

    public void Execute()
    {
        if (_order.OrderCategory != "Personalized")
            throw new Exception("Cannot add custom dishes to regular order");
        if (!_dish.CanModify)
            throw new Exception("This dish cannot be modified");

        _addedLine = new OrderLine
        {
            SelectedDish = _dish,
            Amount = _amount,
            HasModifications = true,
            ModificationNotes = _modifications,
            ExtraCost = _modCost
        };

        _order.OrderLines.Add(_addedLine);
    }

    public void Undo()
    {
        if (_addedLine != null)
        {
            _order.OrderLines.Remove(_addedLine);
            _addedLine = null;
        }
    }
}
