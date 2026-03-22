namespace VendingMachineLib;

public abstract class Operator
{
    protected bool _isActive;

    public abstract void Execute(VendingMachine machine);
    public abstract string GetHelpText();
    public abstract string ReadInput();

    protected Operator() => _isActive = false;

    protected abstract void HandleCommand(VendingMachine machine);
    protected string GetInvalidCommandMessage() => "invalid command";
}

public class AdministratorOperator : Operator
{
    public override string GetHelpText()
    {
        return "add: restock item\n" +
               "quit: exit admin mode\n" +
               "help: show this message\n";
    }

    public override string ReadInput()
    {
        Console.Write("[admin]> ");
        return Console.ReadLine() ?? string.Empty;
    }

    public override void Execute(VendingMachine machine)
    {
        machine.DisplayMessage("Administrator mode activated");
        machine.ClearAccumulatedFunds();
        ManageInventory(machine);
    }

    protected override void HandleCommand(VendingMachine machine)
    {
        string command = ReadInput();

        switch (command)
        {
            case "add":
                Product newItem = ReadProductDetails(machine);
                machine.GetInventory().AddProduct(newItem);
                machine.DisplayMessage($"Added {newItem.Title} to inventory");
                break;
            case "quit":
                _isActive = false;
                break;
            case "help":
                machine.DisplayMessage(GetHelpText());
                break;
            default:
                machine.DisplayMessage(GetInvalidCommandMessage());
                break;
        }
    }

    private Product ReadProductDetails(VendingMachine machine)
    {
        string title, costInput, stockInput;

        machine.DisplayMessage("Enter product details:");

        bool waiting = machine.IsRunning();
        do
        {
            machine.DisplayMessage("Enter product name:");
            title = ReadInput();
            if (Product.IsValidName(title)) waiting = false;
        } while (machine.IsRunning() && waiting);

        waiting = machine.IsRunning();
        do
        {
            machine.DisplayMessage("Enter product cost:");
            costInput = ReadInput();
            if (Product.IsValidCost(costInput)) waiting = false;
        } while (machine.IsRunning() && waiting);

        waiting = machine.IsRunning();
        do
        {
            machine.DisplayMessage("Enter stock quantity:");
            stockInput = ReadInput();
            if (Product.IsValidStock(stockInput)) waiting = false;
        } while (machine.IsRunning() && waiting);

        return new Product(title, int.Parse(costInput), int.Parse(stockInput));
    }

    private void ManageInventory(VendingMachine machine)
    {
        _isActive = true;

        machine.DisplayMessage("Administrator can restock items");
        machine.DisplayMessage(GetHelpText());

        while (machine.IsRunning() && _isActive)
        {
            HandleCommand(machine);
        }
    }
}

public class CustomerOperator : Operator
{
    private int _currentBalance;

    public CustomerOperator() : base() => _currentBalance = 0;

    public override string GetHelpText()
    {
        return "\n" +
               "purchase: complete buy\n" +
               "copper: insert copper coin (1)\n" +
               "nickel: insert nickel coin (10)\n" +
               "platinum: insert platinum coin (100)\n" +
               "diamond: insert diamond coin (1000)\n" +
               "help: show available commands\n";
    }

    public override string ReadInput()
    {
        Console.Write("[customer]> ");
        return Console.ReadLine() ?? string.Empty;
    }

    public override void Execute(VendingMachine machine)
    {
        string selectedProduct = string.Empty;

        selectedProduct = SelectProduct(machine);
        ProcessPurchase(selectedProduct, machine);
    }

    protected override void HandleCommand(VendingMachine machine)
    {
        string command = ReadInput();

        switch (command)
        {
            case "purchase":
                _isActive = false;
                break;
            case "copper":
                _currentBalance += InsertCoin(new CopperCoin());
                machine.DisplayMessage(GetBalanceMessage(_currentBalance));
                break;
            case "nickel":
                _currentBalance += InsertCoin(new NickelCoin());
                machine.DisplayMessage(GetBalanceMessage(_currentBalance));
                break;
            case "platinum":
                _currentBalance += InsertCoin(new PlatinumCoin());
                machine.DisplayMessage(GetBalanceMessage(_currentBalance));
                break;
            case "diamond":
                _currentBalance += InsertCoin(new DiamondCoin());
                machine.DisplayMessage(GetBalanceMessage(_currentBalance));
                break;
            case "help":
                machine.DisplayMessage(GetHelpText());
                break;
            default:
                machine.DisplayMessage(GetInvalidCommandMessage());
                break;
        }
    }

    private int InsertCoin(IMonetaryValue coin) => coin.GetValue();

    private string GetBalanceMessage(int balance) => $"Current balance: {balance} copper units";

    private string SelectProduct(VendingMachine machine)
    {
        _isActive = true;
        string result = string.Empty;

        machine.DisplayMessage("Select a product:");
        machine.DisplayMessage(machine.GetInventory().ToString());

        while (machine.IsRunning() && _isActive)
        {
            string input = ReadInput();
            Product? found = machine.GetInventory().FindProduct(input);

            if (found != null)
            {
                _isActive = false;
                result = found.Title;
            }
            else
            {
                machine.DisplayMessage($"Product not found: {input}");
                machine.DisplayMessage(machine.GetInventory().ToString());
            }
        }

        return result;
    }

    private void ProcessPurchase(string productName, VendingMachine machine)
    {
        _isActive = true;

        Product? product = machine.GetInventory().FindProduct(productName);
        if (product == null)
        {
            machine.DisplayMessage($"Product unavailable: {productName}");
            return;
        }

        machine.DisplayMessage(GetHelpText());
        machine.DisplayMessage(GetBalanceMessage(_currentBalance));

        while (machine.IsRunning() && _isActive)
        {
            HandleCommand(machine);
        }

        if (_currentBalance >= product.Cost)
        {
            product.DecreaseStock();
            _currentBalance -= product.Cost;
            machine.AddFunds(product.Cost);
            machine.DisplayMessage($"\nDispensing: {productName}");
            machine.DisplayMessage("Your change:");
            machine.DisplayMessage(machine.GetChangeDisplay(_currentBalance));
            _currentBalance = 0;
        }
        else
        {
            int shortage = product.Cost - _currentBalance;
            machine.DisplayMessage($"Insufficient funds for {productName}. Need {shortage} more.");
            machine.DisplayMessage(GetBalanceMessage(_currentBalance));
            if (machine.IsRunning()) ProcessPurchase(productName, machine);
        }
    }
}
