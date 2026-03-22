using DeliveryApp;
using DeliveryApp.Commands;
using DeliveryApp.Data;
using DeliveryApp.Decorators;
using DeliveryApp.Factories;
using DeliveryApp.Models;
using DeliveryApp.Services;
using DeliveryApp.States;
using DeliveryApp.Strategies;

namespace Lab4.Tests.Services;

public class OrderManagementTests
{
    private readonly Catalog _catalog;
    private readonly OrderManagementService _orderService;

    public OrderManagementTests()
    {
        _catalog = new Catalog();
        _orderService = new OrderManagementService(_catalog);
    }

    [Fact(DisplayName = "DishOption constructor sets properties correctly")]
    public void DishOption_Constructor_SetsPropertiesCorrectly()
    {
        var dish = new DishOption("TEST001", "Test Burger", 12.99m, "Burgers", 15, true);

        Assert.Equal("TEST001", dish.Code);
        Assert.Equal("Test Burger", dish.Title);
        Assert.Equal(12.99m, dish.Cost);
        Assert.Equal("Burgers", dish.Group);
        Assert.Equal(15, dish.CookTime);
        Assert.True(dish.CanModify);
    }

    [Fact(DisplayName = "OrderLine CalculateTotal returns correct amount")]
    public void OrderLine_CalculateTotal_ReturnsCorrectAmount()
    {
        var dish = new DishOption("1", "Pizza", 12.99m, "Pizza", 20);
        var line = new OrderLine
        {
            SelectedDish = dish,
            Amount = 2,
            HasModifications = true,
            ModificationNotes = "Extra cheese",
            ExtraCost = 1.5m
        };

        var total = line.CalculateTotal();

        Assert.Equal((12.99m + 1.5m) * 2, total);
    }

    [Fact(DisplayName = "OrderLine GetItemInfo standard item returns correct format")]
    public void OrderLine_GetItemInfo_StandardItem_ReturnsCorrectFormat()
    {
        var dish = new DishOption("1", "Pizza", 12.99m, "Pizza", 20);
        var line = new OrderLine
        {
            SelectedDish = dish,
            Amount = 2,
            HasModifications = false
        };

        var info = line.GetItemInfo();

        Assert.Equal("2x Pizza", info);
    }

    [Fact(DisplayName = "OrderLine GetItemInfo modified item returns correct format")]
    public void OrderLine_GetItemInfo_ModifiedItem_ReturnsCorrectFormat()
    {
        var dish = new DishOption("1", "Pizza", 12.99m, "Pizza", 20);
        var line = new OrderLine
        {
            SelectedDish = dish,
            Amount = 1,
            HasModifications = true,
            ModificationNotes = "Extra cheese"
        };

        var info = line.GetItemInfo();

        Assert.Equal("1x Pizza [Mods: Extra cheese]", info);
    }

    [Fact(DisplayName = "OrderLine GetCookTime modified item adds extra time")]
    public void OrderLine_GetCookTime_ModifiedItem_AddsExtraTime()
    {
        var dish = new DishOption("1", "Pizza", 12.99m, "Pizza", 20);
        var line = new OrderLine
        {
            SelectedDish = dish,
            Amount = 1,
            HasModifications = true
        };

        var cookTime = line.GetCookTime();

        Assert.Equal(25, cookTime);
    }

    [Fact(DisplayName = "CookingState ProcessOrder does nothing")]
    public void CookingState_ProcessOrder_DoesNothing()
    {
        var state = new CookingState();
        var order = new CustomerOrder(new RegularOrderStrategy(), "Test Customer", "Test Address", "555-1234");

        var exception = Record.Exception(() => state.ProcessOrder(order));
        Assert.Null(exception);
    }

    [Fact(DisplayName = "CookingState CancelOrder changes to RejectedState")]
    public void CookingState_CancelOrder_ChangesToRejectedState()
    {
        var state = new CookingState();
        var order = new CustomerOrder(new RegularOrderStrategy(), "Test Customer", "Test Address", "555-1234");

        state.CancelOrder(order);

        Assert.Equal(OrderState.Rejected, order.CurrentState);
    }

