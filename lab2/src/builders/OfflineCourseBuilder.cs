namespace EduManagement.Builders;

public interface IClassroomCourseBuilder
{
    ClassroomCourseBuilder WithId(string id);
    ClassroomCourseBuilder WithName(string name);
    ClassroomCourseBuilder WithDescription(string description);
    ClassroomCourseBuilder WithRoom(string roomNumber);
    ClassroomCourseBuilder WithCapacity(int capacity);
}

public class ClassroomCourseBuilder : BaseBuilder<ClassroomCourseBuilder, ClassroomCourse>, IClassroomCourseBuilder
{
    protected override ClassroomCourseBuilder Execute(Action operation)
    {
        operation();
        return this;
    }

    public ClassroomCourseBuilder WithId(string id)
    {
        return Execute(() => _instance.Id = id);
    }

    public ClassroomCourseBuilder WithName(string name)
    {
        return Execute(() => _instance.Name = name);
    }

    public ClassroomCourseBuilder WithDescription(string description)
    {
        return Execute(() => _instance.Description = description);
    }

    public ClassroomCourseBuilder WithRoom(string roomNumber)
    {
        return Execute(() => _instance.RoomNumber = roomNumber);
    }

    public ClassroomCourseBuilder WithCapacity(int capacity)
    {
        return Execute(() => _instance.Capacity = capacity);
    }

    public override ClassroomCourse Create()
    {
        return _instance;
    }
}
