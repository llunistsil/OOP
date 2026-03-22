namespace EduManagement.System;

public interface IInstructorAssignment
{
    string AssignInstructorToModule(string moduleId, string instructorId);
    string AssignInstructorToModule(string moduleId, Instructor instructor);
    string UnassignInstructorFromModule(string moduleId);
}
