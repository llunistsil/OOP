namespace EduManagement.Builders;

public class InstructorBuilder : BaseBuilder<InstructorBuilder, Instructor>
{
    protected override InstructorBuilder Execute(Action operation)
    {
        operation();
        return this;
    }

    public InstructorBuilder WithId(string id)
    {
        return Execute(() => _instance.Id = id);
    }

    public InstructorBuilder WithName(string firstName, string lastName)
    {
        return Execute(() =>
        {
            _instance.FirstName = firstName;
            _instance.LastName = lastName;
        });
    }

    public InstructorBuilder WithEmail(string email)
    {
        return Execute(() => _instance.Email = email);
    }

    public InstructorBuilder WithDepartment(string department)
    {
        return Execute(() => _instance.Department = department);
    }

    public override Instructor Create()
    {
        return _instance;
    }
}
