namespace EduManagement;

public class RemoteCourse : TrainingModule
{
    private string _platformUrl;
    private bool _isSelfPaced;

    public string PlatformUrl
    {
        get => _platformUrl;
        set => _platformUrl = value;
    }

    public bool IsSelfPaced
    {
        get => _isSelfPaced;
        set => _isSelfPaced = value;
    }

    public RemoteCourse() : base()
    {
        _platformUrl = string.Empty;
        _isSelfPaced = false;
    }

    public RemoteCourse(string id, string name, string description, string platformUrl, bool isSelfPaced)
        : base(id, name, description)
    {
        _platformUrl = platformUrl;
        _isSelfPaced = isSelfPaced;
    }

    public override string GetTypeLabel() => "Online";
}
