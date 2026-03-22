namespace EduManagement;

public interface ITrainingModule
{
    string Id { get; set; }
    string Name { get; set; }
    string Description { get; set; }
    Instructor? AssignedInstructor { get; set; }
    List<Learner> EnrolledLearners { get; }
    string GetTypeLabel();
    void SetInstructor(Instructor instructor);
    bool HasInstructor();
    void ClearInstructor();
    void AddLearner(Learner learner);
    void RemoveLearner(Learner learner);
}
