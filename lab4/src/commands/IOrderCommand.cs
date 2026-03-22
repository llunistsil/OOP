namespace DeliveryApp.Commands;

public interface IOrderCommand
{
    string Description { get; }
    void Execute();
    void Undo();
}
