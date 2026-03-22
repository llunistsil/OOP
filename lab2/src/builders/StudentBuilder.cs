namespace EduManagement.Builders;

public class LearnerBuilder : BaseBuilder<LearnerBuilder, Learner>
{
    protected override LearnerBuilder Execute(Action operation)
    {
        operation();
        return this;
    }

    public LearnerBuilder WithId(string id)
    {
        return Execute(() => _instance.Id = id);
    }

    public LearnerBuilder WithName(string firstName, string lastName)
    {
        return Execute(() =>
        {
            _instance.FirstName = firstName;
            _instance.LastName = lastName;
        });
    }

    public LearnerBuilder WithEmail(string email)
    {
        return Execute(() => _instance.Email = email);
    }

    public LearnerBuilder WithEnrollmentYear(string year)
    {
        return Execute(() => _instance.EnrollmentYear = year);
    }

    public override Learner Create()
    {
        return _instance;
    }
}
