namespace EduManagement;

public abstract class Person
{
    private string _id;
    private string _firstName;
    private string _lastName;
    private string _email;

    public string Id
    {
        get => _id;
        set => _id = value;
    }

    public string FirstName
    {
        get => _firstName;
        set => _firstName = value;
    }

    public string LastName
    {
        get => _lastName;
        set => _lastName = value;
    }

    public string Email
    {
        get => _email;
        set => _email = value;
    }

    protected Person()
    {
        _id = string.Empty;
        _firstName = string.Empty;
        _lastName = string.Empty;
        _email = string.Empty;
    }

    protected Person(string id, string firstName, string lastName, string email)
    {
        _id = id;
        _firstName = firstName;
        _lastName = lastName;
        _email = email;
    }

    public override string ToString() => $"{_firstName} {_lastName}";
}
