using DeliveryApp.Models;

namespace DeliveryApp.Commands;

public class AddStandardDishCommand : IOrderCommand
{
    private CustomerOrder _order;
    private DishOption _dish;
    private int _amount;
    private OrderLine? _addedLine;

    public string Description => $"Add {_amount}x {_dish.Title} to order";

    public AddStandardDishCommand(CustomerOrder order, DishOption dish, int amount)
    {
        _order = order;
        _dish = dish;
        _amount = amount;
        _addedLine = null;
    }

    public void Execute()
    {
        if (!_dish.CanModify && _order.OrderCategory == "Personalized")
            throw new Exception("Cannot add non-modifiable dish to personalized order");

        _addedLine = new OrderLine
        {
            SelectedDish = _dish,
            Amount = _amount,
            HasModifications = false,
            ModificationNotes = string.Empty,
            ExtraCost = 0
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
