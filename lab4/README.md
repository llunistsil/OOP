# Лабораторная работа №4: Система управления заказами доставки еды

## Задание

Разработать систему для управления заказами в службе доставки еды. Система должна:

- Поддерживать создание и управление заказами, включая возможность выбора блюд из меню
- Обрабатывать различные типы заказов: стандартные заказы и заказы с особенными условиями (быстрая доставка, персональные предпочтения)
- Предоставлять возможность отслеживания состояния заказа: подготовка, доставка, выполнен
- Включать функции для расчета стоимости заказа с учетом скидок, налогов, платы за доставку
- Логика должна быть покрыта unit-тестами
- CLI не нужен
- Использовать не менее 5 паттернов ООП с четким обоснованием выбора

## Описание решения

### Архитектура приложения

#### 1. State Pattern - Управление состоянием заказа

**Назначение:** Управление жизненным циклом заказа через конечный автомат состояний.

**Интерфейс `IOrderState`:**
- `ProcessOrder(CustomerOrder)` - обработка заказа
- `CancelOrder(CustomerOrder)` - отмена заказа
- `DeliverOrder(CustomerOrder)` - завершение доставки

**Классы состояний:**
- `CookingState` (Подготовка) - начальное состояние, позволяет отменить или начать доставку
- `OnTheWayState` (Доставка) - заказ в пути, позволяет завершить доставку
- `DeliveredState` (Выполнен) - терминальное состояние
- `RejectedState` (Отменен) - терминальное состояние

**Обоснование:** State Pattern позволяет инкапсулировать логику переходов между состояниями и избежать условных операторов для проверки текущего состояния.

```csharp
public class CookingState : IOrderState
{
    public void ProcessOrder(CustomerOrder order) { }
    public void CancelOrder(CustomerOrder order) => order.ChangeState(new RejectedState());
    public void DeliverOrder(CustomerOrder order) => order.ChangeState(new OnTheWayState());
}
```

#### 2. Strategy Pattern - Различные типы заказов

**Назначение:** Инкапсуляция алгоритмов расчета стоимости и времени приготовления для разных типов заказов.

**Интерфейс `IOrderTypeStrategy`:**
- `CalculateTotal(decimal subtotal)` - расчет итоговой стоимости
- `GetPreparationTime(int baseTime)` - расчет времени приготовления
- `GetOrderType()` - получение типа заказа
- `CanAddCustomItems()` - возможность добавления пользовательских модификаций

**Реализации:**
- `RegularOrderStrategy` - стандартный заказ с базовой стоимостью доставки (3.5) и сервисным сбором (8%)
- `CustomOrderStrategy` - заказ с индивидуальными параметрами

**Обоснование:** Strategy Pattern позволяет легко добавлять новые типы заказов без изменения кода класса заказа.

```csharp
public class RegularOrderStrategy : IOrderTypeStrategy
{
    public decimal CalculateTotal(decimal subtotal)
    {
        var shippingCost = 3.5m;
        var serviceFee = subtotal * 0.08m;
        return subtotal + shippingCost + serviceFee;
    }
}
```

#### 3. Command Pattern - Операции с заказом

**Назначение:** Инкапсуляция операций над заказом как объектов с возможностью отмены.

**Интерфейс `IOrderCommand`:**
- `Execute()` - выполнение команды
- `Undo()` - отмена команды
- `Description` - описание команды для истории

**Классы команд:**
- `AddStandardItemCommand` - добавление стандартного блюда
- `AddCustomItemCommand` - добавление блюда с модификациями
- `UpdateOrderCommand` - обновление параметров заказа

**Обоснование:** Command Pattern обеспечивает возможность отмены операций и ведения истории изменений заказа.

```csharp
public interface IOrderCommand
{
    string Description { get; }
    void Execute();
    void Undo();
}
```

#### 4. Factory Pattern - Создание заказов

**Назначение:** Централизованное создание заказов различных типов.

**Интерфейс `IOrderFactory`:**
- `CreateStandardOrder(...)` - создание стандартного заказа
- `CreateCustomOrder(...)` - создание заказа с особенностями