    [Fact(DisplayName = "CookingState DeliverOrder changes to OnTheWayState")]
    public void CookingState_DeliverOrder_ChangesToOnTheWayState()
    {
        var state = new CookingState();
        var order = new CustomerOrder(new RegularOrderStrategy(), "Test Customer", "Test Address", "555-1234");

        state.DeliverOrder(order);

        Assert.Equal(OrderState.OnTheWay, order.CurrentState);
    }

    [Fact(DisplayName = "OnTheWayState CancelOrder throws exception")]
    public void OnTheWayState_CancelOrder_ThrowsException()
    {
        var state = new OnTheWayState();
        var order = new CustomerOrder(new RegularOrderStrategy(), "Test Customer", "Test Address", "555-1234");

        Assert.Throws<Exception>(() => state.CancelOrder(order));
    }

    [Fact(DisplayName = "OnTheWayState DeliverOrder changes to CompletedState")]
    public void OnTheWayState_DeliverOrder_ChangesToCompletedState()
    {
        var state = new OnTheWayState();
        var order = new CustomerOrder(new RegularOrderStrategy(), "Test Customer", "Test Address", "555-1234");

        state.DeliverOrder(order);

        Assert.Equal(OrderState.Completed, order.CurrentState);
    }

    [Fact(DisplayName = "CompletedState ProcessOrder throws exception")]
    public void CompletedState_ProcessOrder_ThrowsException()
    {
        var state = new CompletedState();
        var order = new CustomerOrder(new RegularOrderStrategy(), "Test Customer", "Test Address", "555-1234");

        Assert.Throws<Exception>(() => state.ProcessOrder(order));
    }

    [Fact(DisplayName = "RejectedState ProcessOrder throws exception")]
    public void RejectedState_ProcessOrder_ThrowsException()
    {
        var state = new RejectedState();
        var order = new CustomerOrder(new RegularOrderStrategy(), "Test", "Test Address", "555-1234");

        var exception = Assert.Throws<Exception>(() => state.ProcessOrder(order));
        Assert.Equal("Cannot process rejected order", exception.Message);
    }

    [Fact(DisplayName = "RegularOrderStrategy CalculateTotal includes shipping and service fee")]
    public void RegularOrderStrategy_CalculateTotal_IncludesShippingAndServiceFee()
    {
        var strategy = new RegularOrderStrategy();
        var subtotal = 50.0m;

        var total = strategy.CalculateTotal(subtotal);

        var expectedServiceFee = 50.0m * 0.08m;
        var expectedShipping = 3.5m;
        var expectedTotal = 50.0m + expectedServiceFee + expectedShipping;
        Assert.Equal(expectedTotal, total);
    }

    [Fact(DisplayName = "RegularOrderStrategy GetOrderType returns Regular")]
    public void RegularOrderStrategy_GetOrderType_ReturnsRegular()
    {
        var strategy = new RegularOrderStrategy();

        var orderType = strategy.GetOrderType();

        Assert.Equal("Regular", orderType);
    }

    [Fact(DisplayName = "RegularOrderStrategy CanAddCustomItems returns false")]
    public void RegularOrderStrategy_CanAddCustomItems_ReturnsFalse()
    {
        var strategy = new RegularOrderStrategy();

        var canAddCustom = strategy.CanAddCustomItems();

        Assert.False(canAddCustom);
    }

    [Fact(DisplayName = "PersonalizedOrderStrategy CalculateTotal includes modification fee")]
    public void PersonalizedOrderStrategy_CalculateTotal_IncludesModificationFee()
    {
        var strategy = new PersonalizedOrderStrategy(3.0m);
        var subtotal = 50.0m;

        var total = strategy.CalculateTotal(subtotal);

        var expectedServiceFee = 50.0m * 0.12m;
        var expectedShipping = 4.0m;
        var expectedTotal = 50.0m + expectedShipping + expectedServiceFee + 3.0m;
        Assert.Equal(expectedTotal, total);
    }

    [Fact(DisplayName = "PersonalizedOrderStrategy CanAddCustomItems returns true")]
    public void PersonalizedOrderStrategy_CanAddCustomItems_ReturnsTrue()
    {
        var strategy = new PersonalizedOrderStrategy();

        var canAddCustom = strategy.CanAddCustomItems();

        Assert.True(canAddCustom);
    }

