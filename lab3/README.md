# Лабораторная работа №3: Система управления инвентарем для RPG

## Задание

Разработать систему управления инвентарем для ролевой игры. Система должна:

- Позволять игрокам добавлять различные типы предметов в инвентарь: оружие, броня, зелья, квестовые предметы
- Учитывать уникальные свойства каждого предмета (урон для оружия, защита для брони)
- Предоставлять возможность использовать или экипировать предметы
- Отображать информацию о текущем состоянии инвентаря
- Обеспечивать возможность комбинирования или улучшения предметов
- Логика должна быть покрыта unit-тестами
- CLI не нужен
- Соблюдение всех принципов SOLID

## Описание решения

### Архитектура приложения

#### 1. Базовые интерфейсы (Interface Segregation)

**Разделение интерфейсов по ответственности:**

- `INamable` - только имя объекта
- `IGear` - базовые свойства предмета (Id, Weight, GearType, Rarity, Description)
- `IEquippable` - только для экипируемых предметов (Wear, Remove, EquipSlot)
- `IEnhanceable` - только для улучшаемых предметов (Enhance, CanImproveRarity)
- `IConsumable` - только для расходуемых предметов

#### 2. Иерархия предметов

**Базовый класс `GearBase`:**
- Абстрактный класс, реализующий `IGear`
- Общие свойства: `Id`, `Name`, `Weight`, `GearType`, `Rarity`, `Description`
- Виртуальный метод `GetDetails()` для отображения информации

**Типы предметов:**

1. **WeaponGear (Оружие)**
   - Свойства: `AttackPower`, `EquipSlot`, `Tier`, `MaxTier`
   - Реализует: `IEquippable`, `IEnhanceable`
   - Метод `Activate()` - нанесение урона цели

2. **ArmorGear (Броня)**
   - Свойства: `DefenseRating`, `EquipSlot`, `Tier`, `MaxTier`
   - Реализует: `IEquippable`, `IEnhanceable`
   - Метод `Activate()` - пассивная защита

3. **ConsumablePotion (Зелья)**
   - Свойства: `Effect`, `StackCount`
   - Реализует: `IConsumable`
   - Метод `Activate()` - применение эффекта
   - Поддержка комбинирования зелий

4. **QuestItem (Квестовые предметы)**
   - Минимальная реализация без дополнительных интерфейсов
   - Только базовые свойства предмета

#### 3. Система улучшения (Strategy Pattern)

**Интерфейс `IEnhancementStrategy`:**
- Метод `CanImprove(int tier, RarityLevel rarity)` - проверка возможности улучшения
- Метод `TryGetNextRarity(int tier, RarityLevel rarity)` - получение следующей редкости

**Реализации стратегий:**

1. **StandardEnhancementStrategy**
   - Стандартная логика улучшения
   - Порог улучшения на 10 уровне

2. **CustomEnhancementStrategy**
   - Настраиваемая стратегия с порогом 5 или 10
   - Использует делегат `Func<RarityLevel, int>` для расчета порога

**Фабрика стратегий `EnhancementStrategy`:**
- Статический метод `Get(EnhancementMode)` для получения стратегии
- Режимы: `Standard`, `Tier5`, `Tier10`

#### 4. Фабрики предметов (Factory Pattern)

**Абстрактная фабрика `WeaponGearFactory`:**
- Хранит параметры для создания оружия
- Абстрактный метод `CreateWeapon()`

**Конкретные фабрики:**
- `PrimaryWeaponFactory` - создание оружия для основной руки
- `SecondaryWeaponFactory` - создание оружия для вторичной руки
- `ArmorFactory` - создание брони для разных слотов
- `PotionFactory` - создание зелий с эффектами
- `PotionEffectFactory` - фабрика эффектов зелий

#### 5. Инвентарь (Backpack)

**Класс `Backpack`:**
- Коллекция предметов: `List<IGear> _items`
- Словарь экипировки: `Dictionary<EquipmentSlot, IEquippable> _gearSlots`
- Свойства: `Capacity`, `CurrentWeight`

**Методы:**
- `AddItem(IGear)` - добавление предмета с проверкой веса
- `RemoveItem(IGear)` - удаление предмета
- `EquipGear(IEquippable)` - экипировка предмета
- `UnequipGear(IEquippable)` - снятие экипировки
- `UseItem(IGear)` - использование предмета
- `EnhanceItem(IEnhanceable)` - улучшение предмета
- `CombinePotions()` - комбинирование зелий

## Соблюдение принципов SOLID

### S - Single Responsibility Principle

Каждый класс имеет одну ответственность:

- **Backpack** - только управление коллекцией предметов (добавление, удаление, экипировка, использование)
- **GearBase и наследники** - только за свои данные и логику конкретного типа предмета
- **PotionEffect** - только за реализацию эффектов зелий
- **Фабрики** - только за создание объектов
- **IEnhancementStrategy** - только за логику апгрейда

```csharp
public class Backpack
{
    public void AddItem(IGear item) { ... }
    public void RemoveItem(IGear item) { ... }
    public void EquipGear(IEquippable item) { ... }
}
```

### O - Open/Closed Principle

Реализован через интерфейсы и стратегии:

- **Интерфейсы** (`IGear`, `IEquippable`, `IEnhanceable`) позволяют добавлять новые типы предметов без изменения существующего кода
- **Стратегия улучшения** - можно добавлять новые алгоритмы апгрейда без изменения классов предметов

```csharp
public class CustomEnhancementStrategy : IEnhancementStrategy
{
    public bool CanImprove(int tier, RarityLevel rarity) { ... }
    public RarityLevel TryGetNextRarity(int tier, RarityLevel rarity) { ... }
}
```

### L - Liskov Substitution Principle

- Все наследники `GearBase` (WeaponGear, ArmorGear, ConsumablePotion, QuestItem) могут использоваться везде, где ожидается `IGear`
- Классы корректно реализуют интерфейсы без нарушения контрактов

### I - Interface Segregation Principle

Разделение интерфейсов по назначению:

- `IGear` - базовые свойства предмета
- `IEquippable` - только для экипируемых предметов
- `IEnhanceable` - только для улучшаемых предметов
- `IConsumable` - только для расходуемых предметов

```csharp
// Weapon реализует только нужные интерфейсы
public class WeaponGear : GearBase, IEquippable, IEnhanceable
{
    // Реализация только необходимых методов
}
```

### D - Dependency Inversion Principle

Зависимость от абстракций, а не от конкретных реализаций:

**Стратегия улучшения в WeaponGear:**
```csharp
public class WeaponGear : GearBase, IEquippable, IEnhanceable
{
    private readonly IEnhancementStrategy _enhancementStrategy;

    public WeaponGear(..., IEnhancementStrategy upgradeStrategy)
    {
        _enhancementStrategy = upgradeStrategy;
    }
}
```

**Фабрики эффектов в Potion:**
```csharp
public class ConsumablePotion
{
    public ConsumablePotion(..., PotionEffectFactory factory)
    {
        Effect = factory.CreatePotionEffect();
    }
}
```

**Инвентарь работает с интерфейсами:**
```csharp
public class Backpack
{
    private List<IGear> _items;
    private Dictionary<EquipmentSlot, IEquippable> _gearSlots;
}
```

## Покрытие тестами

- `ItemTests.cs` - тесты базового класса предметов
- `WeaponTests.cs` - тесты оружия
- `ArmorTests.cs` - тесты брони
- `PotionTests.cs` - тесты зелий
- `PotionEffectTests.cs` - тесты эффектов
- `QuestTests.cs` - тесты квестовых предметов
- `InventoryTests.cs` - тесты инвентаря
