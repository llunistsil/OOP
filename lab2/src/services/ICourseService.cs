namespace EduManagement;

public interface ITrainingService
{
    void AddModule(TrainingModule module);
    bool RemoveModule(string id);
    TrainingModule GetModule(string id);
    List<TrainingModule> GetAllModules();
    List<TrainingModule> GetModulesByInstructor(string instructorId);
    List<Learner> GetLearnersInModule(string id);
    void AssignInstructorToModule(string moduleId, Instructor instructor);
    void RemoveInstructorFromModule(string moduleId);
}
