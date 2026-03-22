namespace EduManagement.System;

public interface IEduManagementSystem : IInstructorManager, ITrainingModuleManager, IInstructorAssignment, ILearnerEnrollment
{
}

public class EduManagementSystem : IEduManagementSystem
{
    private readonly TrainingService _moduleService;
    private readonly InstructorService _instructorService;

    public EduManagementSystem()
    {
        _moduleService = new TrainingService();
        _instructorService = new InstructorService();
    }

    public List<Instructor> ListAllInstructors() => _instructorService.GetAllInstructors();
    public List<TrainingModule> ListAllModules() => _moduleService.GetAllModules();
    public TrainingModule GetModule(string id) => _moduleService.GetModule(id);
    public List<TrainingModule> GetModulesByInstructor(string instructorId) => _moduleService.GetModulesByInstructor(instructorId);
    public List<Learner> GetLearnersInModule(string id) => _moduleService.GetLearnersInModule(id);
    public int GetLearnerCountInModule(string id) => _moduleService.GetLearnersInModule(id).Count;
    public Instructor GetInstructor(string id) => _instructorService.GetInstructor(id);

    public string DeleteInstructor(string id)
    {
        var result = _instructorService.RemoveInstructor(id);
        return result ? $"Instructor {id} removed!" : $"Instructor {id} not found!";
    }

    public string DeleteModule(string id)
    {
        var result = _moduleService.RemoveModule(id);
        return result ? $"Module {id} removed!" : $"Module {id} not found!";
    }

    public string RegisterInstructor(Instructor instructor)
        => HandleOperation(() =>
        {
            _instructorService.AddInstructor(instructor);
            return $"Instructor {instructor.FirstName} {instructor.LastName} registered successfully!";
        });

    public string RegisterInstructor(string id, string firstName, string lastName, string email, string department)
        => RegisterInstructor(new Instructor(id, firstName, lastName, email, department));

    public string CreateModule(TrainingModule module)
        => HandleOperation(() =>
        {
            _moduleService.AddModule(module);
            return $"{module.GetTypeLabel()} module '{module.Name}' created successfully!";
        });

    public string CreateRemoteModule(string id, string name, string description, string platformUrl, bool isSelfPaced)
        => CreateModule(new RemoteCourse(id, name, description, platformUrl, isSelfPaced));

    public string CreateClassroomModule(string id, string name, string description, string roomNumber, int capacity)
        => CreateModule(new ClassroomCourse(id, name, description, roomNumber, capacity));

    public string AssignInstructorToModule(string moduleId, string instructorId)
        => HandleOperation(() =>
        {
            var instructor = _instructorService.GetInstructor(instructorId);
            if (instructor == null)
                return $"Instructor {instructorId} not found!";

            _moduleService.AssignInstructorToModule(moduleId, instructor);
            return $"Instructor {instructor.FirstName} {instructor.LastName} assigned to module {moduleId}!";
        });

    public string AssignInstructorToModule(string moduleId, Instructor instructor)
        => AssignInstructorToModule(moduleId, instructor.Id);

    public string UnassignInstructorFromModule(string moduleId)
        => HandleOperation(() =>
        {
            _moduleService.RemoveInstructorFromModule(moduleId);
            return $"Instructor removed from module {moduleId}!";
        });

    public string EnrollLearnerInModule(string moduleId, Learner learner)
        => HandleOperation(() =>
        {
            var module = _moduleService.GetModule(moduleId);
            if (module == null)
                return $"Module {moduleId} not found!";

            module.AddLearner(learner);
            return $"Learner {learner.FirstName} {learner.LastName} enrolled in module {module.Name}!";
        });

    public string EnrollLearnerInModule(string moduleId, string learnerId, string firstName, string lastName, string email, string enrollmentYear)
        => EnrollLearnerInModule(moduleId, new Learner(learnerId, firstName, lastName, email, enrollmentYear));

    private string HandleOperation(Func<string> operation)
    {
        try
        {
            return operation();
        }
        catch (ArgumentException ex)
        {
            return $"Error: {ex.Message}";
        }
        catch (InvalidOperationException ex)
        {
            return $"Error: {ex.Message}";
        }
    }
}
