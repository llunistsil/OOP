namespace EduManagement.System;

public interface ITrainingModuleManager
{
    string CreateModule(TrainingModule module);
    string CreateRemoteModule(string id, string name, string description, string platformUrl, bool isSelfPaced);
    string CreateClassroomModule(string id, string name, string description, string roomNumber, int capacity);
    string DeleteModule(string id);
    List<TrainingModule> ListAllModules();
    TrainingModule GetModule(string id);
    List<TrainingModule> GetModulesByInstructor(string instructorId);
    List<Learner> GetLearnersInModule(string id);
    int GetLearnerCountInModule(string id);
}
