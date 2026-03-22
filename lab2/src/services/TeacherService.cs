namespace EduManagement;

public class InstructorService : IInstructorService
{
    private readonly Dictionary<string, Instructor> _instructors;

    public InstructorService()
    {
        _instructors = new Dictionary<string, Instructor>();
    }

    public void AddInstructor(Instructor instructor)
    {
        if (_instructors.ContainsKey(instructor.Id))
            throw new ArgumentException($"Instructor with ID {instructor.Id} already exists");
        _instructors[instructor.Id] = instructor;
    }

    public Instructor GetInstructor(string id)
    {
        if (!_instructors.ContainsKey(id))
            throw new ArgumentException($"Instructor with ID {id} does not exist");
        return _instructors[id];
    }

    public bool RemoveInstructor(string id) => _instructors.Remove(id);

    public List<Instructor> GetAllInstructors() => _instructors.Values.ToList();
}
