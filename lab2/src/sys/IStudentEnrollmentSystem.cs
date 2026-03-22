namespace EduManagement.System;

public interface ILearnerEnrollment
{
    string EnrollLearnerInModule(string moduleId, Learner learner);
    string EnrollLearnerInModule(string moduleId, string learnerId, string firstName, string lastName, string email, string enrollmentYear);
}
