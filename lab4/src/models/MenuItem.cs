namespace DeliveryApp.Models;

public class DishOption
{
    public string Code { get; set; }
    public string Title { get; set; }
    public string Details { get; set; }
    public decimal Cost { get; set; }
    public string Group { get; set; }
    public int CookTime { get; set; }
    public bool CanModify { get; set; }

    public DishOption(string code, string title, decimal cost, string group, int cookTime, bool canModify = true)
    {
        Code = code;
        Title = title;
        Cost = cost;
        Group = group;
        CookTime = cookTime;
        CanModify = canModify;
        Details = string.Empty;
    }
}