    [Fact(DisplayName = "AddStandardDishCommand Execute adds item to order")]
    public void AddStandardDishCommand_Execute_AddsItemToOrder()
    {
        var order = new CustomerOrder(new RegularOrderStrategy(), "Test", "Test Address", "555-1234");
        var dish = _catalog.FindDish("PZ001");
        Assert.NotNull(dish);
        var command = new AddStandardDishCommand(order, dish, 2);

        command.Execute();

        Assert.Single(order.OrderLines);
        Assert.Equal(2, order.OrderLines[0].Amount);
        Assert.Equal(dish, order.OrderLines[0].SelectedDish);
    }

    [Fact(DisplayName = "AddStandardDishCommand Undo removes added item")]
    public void AddStandardDishCommand_Undo_RemovesAddedItem()
    {
        var order = new CustomerOrder(new RegularOrderStrategy(), "Test Customer", "Test Address", "555-1234");
        var dish = _catalog.FindDish("PZ001");
        Assert.NotNull(dish);
        var command = new AddStandardDishCommand(order, dish, 2);

        command.Execute();
        command.Undo();

        Assert.Empty(order.OrderLines);
    }

    [Fact(DisplayName = "AddStandardDishCommand Description returns correct format")]
    public void AddStandardDishCommand_Description_ReturnsCorrectFormat()
    {
        var order = new CustomerOrder(new RegularOrderStrategy(), "Test Customer", "Test Address", "555-1234");
        var dish = _catalog.FindDish("PZ001");
        Assert.NotNull(dish);
        var command = new AddStandardDishCommand(order, dish, 2);

        var description = command.Description;

        Assert.Equal("Add 2x Classic Margherita to order", description);
    }

    [Fact(DisplayName = "AddCustomDishCommand Execute adds custom item to order")]
    public void AddCustomDishCommand_Execute_AddsCustomItemToOrder()
    {
        var order = new CustomerOrder(new PersonalizedOrderStrategy(), "Test Customer", "Test Address", "555-1234");
        var dish = _catalog.FindDish("BG001");
        Assert.NotNull(dish);
        var command = new AddCustomDishCommand(order, dish, 1, "No onions, extra cheese", 1.5m);

        command.Execute();

        Assert.Single(order.OrderLines);
        Assert.True(order.OrderLines[0].HasModifications);
        Assert.Equal("No onions, extra cheese", order.OrderLines[0].ModificationNotes);
        Assert.Equal(1.5m, order.OrderLines[0].ExtraCost);
    }

    [Fact(DisplayName = "AddCustomDishCommand Execute on regular order throws exception")]
    public void AddCustomDishCommand_Execute_OnRegularOrder_ThrowsException()
    {
        var order = new CustomerOrder(new RegularOrderStrategy(), "Test", "Test Address", "555-1234");
        var dish = _catalog.FindDish("PZ001");
        Assert.NotNull(dish);
        var command = new AddCustomDishCommand(order, dish, 1, "Extra cheese", 1.5m);

        var exception = Assert.Throws<Exception>(() => command.Execute());
        Assert.Equal("Cannot add custom dishes to regular order", exception.Message);
    }

    [Fact(DisplayName = "UpdateDeliveryAddressCommand Execute updates address")]
    public void UpdateDeliveryAddressCommand_Execute_UpdatesAddress()
    {
        var order = new CustomerOrder(new RegularOrderStrategy(), "Test Customer", "Old Address", "555-1234");
        var command = new UpdateDeliveryAddressCommand(order, "New Address");

        command.Execute();

        Assert.Equal("New Address", order.DeliveryAddress);
    }

    [Fact(DisplayName = "UpdateDeliveryAddressCommand Undo restores old address")]
    public void UpdateDeliveryAddressCommand_Undo_RestoresOldAddress()
    {
        var order = new CustomerOrder(new RegularOrderStrategy(), "Test", "Old Address", "555-1234");
        var command = new UpdateDeliveryAddressCommand(order, "New Address");

        command.Execute();
        command.Undo();

        Assert.Equal("Old Address", order.DeliveryAddress);
    }

    [Fact(DisplayName = "OrderFactory CreateRegularOrder returns regular order")]
    public void OrderFactory_CreateRegularOrder_ReturnsRegularOrder()
    {
        var factory = new OrderFactory();

        var order = factory.CreateRegularOrder("John", "123 St", "555-1234");

        Assert.NotNull(order);
        Assert.Equal("Regular", order.OrderCategory);
        Assert.IsType<RegularOrderStrategy>(order.OrderStrategy);
    }

