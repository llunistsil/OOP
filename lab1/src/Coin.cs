namespace VendingMachineLib;

public interface IMonetaryValue
{
    int GetValue();
}

public abstract class Coin : IComparable<Coin>, IMonetaryValue
{
    private readonly int _nominal;

    protected Coin(int nominal) => _nominal = nominal;
    public int GetValue() => _nominal;
    public int CompareTo(Coin? other)
    {
        if (other == null) return 1;
        return -_nominal.CompareTo(other.GetValue());
    }
}

public class CopperCoin : Coin
{
    public CopperCoin() : base(1) { }

    public override string ToString() => "copper coin";
}

public class NickelCoin : Coin
{
    public NickelCoin() : base(10) { }

    public override string ToString() => "nickel coin";
}

public class PlatinumCoin : Coin
{
    public PlatinumCoin() : base(100) { }

    public override string ToString() => "platinum coin";
}

public class DiamondCoin : Coin
{
    public DiamondCoin() : base(1000) { }

    public override string ToString() => "diamond coin";
}

public class CoinCollection
{
    private readonly List<Coin> _coins;

    public CoinCollection()
    {
        _coins =
        [
            new CopperCoin(),
            new NickelCoin(),
            new PlatinumCoin(),
            new DiamondCoin()
        ];
        _coins.Sort();
    }

    public List<Coin> GetAll() => _coins;
}
