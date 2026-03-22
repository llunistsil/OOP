namespace Lab2.Tests.Data;

public class SampleDataFactory
{
    public string ModuleId => "MOD-789";
    public string ModuleName => "Data Structures";
    public string ModuleDescription => "Core algorithms and data structures course";

    public string ClassroomNumber => "Auditorium 210";
    public int MaxSeats => 30;

    public string LearningPlatform => "edX";
    public string VideoConferenceUrl => "https://zoom.edu/j/123456";
    public bool SelfDirected => false;

    public string PersonId => "P-555";
    public string FirstNm => "Emma";
    public string LastNm => "Wilson";
    public string EmailAddr => "emma.wilson@university.edu";

    public string InstructorDept => "Software Engineering";

    public string StudentCohort => "2024";

    public ClassroomCourse BuildClassroomModule()
        => new ClassroomCourseBuilder()
            .WithId(ModuleId)
            .WithName(ModuleName)
            .WithDescription(ModuleDescription)
            .WithRoom(ClassroomNumber)
            .WithCapacity(MaxSeats)
            .Create();

    public RemoteCourse BuildRemoteModule()
        => new RemoteCourseBuilder()
            .WithId(ModuleId)
            .WithName(ModuleName)
            .WithDescription(ModuleDescription)
            .WithPlatformUrl(LearningPlatform)
            .WithSelfPaced(SelfDirected)
            .Create();

    public Instructor BuildInstructor()
        => new InstructorBuilder()
            .WithId(PersonId)
            .WithName(FirstNm, LastNm)
            .WithEmail(EmailAddr)
            .WithDepartment(InstructorDept)
            .Create();

    public Learner BuildLearner()
        => new LearnerBuilder()
            .WithId(PersonId)
            .WithName(FirstNm, LastNm)
            .WithEmail(EmailAddr)
            .WithEnrollmentYear(StudentCohort)
            .Create();
}