    [Fact(DisplayName = "OrderFactory CreatePersonalizedOrder returns personalized order")]
    public void OrderFactory_CreatePersonalizedOrder_ReturnsPersonalizedOrder()
    {
        var factory = new OrderFactory();

        var order = factory.CreatePersonalizedOrder("Jane", "456 St", "555-5678", 5.0m);

        Assert.NotNull(order);
        Assert.Equal("Personalized", order.OrderCategory);
        Assert.IsType<PersonalizedOrderStrategy>(order.OrderStrategy);
    }

    [Fact(DisplayName = "CustomerOrder constructor sets properties correctly")]
    public void CustomerOrder_Constructor_SetsPropertiesCorrectly()
    {
        var order = new CustomerOrder(new RegularOrderStrategy(), "Test Customer", "Test Address", "555-1234");

        Assert.NotNull(order.OrderId);
        Assert.NotEmpty(order.OrderId);
        Assert.Equal("Test Customer", order.ClientName);
        Assert.Equal("Test Address", order.DeliveryAddress);
        Assert.Equal("555-1234", order.ContactPhone);
        Assert.Equal(OrderState.Cooking, order.CurrentState);
        Assert.Empty(order.OrderLines);
    }

    [Fact(DisplayName = "CustomerOrder AddStandardDish adds item to order")]
    public void CustomerOrder_AddStandardDish_AddsItemToOrder()
    {
        var order = new CustomerOrder(new RegularOrderStrategy(), "Test Customer", "Test Address", "555-1234");
        var dish = _catalog.FindDish("PZ001");
        Assert.NotNull(dish);

        order.AddStandardDish(dish, 2);

        Assert.Single(order.OrderLines);
        Assert.Equal(2, order.OrderLines[0].Amount);
    }

    [Fact(DisplayName = "CustomerOrder CalculateFinalCost returns correct sum")]
    public void CustomerOrder_CalculateFinalCost_ReturnsCorrectSum()
    {
        var order = new CustomerOrder(new RegularOrderStrategy(), "Test Customer", "Test Address", "555-1234");
        var dish1 = _catalog.FindDish("PZ001");
        var dish2 = _catalog.FindDish("SL001");
        Assert.NotNull(dish1);
        Assert.NotNull(dish2);

        order.AddStandardDish(dish1, 1);
        order.AddStandardDish(dish2, 2);

        var total = order.CalculateFinalCost();

        Assert.True(total > 0);
    }

    [Fact(DisplayName = "CustomerOrder GetTotalCookTime calculates correctly")]
    public void CustomerOrder_GetTotalCookTime_CalculatesCorrectly()
    {
        var order = new CustomerOrder(new RegularOrderStrategy(), "Test Customer", "Test Address", "555-1234");
        var dish = _catalog.FindDish("PZ001");
        Assert.NotNull(dish);

        order.AddStandardDish(dish, 1);

        var cookTime = order.GetTotalCookTime();

        Assert.True(cookTime > 0);
    }

    [Fact(DisplayName = "CustomerOrder RunCommand adds to history")]
    public void CustomerOrder_RunCommand_AddsToHistory()
    {
        var order = new CustomerOrder(new RegularOrderStrategy(), "Test Customer", "Test Address", "555-1234");
        var dish = _catalog.FindDish("PZ001");
        Assert.NotNull(dish);
        var command = new AddStandardDishCommand(order, dish, 1);

        order.RunCommand(command);

        var history = order.GetHistory();
        Assert.Single(history);
    }

    [Fact(DisplayName = "CustomerOrder RevertLastCommand removes from history")]
    public void CustomerOrder_RevertLastCommand_RemovesFromHistory()
    {
        var order = new CustomerOrder(new RegularOrderStrategy(), "Test Customer", "Test Address", "555-1234");
        var dish = _catalog.FindDish("PZ001");
        Assert.NotNull(dish);
        var command = new AddStandardDishCommand(order, dish, 1);

        order.RunCommand(command);
        order.RevertLastCommand();

        Assert.Empty(order.OrderLines);
    }

