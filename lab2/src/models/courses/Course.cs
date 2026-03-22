namespace EduManagement;

public abstract class TrainingModule
{
    private string _id;
    private string _name;
    private string _description;
    private Instructor? _assignedInstructor;
    private List<Learner> _enrolledLearners;

    public string Id
    {
        get => _id;
        set => _id = value;
    }

    public string Name
    {
        get => _name;
        set => _name = value;
    }

    public string Description
    {
        get => _description;
        set => _description = value;
    }

    public Instructor? AssignedInstructor
    {
        get => _assignedInstructor;
        set => _assignedInstructor = value;
    }

    public List<Learner> EnrolledLearners => _enrolledLearners;

    public TrainingModule()
    {
        _id = string.Empty;
        _name = string.Empty;
        _description = string.Empty;
        _enrolledLearners = new List<Learner>();
    }

    public TrainingModule(string id, string name, string description)
    {
        _id = id;
        _name = name;
        _description = description;
        _enrolledLearners = new List<Learner>();
    }

    public abstract string GetTypeLabel();

    public void SetInstructor(Instructor instructor) => _assignedInstructor = instructor;
    public bool HasInstructor() => _assignedInstructor != null;

    public void ClearInstructor()
    {
        if (HasInstructor())
            _assignedInstructor = null;
    }

    public virtual void AddLearner(Learner learner)
    {
        if (!_enrolledLearners.Contains(learner))
            _enrolledLearners.Add(learner);
    }

    public void RemoveLearner(Learner learner)
    {
        _enrolledLearners.Remove(learner);
    }
}
