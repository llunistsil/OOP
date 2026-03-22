namespace EduManagement;

public class Learner : Person
{
    private string _enrollmentYear;

    public string EnrollmentYear
    {
        get => _enrollmentYear;
        set => _enrollmentYear = value;
    }

    public Learner() : base()
    {
        _enrollmentYear = string.Empty;
    }

    public Learner(string id, string firstName, string lastName, string email, string enrollmentYear)
        : base(id, firstName, lastName, email)
    {
        _enrollmentYear = enrollmentYear;
    }

    public Learner(Learner source)
        : this(source.Id, source.FirstName, source.LastName, source.Email, source._enrollmentYear)
    {
    }

    public override string ToString() => $"{FirstName} {LastName} (Enrolled: {_enrollmentYear})";
}
