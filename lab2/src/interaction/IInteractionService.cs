namespace EduManagement.Interaction;

public interface IUserInterface
{
    void ShowMessage(string message);
    void ShowError(string error);
    void ShowWarning(string warning);
    void ShowSuccess(string success);
    void ShowHeader(string header);
    void ShowItem(string item);
    string ReadString(string prompt);
    int ReadInt(string prompt);
    bool ReadBool(string prompt);
    void WaitForInput();
    void ClearDisplay();
    void ShowMenu(string title, List<string> options);
    void ShowTable(List<string> headers, List<List<string>> rows);
}
