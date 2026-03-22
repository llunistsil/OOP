namespace EduManagement;

public class TrainingService : ITrainingService
{
    private readonly Dictionary<string, TrainingModule> _modules;

    public TrainingService()
    {
        _modules = new Dictionary<string, TrainingModule>();
    }

    public void AddModule(TrainingModule module)
    {
        if (_modules.ContainsKey(module.Id))
            throw new ArgumentException($"Module with ID {module.Id} already exists");
        _modules[module.Id] = module;
    }

    public bool RemoveModule(string id) => _modules.Remove(id);

    public TrainingModule GetModule(string id)
    {
        if (!_modules.ContainsKey(id))
            throw new ArgumentException($"Module with ID {id} does not exist");
        return _modules[id];
    }

    public List<TrainingModule> GetAllModules() => _modules.Values.ToList();

    public List<TrainingModule> GetModulesByInstructor(string instructorId)
        => _modules.Values
            .Where(m => m.AssignedInstructor != null && m.AssignedInstructor.Id == instructorId)
            .ToList();

    public void AssignInstructorToModule(string moduleId, Instructor instructor)
    {
        if (!_modules.ContainsKey(moduleId))
            throw new ArgumentException($"Module with ID {moduleId} not found");
        if (_modules[moduleId].HasInstructor())
            throw new ArgumentException($"Module with ID {moduleId} already has an instructor");
        _modules[moduleId].SetInstructor(instructor);
    }

    public void RemoveInstructorFromModule(string moduleId)
    {
        if (!_modules.ContainsKey(moduleId))
            throw new ArgumentException($"Module with ID {moduleId} not found");
        if (!_modules[moduleId].HasInstructor())
            throw new ArgumentException($"Module with ID {moduleId} does not have an instructor");
        _modules[moduleId].ClearInstructor();
    }

    public List<Learner> GetLearnersInModule(string id)
    {
        if (!_modules.ContainsKey(id))
            return new List<Learner>();
        return _modules[id].EnrolledLearners;
    }
}
