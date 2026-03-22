namespace Lab2.Tests.Models;

public class TrainingModuleTests
{
    [Fact(DisplayName = "Create classroom module with constructor")]
    public void CreateClassroomModule_WithConstructor_SetsProperties()
    {
        var module = new ClassroomCourse("CM-001", "Machine Learning", "AI fundamentals", "Lab 305", 20);

        Assert.Equal("CM-001", module.Id);
        Assert.Equal("Machine Learning", module.Name);
        Assert.Equal("AI fundamentals", module.Description);
        Assert.Equal("Lab 305", module.RoomNumber);
        Assert.Equal(20, module.Capacity);
        Assert.Equal("Classroom", module.GetTypeLabel());
    }

    [Fact(DisplayName = "Create remote module with constructor")]
    public void CreateRemoteModule_WithConstructor_SetsProperties()
    {
        var module = new RemoteCourse("RM-002", "DevOps Practices", "CI/CD and automation", "https://devops.edu", true);

        Assert.Equal("RM-002", module.Id);
        Assert.Equal("DevOps Practices", module.Name);
        Assert.Equal("CI/CD and automation", module.Description);
        Assert.Equal("https://devops.edu", module.PlatformUrl);
        Assert.True(module.IsSelfPaced);
        Assert.Equal("Online", module.GetTypeLabel());
    }

    [Fact(DisplayName = "Add learner to module")]
    public void AddLearner_ToModule_IncreasesEnrollment()
    {
        var module = new ClassroomCourse("MOD-003", "Security Basics", "Intro to cybersecurity", "Room 101", 15);
        var learner = new Learner("L-001", "Henry", "Irwin", "henry.i@stu.com", "2024");

        module.AddLearner(learner);

        Assert.Single(module.EnrolledLearners);
        Assert.Contains(learner, module.EnrolledLearners);
    }

    [Fact(DisplayName = "Add same learner twice does not duplicate")]
    public void AddLearner_Duplicate_DoesNotAddAgain()
    {
        var module = new ClassroomCourse("MOD-004", "Network Admin", "Network management", "Room 102", 18);
        var learner = new Learner("L-002", "Ivy", "Jones", "ivy.j@stu.com", "2024");

        module.AddLearner(learner);
        module.AddLearner(learner);

        Assert.Single(module.EnrolledLearners);
    }

    [Fact(DisplayName = "Remove learner from module")]
    public void RemoveLearner_FromModule_DecreasesEnrollment()
    {
        var module = new ClassroomCourse("MOD-005", "Cloud Security", "Cloud protection", "Room 103", 22);
        var learner = new Learner("L-003", "Jack", "Kent", "jack.k@stu.com", "2023");
        module.AddLearner(learner);

        module.RemoveLearner(learner);

        Assert.Empty(module.EnrolledLearners);
    }

    [Fact(DisplayName = "Set instructor on module")]
    public void SetInstructor_OnModule_AssignsCorrectly()
    {
        var module = new RemoteCourse("MOD-006", "API Design", "RESTful services", "https://api.edu", false);
        var instructor = new Instructor("I-003", "Karen", "Lee", "karen.l@edu.com", "Software Eng");

        module.SetInstructor(instructor);

        Assert.NotNull(module.AssignedInstructor);
        Assert.Equal(instructor.Id, module.AssignedInstructor.Id);
        Assert.True(module.HasInstructor());
    }

    [Fact(DisplayName = "Clear instructor from module")]
    public void ClearInstructor_OnModule_RemovesAssignment()
    {
        var module = new RemoteCourse("MOD-007", "Microservices", "Distributed systems", "https://micro.edu", true);
        var instructor = new Instructor("I-004", "Leo", "Martin", "leo.m@edu.com", "Architecture");
        module.SetInstructor(instructor);

        module.ClearInstructor();

        Assert.Null(module.AssignedInstructor);
        Assert.False(module.HasInstructor());
    }

    [Fact(DisplayName = "Module without instructor returns false")]
    public void HasInstructor_WhenNotAssigned_ReturnsFalse()
    {
        var module = new ClassroomCourse("MOD-008", "Data Analysis", "Statistics with Python", "Room 104", 25);

        var result = module.HasInstructor();

        Assert.False(result);
    }

    [Fact(DisplayName = "Module with default constructor has empty properties")]
    public void CreateModule_WithDefaultConstructor_HasEmptyProperties()
    {
        var module = new ClassroomCourse();

        Assert.Empty(module.Id);
        Assert.Empty(module.Name);
        Assert.Empty(module.Description);
        Assert.Empty(module.RoomNumber);
        Assert.Equal(0, module.Capacity);
    }

    [Fact(DisplayName = "Remote module with default constructor has empty properties")]
    public void CreateRemoteModule_WithDefaultConstructor_HasEmptyProperties()
    {
        var module = new RemoteCourse();

        Assert.Empty(module.Id);
        Assert.Empty(module.Name);
        Assert.Empty(module.Description);
        Assert.Empty(module.PlatformUrl);
        Assert.False(module.IsSelfPaced);
    }
}
