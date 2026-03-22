namespace EduManagement.Builders;

public interface ICreator<TObject>
{
    TObject Create();
}

public abstract class BaseBuilder<TBuilder, TObject> : ICreator<TObject>
    where TObject : new()
{
    protected TObject _instance = new TObject();

    protected abstract TBuilder Execute(Action operation);

    public virtual TObject Create() => _instance;
}
