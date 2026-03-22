namespace EduManagement;

public class ClassroomCourse : TrainingModule
{
    private string _roomNumber;
    private int _capacity;

    public string RoomNumber
    {
        get => _roomNumber;
        set => _roomNumber = value;
    }

    public int Capacity
    {
        get => _capacity;
        set => _capacity = value;
    }

    public ClassroomCourse() : base()
    {
        _roomNumber = string.Empty;
        _capacity = 0;
    }

    public ClassroomCourse(string id, string name, string description, string roomNumber, int capacity)
        : base(id, name, description)
    {
        _roomNumber = roomNumber;
        _capacity = capacity;
    }

    public override string GetTypeLabel() => "Classroom";
}
