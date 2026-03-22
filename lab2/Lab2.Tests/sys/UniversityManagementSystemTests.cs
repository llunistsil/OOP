namespace Lab2.Tests.System;

public class EduManagementSystemTests
{
    private readonly EduManagementSystem _system;
    private readonly SampleDataFactory _data;

    public EduManagementSystemTests()
    {
        _system = new EduManagementSystem();
        _data = new SampleDataFactory();
    }

    [Fact(DisplayName = "Register instructor successfully")]
    public void RegisterInstructor_WithValidData_ReturnsSuccess()
    {
        var result = _system.RegisterInstructor("I-100", "Nathan", "Oxford", "nathan.o@faculty.edu", "Engineering");

        Assert.Contains("registered successfully", result);
        Assert.Single(_system.ListAllInstructors());
    }

    [Fact(DisplayName = "Register duplicate instructor returns error")]
    public void RegisterInstructor_WithDuplicateId_ReturnsError()
    {
        var instructor = _data.BuildInstructor();
        _system.RegisterInstructor(instructor);

        var result = _system.RegisterInstructor(instructor);

        Assert.Contains("Error", result);
        Assert.Contains("already exists", result);
    }

    [Fact(DisplayName = "Delete existing instructor")]
    public void DeleteInstructor_WithExistingId_ReturnsSuccess()
    {
        var instructor = _data.BuildInstructor();
        _system.RegisterInstructor(instructor);

        var result = _system.DeleteInstructor(instructor.Id);

        Assert.Contains("removed", result);
        Assert.Empty(_system.ListAllInstructors());
    }

    [Fact(DisplayName = "Delete non-existing instructor returns not found")]
    public void DeleteInstructor_WithNonExistingId_ReturnsNotFound()
    {
        var result = _system.DeleteInstructor("NON-EXISTENT");

        Assert.Contains("not found", result);
    }

    [Fact(DisplayName = "Create classroom module successfully")]
    public void CreateClassroomModule_WithValidData_ReturnsSuccess()
    {
        var result = _system.CreateClassroomModule("MOD-100", "Algorithms", "Sorting and searching", "Hall 201", 40);

        Assert.Contains("created successfully", result);
        Assert.Single(_system.ListAllModules());
    }

    [Fact(DisplayName = "Create remote module successfully")]
    public void CreateRemoteModule_WithValidData_ReturnsSuccess()
    {
        var result = _system.CreateRemoteModule("MOD-200", "Blockchain", "Cryptocurrency basics", "https://blockchain.edu", false);

        Assert.Contains("created successfully", result);
        Assert.Single(_system.ListAllModules());
    }

    [Fact(DisplayName = "Delete existing module")]
    public void DeleteModule_WithExistingId_ReturnsSuccess()
    {
        _system.CreateClassroomModule("MOD-300", "Mobile Dev", "iOS and Android", "Lab 105", 15);

        var result = _system.DeleteModule("MOD-300");

        Assert.Contains("removed", result);
        Assert.Empty(_system.ListAllModules());
    }

    [Fact(DisplayName = "Delete non-existing module returns not found")]
    public void DeleteModule_WithNonExistingId_ReturnsNotFound()
    {
        var result = _system.DeleteModule("NON-EXISTENT");

        Assert.Contains("not found", result);
    }

    [Fact(DisplayName = "Assign instructor to module")]
    public void AssignInstructorToModule_WithValidData_ReturnsSuccess()
    {
        var instructor = _data.BuildInstructor();
        _system.RegisterInstructor(instructor);
        _system.CreateClassroomModule("MOD-400", "Game Dev", "Unity and C#", "Lab 303", 20);

        var result = _system.AssignInstructorToModule("MOD-400", instructor.Id);

        Assert.Contains("assigned", result);
    }

    [Fact(DisplayName = "Assign instructor to non-existing module returns error")]
    public void AssignInstructorToModule_WithNonExistingModule_ReturnsError()
    {
        var instructor = _data.BuildInstructor();
        _system.RegisterInstructor(instructor);

        var result = _system.AssignInstructorToModule("NON-EXISTENT", instructor.Id);

        Assert.Contains("Error", result);
        Assert.Contains("not found", result);
    }

    [Fact(DisplayName = "Unassign instructor from module")]
    public void UnassignInstructorFromModule_WithValidData_ReturnsSuccess()
    {
        var instructor = _data.BuildInstructor();
        _system.RegisterInstructor(instructor);
        _system.CreateClassroomModule("MOD-500", "VR Development", "Virtual reality basics", "Lab 404", 12);
        _system.AssignInstructorToModule("MOD-500", instructor.Id);

        var result = _system.UnassignInstructorFromModule("MOD-500");

        Assert.Contains("removed", result);
    }

    [Fact(DisplayName = "Enroll learner in module")]
    public void EnrollLearnerInModule_WithValidData_ReturnsSuccess()
    {
        _system.CreateClassroomModule("MOD-600", "UI/UX Design", "User experience principles", "Studio 101", 18);
        var learner = _data.BuildLearner();

        var result = _system.EnrollLearnerInModule("MOD-600", learner);

        Assert.Contains("enrolled", result);
    }

    [Fact(DisplayName = "Enroll learner in non-existing module returns error")]
    public void EnrollLearnerInModule_WithNonExistingModule_ReturnsError()
    {
        var learner = _data.BuildLearner();

        var result = _system.EnrollLearnerInModule("NON-EXISTENT", learner);

        Assert.Contains("Error", result);
        Assert.Contains("does not exist", result);
    }

    [Fact(DisplayName = "Get modules by instructor")]
    public void GetModulesByInstructor_WithAssignedInstructor_ReturnsModules()
    {
        var instructor = _data.BuildInstructor();
        _system.RegisterInstructor(instructor);
        _system.CreateClassroomModule("MOD-700", "Compiler Design", "Parsing and optimization", "Lab 505", 25);
        _system.AssignInstructorToModule("MOD-700", instructor.Id);

        var modules = _system.GetModulesByInstructor(instructor.Id);

        Assert.NotEmpty(modules);
        Assert.Single(modules);
    }

    [Fact(DisplayName = "Get learners in module")]
    public void GetLearnersInModule_WithEnrolledLearners_ReturnsLearners()
    {
        _system.CreateClassroomModule("MOD-800", "Network Security", "Firewall configuration", "Lab 202", 16);
        var learner = _data.BuildLearner();
        _system.EnrollLearnerInModule("MOD-800", learner);

        var learners = _system.GetLearnersInModule("MOD-800");

        Assert.NotEmpty(learners);
        Assert.Contains(learner, learners);
    }

    [Fact(DisplayName = "Get learner count in module")]
    public void GetLearnerCountInModule_WithMultipleLearners_ReturnsCorrectCount()
    {
        _system.CreateClassroomModule("MOD-900", "Database Design", "Normalization and indexing", "Lab 303", 20);

        var learner1 = new Learner("L-001", "Oscar", "Page", "oscar.p@stu.edu", "2024");
        var learner2 = new Learner("L-002", "Paula", "Quinn", "paula.q@stu.edu", "2024");
        var learner3 = new Learner("L-003", "Quincy", "Ross", "quincy.r@stu.edu", "2024");

        _system.EnrollLearnerInModule("MOD-900", learner1);
        _system.EnrollLearnerInModule("MOD-900", learner2);
        _system.EnrollLearnerInModule("MOD-900", learner3);

        var count = _system.GetLearnerCountInModule("MOD-900");

        Assert.Equal(3, count);
    }
}
