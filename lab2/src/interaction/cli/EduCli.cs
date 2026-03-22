using EduManagement.System;

namespace EduManagement.Interaction;

public interface ICliRunner
{
    void Execute();
    void InitializeData();
}

public class EduCli : ICliRunner
{
    private static readonly EduCli _instance = new EduCli();
    private readonly EduManagementSystem _management;
    private readonly IUserInterface _ui;

    public static EduCli Instance => _instance;

    private EduCli()
    {
        _management = new EduManagementSystem();
        _ui = new ConsoleInterface();
    }

    public void Execute()
    {
        ShowWelcomeScreen();
        ShowFarewellScreen();
    }

    public void InitializeData()
    {
        _ui.ShowHeader("Loading sample data");

        try
        {
            _management.RegisterInstructor("I001", "John", "Smith", "john@edu.com", "Computer Science");
            _management.RegisterInstructor("I002", "Sarah", "Johnson", "sarah@edu.com", "Mathematics");
            _management.RegisterInstructor("I003", "Michael", "Brown", "michael@edu.com", "Physics");

            _management.CreateRemoteModule("MOD101", "Programming Basics", "Fundamental programming concepts", "https://learn.edu.com", false);
            _management.CreateClassroomModule("MOD201", "Advanced Mathematics", "Higher level math", "Room 405", 25);
            _management.CreateRemoteModule("MOD301", "Web Development", "Modern web technologies", "https://courses.edu.com", true);

            _management.AssignInstructorToModule("MOD101", "I001");
            _management.AssignInstructorToModule("MOD201", "I002");
            _management.AssignInstructorToModule("MOD301", "I001");

            _management.EnrollLearnerInModule("MOD101", "L001", "Alice", "Williams", "alice@learner.com", "2023");
            _management.EnrollLearnerInModule("MOD101", "L002", "Bob", "Davis", "bob@learner.com", "2023");
            _management.EnrollLearnerInModule("MOD201", "L001", "Alice", "Williams", "alice@learner.com", "2023");
            _management.EnrollLearnerInModule("MOD301", "L003", "Carol", "Miller", "carol@learner.com", "2024");

            _ui.ShowSuccess("Sample data loaded successfully!");
            _ui.ShowMessage("Created: 3 instructors, 3 modules, 3 learners");
        }
        catch (Exception ex)
        {
            _ui.ShowError($"Error loading sample data: {ex.Message}");
        }

        _ui.WaitForInput();
    }

    private void ShowWelcomeScreen()
    {
        _ui.ClearDisplay();
        _ui.ShowHeader("EDUCATION MANAGEMENT SYSTEM");
        _ui.ShowMessage("Terminal Client v1.0");
    }

    private void ShowFarewellScreen()
    {
        _ui.ShowSuccess("Thank you for using the system. Goodbye!");
    }
}
