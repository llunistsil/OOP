using EduManagement.Interaction;

namespace EduManagement;

public class EntryPoint
{
    public static void Main(string[] args)
    {
        var ui = new ConsoleInterface();

        try
        {
            var cli = EduCli.Instance;
            cli.InitializeData();
            cli.Execute();
        }
        catch (Exception ex)
        {
            ui.ShowError($"Critical error: {ex.Message}");
        }
        finally
        {
            ui.ShowMessage("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
