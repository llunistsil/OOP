namespace EduManagement;

public class Instructor : Person
{
    private string _department;

    public string Department
    {
        get => _department;
        set => _department = value;
    }

    public Instructor() : base()
    {
        _department = string.Empty;
    }

    public Instructor(string id, string firstName, string lastName, string email, string department)
        : base(id, firstName, lastName, email)
    {
        _department = department;
    }

    public Instructor(Instructor source)
        : this(source.Id, source.FirstName, source.LastName, source.Email, source._department)
    {
    }

    public override string ToString() => $"{FirstName} {LastName} ({_department})";
}
