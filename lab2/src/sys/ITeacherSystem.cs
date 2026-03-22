namespace EduManagement.System;

public interface IInstructorManager
{
    string RegisterInstructor(string id, string firstName, string lastName, string email, string department);
    string RegisterInstructor(Instructor instructor);
    string DeleteInstructor(string id);
    List<Instructor> ListAllInstructors();
    Instructor GetInstructor(string id);
}
