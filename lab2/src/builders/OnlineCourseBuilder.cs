namespace EduManagement.Builders;

public interface IRemoteCourseBuilder
{
    RemoteCourseBuilder WithId(string id);
    RemoteCourseBuilder WithName(string name);
    RemoteCourseBuilder WithDescription(string description);
    RemoteCourseBuilder WithPlatformUrl(string url);
    RemoteCourseBuilder WithSelfPaced(bool isSelfPaced);
}

public class RemoteCourseBuilder : BaseBuilder<RemoteCourseBuilder, RemoteCourse>, IRemoteCourseBuilder
{
    protected override RemoteCourseBuilder Execute(Action operation)
    {
        operation();
        return this;
    }

    public RemoteCourseBuilder WithId(string id)
    {
        return Execute(() => _instance.Id = id);
    }

    public RemoteCourseBuilder WithName(string name)
    {
        return Execute(() => _instance.Name = name);
    }

    public RemoteCourseBuilder WithDescription(string description)
    {
        return Execute(() => _instance.Description = description);
    }

    public RemoteCourseBuilder WithPlatformUrl(string url)
    {
        return Execute(() => _instance.PlatformUrl = url);
    }

    public RemoteCourseBuilder WithSelfPaced(bool isSelfPaced)
    {
        return Execute(() => _instance.IsSelfPaced = isSelfPaced);
    }

    public override RemoteCourse Create()
    {
        return _instance;
    }
}
