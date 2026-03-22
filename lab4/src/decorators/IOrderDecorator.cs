namespace DeliveryApp.Decorators;

public interface IOrderDecorator
{
    string GetOrderDescription();
    decimal CalculateFinalCost();
    int GetTotalCookTime();
}
