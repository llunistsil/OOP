namespace Lab2.Tests.Services;

public class InstructorServiceTests
{
    private readonly SampleDataFactory _sampleData = new SampleDataFactory();

    [Fact(DisplayName = "Add valid instructor")]
    public void AddInstructor_WithValidInput_AddsSuccessfully()
    {
        var service = new InstructorService();
        var instructor = _sampleData.BuildInstructor();

        service.AddInstructor(instructor);

        Assert.NotEmpty(service.GetAllInstructors());
    }

    [Fact(DisplayName = "Add duplicate instructor throws exception")]
    public void AddInstructor_WithDuplicateId_ThrowsException()
    {
        var service = new InstructorService();
        var instructor = _sampleData.BuildInstructor();
        service.AddInstructor(instructor);

        var action = () => service.AddInstructor(instructor);

        var exception = Assert.Throws<ArgumentException>(action);
        Assert.Contains("already exists", exception.Message);
    }

    [Fact(DisplayName = "Get existing instructor")]
    public void GetInstructor_WithExistingId_ReturnsInstructor()
    {
        var service = new InstructorService();
        var instructor = _sampleData.BuildInstructor();
        service.AddInstructor(instructor);

        var result = service.GetInstructor(instructor.Id);

        Assert.NotNull(result);
        Assert.Equal(instructor.Id, result.Id);
    }

    [Fact(DisplayName = "Get non-existing instructor throws exception")]
    public void GetInstructor_WithNonExistingId_ThrowsException()
    {
        var service = new InstructorService();

        var action = () => service.GetInstructor("NON-EXISTENT-ID");

        var exception = Assert.Throws<ArgumentException>(action);
        Assert.Contains("does not exist", exception.Message);
    }

    [Fact(DisplayName = "Remove existing instructor")]
    public void RemoveInstructor_WithExistingId_RemovesSuccessfully()
    {
        var service = new InstructorService();
        var instructor = _sampleData.BuildInstructor();
        service.AddInstructor(instructor);

        var result = service.RemoveInstructor(instructor.Id);

        Assert.True(result);
        Assert.Empty(service.GetAllInstructors());
    }

    [Fact(DisplayName = "Remove non-existing instructor returns false")]
    public void RemoveInstructor_WithNonExistingId_ReturnsFalse()
    {
        var service = new InstructorService();

        var result = service.RemoveInstructor("NON-EXISTENT-ID");

        Assert.False(result);
    }

    [Fact(DisplayName = "Get all instructors when empty")]
    public void GetAllInstructors_WhenEmpty_ReturnsEmptyList()
    {
        var service = new InstructorService();

        var result = service.GetAllInstructors();

        Assert.Empty(result);
    }

    [Fact(DisplayName = "Get all instructors with data")]
    public void GetAllInstructors_WithData_ReturnsAllInstructors()
    {
        var service = new InstructorService();
        var instructor = _sampleData.BuildInstructor();
        service.AddInstructor(instructor);

        var result = service.GetAllInstructors();

        Assert.NotEmpty(result);
        Assert.Single(result);
        Assert.Contains(instructor, result);
    }
}