**Класс `OrderFactory`:**
- Реализует фабричные методы для создания заказов
- Возвращает объект `CustomerOrder` с соответствующей стратегией

**Обоснование:** Factory Pattern инкапсулирует логику создания объектов и позволяет легко изменять процесс инициализации заказов.

```csharp
public class OrderFactory : IOrderFactory
{
    public CustomerOrder CreateStandardOrder(string client, string address, string phone)
    {
        return new CustomerOrder(new RegularOrderStrategy(), client, address, phone);
    }
}
```

#### 5. Decorator Pattern - Модификация заказов

**Назначение:** Динамическое добавление дополнительных возможностей к заказам.

**Интерфейс `IOrderDecorator`:**
- `GetOrderDescription()` - получение описания заказа
- `CalculateFinalCost()` - расчет итоговой стоимости
- `GetTotalCookTime()` - получение времени приготовления

**Классы декораторов:**
- `SpecialRequestDecorator` - добавление особых пожеланий к заказу
- `UrgentDeliveryDecorator` - приоритетная доставка

**Обоснование:** Decorator Pattern позволяет добавлять новые функции к заказам динамически без изменения базового класса.

```csharp
public class SpecialRequestDecorator : IOrderDecorator
{
    private IOrderDecorator _wrappedOrder;
    private string _requestNotes;
    private decimal _extraCharge;

    public decimal CalculateFinalCost() => _wrappedOrder.CalculateFinalCost() + _extraCharge;
}
```

#### 6. Facade Pattern - Упрощенный интерфейс системы

**Назначение:** Предоставление простого интерфейса к сложной подсистеме управления заказами.

**Класс `OrderService`:**
- Единая точка входа для клиентского кода
- Скрывает сложность работы с состояниями, командами, стратегиями
- Предоставляет высокоуровневые методы для управления заказами

**Обоснование:** Facade Pattern упрощает взаимодействие с системой, скрывая детали реализации паттернов State, Strategy, Command.

### Основные классы

#### CustomerOrder (Заказ клиента)

**Свойства:**
- `OrderId`, `ClientName`, `DeliveryAddress`, `ContactPhone`
- `OrderLines` - список позиций заказа
- `OrderStrategy` - стратегия типа заказа
- `CurrentState` - текущее состояние заказа

**Методы:**
- `RunCommand(IOrderCommand)` - выполнение команды с сохранением в истории
- `RevertLastCommand()` - отмена последней команды
- `ChangeState(IOrderState)` - изменение состояния
- `StartProcessing()`, `RejectOrder()`, `CompleteOrder()` - управление жизненным циклом
- `CalculateFinalCost()` - расчет итоговой стоимости
- `GetSummary()` - получение полной информации о заказе

#### OrderLine (Позиция заказа)

**Свойства:**
- `SelectedDish` - выбранное блюдо
- `Amount` - количество
- `HasModifications` - наличие модификаций
- `ModificationNotes` - описание модификаций
- `ExtraCost` - дополнительная стоимость

### Покрытие тестами

- `MenuTests.cs` - тесты меню и блюд
- `OrderManagementTests.cs` - тесты управления заказами:
  - Создание заказов различных типов
  - Переходы между состояниями
  - Расчет стоимости
  - Работа с командами (Execute/Undo)
  - Применение декораторов

## Таблица паттернов

| Паттерн | Классы | Назначение |
|---------|--------|------------|
| State | IOrderState, CookingState, OnTheWayState, DeliveredState, RejectedState | Управление жизненным циклом заказа |
| Strategy | IOrderTypeStrategy, RegularOrderStrategy, CustomOrderStrategy | Различные алгоритмы расчета стоимости |
| Command | IOrderCommand, AddStandardItemCommand, AddCustomItemCommand, UpdateOrderCommand | Операции с отменой действий |
| Factory | IOrderFactory, OrderFactory | Создание заказов различных типов |
| Decorator | IOrderDecorator, SpecialRequestDecorator, UrgentDeliveryDecorator | Динамическое добавление функций |
| Facade | OrderService | Упрощенный интерфейс к системе |
