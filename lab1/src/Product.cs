namespace VendingMachineLib;

public class Product
{
    private string _title;
    private int _cost;
    private int _stockCount;

    public Product(string title, int cost, int stockCount)
    {
        _title = title;
        _cost = cost;
        _stockCount = stockCount;
    }

    public Product(Product source) : this(source._title, source._cost, source._stockCount) { }

    public string Title => _title;
    public int StockCount => _stockCount;
    public int Cost => _cost;
    public void DecreaseStock() => _stockCount--;
    public void IncreaseStock(int amount) => _stockCount += amount;

    public static bool IsValidName(string name) => !string.IsNullOrWhiteSpace(name);

    public static bool IsValidCost(string costInput)
    {
        return int.TryParse(costInput, out int parsed) && parsed > 0;
    }

    public static bool IsValidStock(string stockInput)
    {
        return int.TryParse(stockInput, out int parsed) && parsed > 0;
    }

    public override string ToString() => $"{_title} {_cost} {_stockCount}";
}

public class Inventory
{
    private readonly Dictionary<string, Product> _items;

    public Inventory()
    {
        _items = new Dictionary<string, Product>();
        InitializeDefaultItems();
    }

    public Product? FindProduct(string name)
    {
        if (_items.Count == 0) return null;
        if (!_items.ContainsKey(name)) return null;

        Product product = _items[name];
        return product.StockCount > 0 ? product : null;
    }

    public void AddProduct(Product product)
    {
        string title = product.Title;
        Product? existing = FindProduct(title);

        if (existing == null)
        {
            _items.Add(title, product);
        }
        else
        {
            _items[title].IncreaseStock(product.StockCount);
        }
    }

    public override string ToString()
    {
        var output = new System.Text.StringBuilder();
        output.AppendLine("title cost stock");

        foreach (var entry in _items)
        {
            output.AppendLine(entry.Value.ToString());
        }

        return output.ToString();
    }

    private void InitializeDefaultItems()
    {
        AddProduct(new Product("cola", 3, 25));
        AddProduct(new Product("candy", 2, 15));
    }
}
