namespace Lab2.Tests.Models;

public class PersonTests
{
    [Fact(DisplayName = "Create person with all properties")]
    public void CreatePerson_WithAllProperties_SetsCorrectly()
    {
        var person = new Instructor("P-100", "Alice", "Thompson", "alice.t@edu.com", "Biology");

        Assert.Equal("P-100", person.Id);
        Assert.Equal("Alice", person.FirstName);
        Assert.Equal("Thompson", person.LastName);
        Assert.Equal("alice.t@edu.com", person.Email);
        Assert.Equal("Biology", person.Department);
    }

    [Fact(DisplayName = "Person ToString returns formatted name")]
    public void PersonToString_WithName_ReturnsFormattedString()
    {
        var person = new Instructor("P-101", "Brian", "Cooper", "brian.c@edu.com", "Chemistry");

        var result = person.ToString();

        Assert.Equal("Brian Cooper (Chemistry)", result);
    }

    [Fact(DisplayName = "Learner ToString returns formatted string")]
    public void LearnerToString_WithNameAndYear_ReturnsFormattedString()
    {
        var learner = new Learner("L-100", "Diana", "Evans", "diana.e@stu.com", "2024");

        var result = learner.ToString();

        Assert.Equal("Diana Evans (Enrolled: 2024)", result);
    }

    [Fact(DisplayName = "Create learner with default constructor")]
    public void CreateLearner_WithDefaultConstructor_HasEmptyProperties()
    {
        var learner = new Learner();

        Assert.Empty(learner.Id);
        Assert.Empty(learner.FirstName);
        Assert.Empty(learner.LastName);
        Assert.Empty(learner.Email);
        Assert.Empty(learner.EnrollmentYear);
    }

    [Fact(DisplayName = "Create instructor with default constructor")]
    public void CreateInstructor_WithDefaultConstructor_HasEmptyProperties()
    {
        var instructor = new Instructor();

        Assert.Empty(instructor.Id);
        Assert.Empty(instructor.FirstName);
        Assert.Empty(instructor.LastName);
        Assert.Empty(instructor.Email);
        Assert.Empty(instructor.Department);
    }

    [Fact(DisplayName = "Copy learner creates independent instance")]
    public void CopyLearner_CreatesIndependentCopy()
    {
        var original = new Learner("L-200", "Frank", "Green", "frank.g@stu.com", "2023");
        var copy = new Learner(original);

        Assert.Equal(original.Id, copy.Id);
        Assert.Equal(original.FirstName, copy.FirstName);
        Assert.Equal(original.LastName, copy.LastName);
        Assert.Equal(original.Email, copy.Email);
        Assert.Equal(original.EnrollmentYear, copy.EnrollmentYear);

        copy.FirstName = "Modified";
        Assert.NotEqual(original.FirstName, copy.FirstName);
    }

    [Fact(DisplayName = "Copy instructor creates independent instance")]
    public void CopyInstructor_CreatesIndependentCopy()
    {
        var original = new Instructor("I-200", "Grace", "Hill", "grace.h@edu.com", "Math");
        var copy = new Instructor(original);

        Assert.Equal(original.Id, copy.Id);
        Assert.Equal(original.FirstName, copy.FirstName);
        Assert.Equal(original.LastName, copy.LastName);
        Assert.Equal(original.Email, copy.Email);
        Assert.Equal(original.Department, copy.Department);

        copy.Department = "Physics";
        Assert.NotEqual(original.Department, copy.Department);
    }
}
