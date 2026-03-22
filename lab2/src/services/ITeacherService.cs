namespace EduManagement;

public interface IInstructorService
{
    void AddInstructor(Instructor instructor);
    Instructor GetInstructor(string id);
    bool RemoveInstructor(string id);
    List<Instructor> GetAllInstructors();
}
