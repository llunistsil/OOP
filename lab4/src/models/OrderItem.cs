namespace DeliveryApp.Models;

public class OrderLine
{
    public DishOption SelectedDish { get; set; } = null!;
    public int Amount { get; set; }
    public bool HasModifications { get; set; }
    public string ModificationNotes { get; set; } = string.Empty;
    public decimal ExtraCost { get; set; }

    public decimal CalculateTotal() => (SelectedDish.Cost + ExtraCost) * Amount;

    public string GetItemInfo()
    {
        if (HasModifications)
            return $"{Amount}x {SelectedDish.Title} [Mods: {ModificationNotes}]";
        return $"{Amount}x {SelectedDish.Title}";
    }

    public int GetCookTime()
    {
        var baseTime = SelectedDish.CookTime;
        return HasModifications ? baseTime + 5 : baseTime;
    }
}