    [Fact(DisplayName = "PriorityDeliveryDecorator adds fee and reduces time")]
    public void PriorityDeliveryDecorator_AddsFeeAndReducesTime()
    {
        var order = new CustomerOrder(new RegularOrderStrategy(), "Test Customer", "Test Address", "555-1234");
        var dish = _catalog.FindDish("PZ001");
        Assert.NotNull(dish);
        order.AddStandardDish(dish, 1);

        var decorator = new PriorityDeliveryDecorator(order);

        var baseCost = order.CalculateFinalCost();
        var decoratedCost = decorator.CalculateFinalCost();
        Assert.True(decoratedCost > baseCost);

        var baseTime = order.GetTotalCookTime();
        var decoratedTime = decorator.GetTotalCookTime();
        Assert.True(decoratedTime <= baseTime);
    }

    [Fact(DisplayName = "SpecialRequestDecorator adds extra charge and time")]
    public void SpecialRequestDecorator_AddsExtraChargeAndTime()
    {
        var order = new CustomerOrder(new RegularOrderStrategy(), "Test Customer", "Test Address", "555-1234");
        var dish = _catalog.FindDish("PZ001");
        Assert.NotNull(dish);
        order.AddStandardDish(dish, 1);

        var decorator = new SpecialRequestDecorator(order, "Allergic to nuts", 2.0m, 5);

        var baseCost = order.CalculateFinalCost();
        var decoratedCost = decorator.CalculateFinalCost();
        Assert.Equal(baseCost + 2.0m, decoratedCost);

        var baseTime = order.GetTotalCookTime();
        var decoratedTime = decorator.GetTotalCookTime();
        Assert.Equal(baseTime + 5, decoratedTime);
    }

    [Fact(DisplayName = "OrderManagementService CreateRegularOrder succeeds")]
    public void OrderManagementService_CreateRegularOrder_Success()
    {
        var order = _orderService.CreateRegularOrder("John Doe", "123 Main St", "555-1234");

        Assert.NotNull(order);
        Assert.Equal("Regular", order.OrderCategory);
        Assert.Equal(OrderState.Cooking, order.CurrentState);
    }

    [Fact(DisplayName = "OrderManagementService CreatePersonalizedOrder succeeds")]
    public void OrderManagementService_CreatePersonalizedOrder_Success()
    {
        var order = _orderService.CreatePersonalizedOrder("Jane Smith", "456 Oak Ave", "555-5678", 5.0m);

        Assert.NotNull(order);
        Assert.Equal("Personalized", order.OrderCategory);
        Assert.Equal(OrderState.Cooking, order.CurrentState);
    }

    [Fact(DisplayName = "OrderManagementService AddStandardDish succeeds")]
    public void OrderManagementService_AddStandardDish_Success()
    {
        var order = _orderService.CreateRegularOrder("John Doe", "123 Main St", "555-1234");
        var pizza = _catalog.FindDish("PZ001");
        Assert.NotNull(pizza);

        _orderService.AddStandardDish(order.OrderId, pizza, 2);

        var retrievedOrder = _orderService.FindOrder(order.OrderId);
        Assert.Single(retrievedOrder.OrderLines);
    }

    [Fact(DisplayName = "OrderManagementService AddCustomDish succeeds")]
    public void OrderManagementService_AddCustomDish_Success()
    {
        var order = _orderService.CreatePersonalizedOrder("Jane Smith", "456 Oak Ave", "555-5678");
        var burger = _catalog.FindDish("BG001");
        Assert.NotNull(burger);

        _orderService.AddCustomDish(order.OrderId, burger, 1, "Extra cheese", 1.5m);

        var retrievedOrder = _orderService.FindOrder(order.OrderId);
        Assert.Single(retrievedOrder.OrderLines);
    }

    [Fact(DisplayName = "OrderManagementService UndoLastAction succeeds")]
    public void OrderManagementService_UndoLastAction_Success()
    {
        var order = _orderService.CreateRegularOrder("John Doe", "123 Main St", "555-1234");
        var pizza = _catalog.FindDish("PZ001");
        var salad = _catalog.FindDish("SL001");
        Assert.NotNull(pizza);
        Assert.NotNull(salad);

        _orderService.AddStandardDish(order.OrderId, pizza, 1);
        _orderService.AddStandardDish(order.OrderId, salad, 1);
        _orderService.RevertLastAction(order.OrderId);

        Assert.Single(_orderService.FindOrder(order.OrderId).OrderLines);
    }

