using System.Text;
using DeliveryApp.Commands;
using DeliveryApp.Decorators;
using DeliveryApp.Models;
using DeliveryApp.States;
using DeliveryApp.Strategies;

namespace DeliveryApp;

public class CustomerOrder : IOrderDecorator
{
    private IOrderState _currentState;
    private Stack<IOrderCommand> _commandHistory = new Stack<IOrderCommand>();
    private string _deliveryNotes = string.Empty;
    private decimal _extraFees = 0;
    private bool _priorityDelivery = false;

    public string OrderId { get; private set; }
    public string ClientName { get; set; }
    public string DeliveryAddress { get; set; }
    public string ContactPhone { get; set; }
    public DateTime CreatedAt { get; private set; }
    public List<OrderLine> OrderLines { get; private set; }
    public IOrderTypeStrategy OrderStrategy { get; private set; }

    public OrderState CurrentState => _currentState.Status;
    public string OrderCategory => OrderStrategy.GetOrderType();

    public CustomerOrder(IOrderTypeStrategy strategy, string clientName, string address, string phone)
    {
        OrderId = Guid.NewGuid().ToString();
        CreatedAt = DateTime.Now;
        OrderLines = new List<OrderLine>();
        OrderStrategy = strategy;
        _currentState = new CookingState();
        ClientName = clientName;
        DeliveryAddress = address;
        ContactPhone = phone;
    }

    public void ChangeState(IOrderState state) => _currentState = state;
    public void StartProcessing() => _currentState.ProcessOrder(this);
    public void RejectOrder() => _currentState.CancelOrder(this);
    public void CompleteOrder() => _currentState.DeliverOrder(this);
    public IEnumerable<string> GetHistory() => _commandHistory.Select(cmd => cmd.Description);
    public void EnablePriorityDelivery(bool enable) => _priorityDelivery = enable;
    public decimal GetSubtotal() => OrderLines.Sum(line => line.CalculateTotal());
    public decimal GetFinalTotal() => CalculateFinalCost();

    public string GetSummary()
        => new StringBuilder()
            .AppendLine($"Order Details:")
            .AppendLine($"Order ID: {OrderId}")
            .AppendLine($"Client: {ClientName}")
            .AppendLine($"Address: {DeliveryAddress}")
            .AppendLine($"Phone: {ContactPhone}")
            .AppendLine($"Category: {OrderCategory}")
            .AppendLine($"State: {CurrentState}")
            .AppendLine($"Lines: {OrderLines.Count}")
            .AppendLine($"Subtotal: ${GetSubtotal():F2}")
            .AppendLine($"Final Total: ${GetFinalTotal():F2}")
            .AppendLine($"Cook Time: {GetTotalCookTime()} minutes")
            .ToString();

    public void RunCommand(IOrderCommand command)
    {
        command.Execute();
        _commandHistory.Push(command);
    }

    public void RevertLastCommand()
    {
        if (_commandHistory.Count > 0)
        {
            var command = _commandHistory.Pop();
            command.Undo();
        }
    }

    public void AddDeliveryNotes(string notes, decimal extraFee = 0)
    {
        _deliveryNotes = notes;
        _extraFees = extraFee;
    }

    public string GetOrderDescription()
    {
        var itemsInfo = string.Join(", ", OrderLines.Select(line => line.GetItemInfo()));
        var desc = $"{OrderCategory} Order #{OrderId}: {itemsInfo}";

        if (!string.IsNullOrEmpty(_deliveryNotes))
            desc += $" [Notes: {_deliveryNotes}]";
        if (_priorityDelivery)
            desc += " [PRIORITY]";

        return desc;
    }

    public decimal CalculateFinalCost()
    {
        var lineCost = OrderLines.Sum(line => line.CalculateTotal());
        var total = OrderStrategy.CalculateTotal(lineCost);
        total += _extraFees;

        if (_priorityDelivery)
            total += 5.0m;

        return total;
    }

    public int GetTotalCookTime()
    {
        var maxLineTime = OrderLines.Any() ? OrderLines.Max(line => line.GetCookTime()) : 0;
        var totalTime = OrderStrategy.GetPreparationTime(maxLineTime);
        totalTime += !string.IsNullOrEmpty(_deliveryNotes) ? 3 : 0;

        if (_priorityDelivery)
            totalTime = Math.Max(10, totalTime / 2);

        return totalTime;
    }

    public void AddStandardDish(DishOption dish, int amount = 1)
    {
        if (!dish.CanModify && OrderStrategy.CanAddCustomItems())
            throw new Exception("Cannot add non-modifiable dish to custom order");

        var line = new OrderLine
        {
            SelectedDish = dish,
            Amount = amount,
            HasModifications = false,
            ModificationNotes = string.Empty,
            ExtraCost = 0
        };

        OrderLines.Add(line);
    }

    public void AddCustomDish(DishOption dish, int amount, string modifications, decimal modCost = 0)
    {
        if (!OrderStrategy.CanAddCustomItems())
            throw new Exception("Cannot add custom dishes to standard order");
        if (!dish.CanModify)
            throw new Exception("This dish cannot be modified");

        var line = new OrderLine
        {
            SelectedDish = dish,
            Amount = amount,
            HasModifications = true,
            ModificationNotes = modifications,
            ExtraCost = modCost
        };

        OrderLines.Add(line);
    }
}
