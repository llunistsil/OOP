namespace VendingMachineLib;

public class VendingMachine
{
    private static bool _isRunning;
    private Inventory _inventory;
    private CoinCollection _coinCollection;
    private AdministratorOperator _adminOperator;
    private CustomerOperator _customerOperator;
    private bool _isAdminMode;
    private ulong _accumulatedFunds;

    protected static void HandleExit(object? sender, ConsoleCancelEventArgs args)
    {
        Console.WriteLine("\nExiting application...");
        Console.WriteLine("Press Enter to close.");
        args.Cancel = true;
        _isRunning = false;
    }

    public VendingMachine()
    {
        _inventory = new Inventory();
        _coinCollection = new CoinCollection();
        _adminOperator = new AdministratorOperator();
        _customerOperator = new CustomerOperator();

        Console.CancelKeyPress += HandleExit;
        _isRunning = false;
        ResetFunds();
    }

    public Inventory GetInventory() => _inventory;
    public void ResetFunds() => _accumulatedFunds = 0;
    public void AddFunds(int amount) => _accumulatedFunds += (ulong)amount;
    public string GetChangeDisplay(int balance) => FormatChange(CalculateChange(balance));
    public bool IsRunning() => _isRunning;

    public void DisplayMessage(string message)
    {
        if (_isRunning) Console.WriteLine(message);
    }

    public void ClearAccumulatedFunds() => _accumulatedFunds = 0;

    public void SelectMode()
    {
        DisplayMessage($"Active mode: {(_isAdminMode ? "administrator" : "customer")}");
        DisplayMessage("Type 'mode' to switch, or press Enter to continue:");
        string? input = Console.ReadLine();

        if (input == "mode")
        {
            _isAdminMode = !_isAdminMode;
        }
    }

    public void Start()
    {
        _isRunning = true;

        while (_isRunning)
        {
            SelectMode();

            Operator activeOperator = _isAdminMode ? _adminOperator : _customerOperator;
            activeOperator.Execute(this);
        }
    }

    private Dictionary<Coin, int> CalculateChange(int balance)
    {
        var change = new Dictionary<Coin, int>();

        foreach (Coin coin in _coinCollection.GetAll())
        {
            int count = 0;
            int remaining = balance;

            while (remaining >= coin.GetValue())
            {
                count++;
                remaining -= coin.GetValue();
            }

            change[coin] = count;
        }

        return change;
    }

    private string FormatChange(Dictionary<Coin, int> change)
    {
        var output = new System.Text.StringBuilder();

        foreach (var entry in change)
        {
            output.AppendLine($"{entry.Key}: {entry.Value}");
        }

        return output.ToString();
    }
}