    [Fact(DisplayName = "OrderManagementService StartProcessing changes state")]
    public void OrderManagementService_StartProcessing_ChangesState()
    {
        var order = _orderService.CreateRegularOrder("John Doe", "123 Main St", "555-1234");

        _orderService.StartProcessing(order.OrderId);
        _orderService.FinishOrder(order.OrderId);

        var updatedOrder = _orderService.FindOrder(order.OrderId);
        Assert.Equal(OrderState.OnTheWay, updatedOrder.CurrentState);
    }

    [Fact(DisplayName = "OrderManagementService RejectOrder changes to rejected")]
    public void OrderManagementService_RejectOrder_ChangesToRejected()
    {
        var order = _orderService.CreateRegularOrder("John Doe", "123 Main St", "555-1234");

        _orderService.RejectOrder(order.OrderId);

        Assert.Equal(OrderState.Rejected, _orderService.FindOrder(order.OrderId).CurrentState);
    }

    [Fact(DisplayName = "OrderManagementService FindOrder non-existent throws exception")]
    public void OrderManagementService_FindOrder_NonExistent_ThrowsException()
    {
        Assert.Throws<Exception>(() => _orderService.FindOrder("non-existent-id"));
    }

    [Fact(DisplayName = "OrderManagementService GetAllOrders returns all orders")]
    public void OrderManagementService_GetAllOrders_ReturnsAllOrders()
    {
        _orderService.CreateRegularOrder("John", "Addr1", "555-1111");
        _orderService.CreatePersonalizedOrder("Jane", "Addr2", "555-2222");

        var orders = _orderService.GetAllOrders();

        Assert.Equal(2, orders.Count());
    }

    [Fact(DisplayName = "OrderManagementService GetOrderTotal returns correct amount")]
    public void OrderManagementService_GetOrderTotal_ReturnsCorrectAmount()
    {
        var order = _orderService.CreateRegularOrder("John Doe", "123 Main St", "555-1234");
        var dish = _catalog.FindDish("PZ001");
        Assert.NotNull(dish);
        _orderService.AddStandardDish(order.OrderId, dish, 1);

        var total = _orderService.GetOrderTotal(order.OrderId);

        Assert.True(total > 0);
    }

    [Fact(DisplayName = "OrderManagementService GetActionHistory returns command history")]
    public void OrderManagementService_GetActionHistory_ReturnsCommandHistory()
    {
        var order = _orderService.CreateRegularOrder("John Doe", "123 Main St", "555-1234");
        var dish = _catalog.FindDish("PZ001");
        Assert.NotNull(dish);
        _orderService.AddStandardDish(order.OrderId, dish, 1);

        var history = _orderService.GetActionHistory(order.OrderId);

        Assert.Single(history);
    }

    [Fact(DisplayName = "CompleteOrderFlow RegularOrder succeeds")]
    public void CompleteOrderFlow_RegularOrder_Success()
    {
        var order = _orderService.CreateRegularOrder("John Doe", "123 Main St", "555-1234");
        _orderService.AddStandardDish(order.OrderId, _catalog.FindDish("PZ001")!, 1);
        _orderService.AddStandardDish(order.OrderId, _catalog.FindDish("DR001")!, 2);

        _orderService.StartProcessing(order.OrderId);
        _orderService.FinishOrder(order.OrderId);

        var finalOrder = _orderService.FindOrder(order.OrderId);
        Assert.Equal(OrderState.OnTheWay, finalOrder.CurrentState);
        Assert.Equal(2, finalOrder.OrderLines.Count);
        Assert.True(finalOrder.CalculateFinalCost() > 0);
    }

    [Fact(DisplayName = "CompleteOrderFlow PersonalizedOrder succeeds")]
    public void CompleteOrderFlow_PersonalizedOrder_Success()
    {
        var order = _orderService.CreatePersonalizedOrder("Jane Smith", "456 Oak Ave", "555-5678", 3.0m);
        _orderService.AddCustomDish(order.OrderId, _catalog.FindDish("BG001")!, 1, "No onions", 0.5m);

        _orderService.StartProcessing(order.OrderId);

        var finalOrder = _orderService.FindOrder(order.OrderId);
        Assert.Equal(OrderState.Cooking, finalOrder.CurrentState);
        Assert.Single(finalOrder.OrderLines);
        Assert.True(finalOrder.CalculateFinalCost() > 0);
    }
}
