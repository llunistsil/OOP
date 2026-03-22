namespace Lab2.Tests.Services;

public class TrainingServiceTests
{
    private readonly SampleDataFactory _sampleData = new SampleDataFactory();

    [Fact(DisplayName = "Add valid module")]
    public void AddModule_WithValidInput_AddsSuccessfully()
    {
        var service = new TrainingService();
        var module = _sampleData.BuildClassroomModule();

        service.AddModule(module);

        Assert.NotEmpty(service.GetAllModules());
    }

    [Fact(DisplayName = "Add duplicate module throws exception")]
    public void AddModule_WithDuplicateId_ThrowsException()
    {
        var service = new TrainingService();
        var module = _sampleData.BuildClassroomModule();
        service.AddModule(module);

        var action = () => service.AddModule(module);

        var exception = Assert.Throws<ArgumentException>(action);
        Assert.Contains("already exists", exception.Message);
    }

    [Fact(DisplayName = "Remove existing module")]
    public void RemoveModule_WithExistingId_RemovesSuccessfully()
    {
        var service = new TrainingService();
        var module = _sampleData.BuildClassroomModule();
        service.AddModule(module);

        var result = service.RemoveModule(module.Id);

        Assert.True(result);
        Assert.Empty(service.GetAllModules());
    }

    [Fact(DisplayName = "Remove non-existing module returns false")]
    public void RemoveModule_WithNonExistingId_ReturnsFalse()
    {
        var service = new TrainingService();

        var result = service.RemoveModule("NON-EXISTENT-ID");

        Assert.False(result);
    }

    [Fact(DisplayName = "Get existing module")]
    public void GetModule_WithExistingId_ReturnsModule()
    {
        var service = new TrainingService();
        var module = _sampleData.BuildClassroomModule();
        service.AddModule(module);

        var result = service.GetModule(module.Id);

        Assert.NotNull(result);
        Assert.Equal(module.Id, result.Id);
    }

    [Fact(DisplayName = "Get non-existing module throws exception")]
    public void GetModule_WithNonExistingId_ThrowsException()
    {
        var service = new TrainingService();

        var action = () => service.GetModule("NON-EXISTENT-ID");

        var exception = Assert.Throws<ArgumentException>(action);
        Assert.Contains("does not exist", exception.Message);
    }

    [Fact(DisplayName = "Get all modules when empty")]
    public void GetAllModules_WhenEmpty_ReturnsEmptyList()
    {
        var service = new TrainingService();

        var result = service.GetAllModules();

        Assert.Empty(result);
    }

    [Fact(DisplayName = "Get all modules with data")]
    public void GetAllModules_WithData_ReturnsAllModules()
    {
        var service = new TrainingService();
        var module = _sampleData.BuildClassroomModule();
        service.AddModule(module);

        var result = service.GetAllModules();

        Assert.NotEmpty(result);
        Assert.Single(result);
        Assert.Contains(module, result);
    }

    [Fact(DisplayName = "Assign instructor to module")]
    public void AssignInstructorToModule_WithValidData_AssignsSuccessfully()
    {
        var service = new TrainingService();
        var module = _sampleData.BuildClassroomModule();
        var instructor = _sampleData.BuildInstructor();
        service.AddModule(module);

        service.AssignInstructorToModule(module.Id, instructor);

        Assert.NotNull(service.GetModule(module.Id).AssignedInstructor);
        Assert.Equal(instructor, service.GetModule(module.Id).AssignedInstructor);
    }

    [Fact(DisplayName = "Assign instructor to non-existing module throws exception")]
    public void AssignInstructorToModule_WithNonExistingModule_ThrowsException()
    {
        var service = new TrainingService();
        var instructor = _sampleData.BuildInstructor();

        var action = () => service.AssignInstructorToModule("NON-EXISTENT", instructor);

        var exception = Assert.Throws<ArgumentException>(action);
        Assert.Contains("not found", exception.Message);
    }

    [Fact(DisplayName = "Assign instructor to module with existing instructor throws exception")]
    public void AssignInstructorToModule_WithExistingInstructor_ThrowsException()
    {
        var service = new TrainingService();
        var module = _sampleData.BuildClassroomModule();
        var instructor = _sampleData.BuildInstructor();
        service.AddModule(module);
        service.AssignInstructorToModule(module.Id, instructor);

        var action = () => service.AssignInstructorToModule(module.Id, instructor);

        var exception = Assert.Throws<ArgumentException>(action);
        Assert.Contains("already has an instructor", exception.Message);
    }

    [Fact(DisplayName = "Remove instructor from module")]
    public void RemoveInstructorFromModule_WithValidData_RemovesSuccessfully()
    {
        var service = new TrainingService();
        var module = _sampleData.BuildClassroomModule();
        var instructor = _sampleData.BuildInstructor();
        service.AddModule(module);
        service.AssignInstructorToModule(module.Id, instructor);

        service.RemoveInstructorFromModule(module.Id);

        Assert.Null(service.GetModule(module.Id).AssignedInstructor);
    }

    [Fact(DisplayName = "Remove instructor from non-existing module throws exception")]
    public void RemoveInstructorFromModule_WithNonExistingModule_ThrowsException()
    {
        var service = new TrainingService();

        var action = () => service.RemoveInstructorFromModule("NON-EXISTENT");

        var exception = Assert.Throws<ArgumentException>(action);
        Assert.Contains("not found", exception.Message);
    }

    [Fact(DisplayName = "Remove instructor from module without instructor throws exception")]
    public void RemoveInstructorFromModule_WithoutInstructor_ThrowsException()
    {
        var service = new TrainingService();
        var module = _sampleData.BuildClassroomModule();
        service.AddModule(module);

        var action = () => service.RemoveInstructorFromModule(module.Id);

        var exception = Assert.Throws<ArgumentException>(action);
        Assert.Contains("does not have an instructor", exception.Message);
    }

    [Fact(DisplayName = "Get modules by instructor")]
    public void GetModulesByInstructor_WithValidData_ReturnsModules()
    {
        var service = new TrainingService();
        var module = _sampleData.BuildClassroomModule();
        var instructor = _sampleData.BuildInstructor();
        service.AddModule(module);
        service.AssignInstructorToModule(module.Id, instructor);

        var result = service.GetModulesByInstructor(instructor.Id);

        Assert.NotEmpty(result);
        Assert.Contains(module, result);
    }

    [Fact(DisplayName = "Get modules by non-existing instructor returns empty")]
    public void GetModulesByInstructor_WithNonExistingInstructor_ReturnsEmpty()
    {
        var service = new TrainingService();

        var result = service.GetModulesByInstructor("NON-EXISTENT");

        Assert.Empty(result);
    }

    [Fact(DisplayName = "Get learners in module")]
    public void GetLearnersInModule_WithEnrolledLearners_ReturnsLearners()
    {
        var service = new TrainingService();
        var module = _sampleData.BuildClassroomModule();
        var learner = _sampleData.BuildLearner();
        module.AddLearner(learner);
        service.AddModule(module);

        var result = service.GetLearnersInModule(module.Id);

        Assert.NotEmpty(result);
        Assert.Contains(learner, result);
    }

    [Fact(DisplayName = "Get learners in non-existing module returns empty")]
    public void GetLearnersInModule_WithNonExistingModule_ReturnsEmpty()
    {
        var service = new TrainingService();

        var result = service.GetLearnersInModule("NON-EXISTENT");

        Assert.Empty(result);
    }
}
