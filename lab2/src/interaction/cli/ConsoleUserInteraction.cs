namespace EduManagement.Interaction;

public class ConsoleInterface : IUserInterface
{
    public void ShowMessage(string message)
    {
        Console.WriteLine(message);
    }

    public void ShowError(string error)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(error);
        Console.ResetColor();
    }

    public void ShowWarning(string warning)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(warning);
        Console.ResetColor();
    }

    public void ShowSuccess(string success)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(success);
        Console.ResetColor();
    }

    public void ShowHeader(string header)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"\n=== {header} ===");
        Console.ResetColor();
    }

    public void ShowItem(string item)
    {
        Console.WriteLine($"  - {item}");
    }

    public string ReadString(string prompt)
    {
        Console.Write($"{prompt}: ");
        return Console.ReadLine()?.Trim() ?? string.Empty;
    }

    public int ReadInt(string prompt)
    {
        while (true)
        {
            var input = ReadString(prompt);
            if (int.TryParse(input, out int result))
            {
                return result;
            }
            ShowError("Please enter a valid integer.");
        }
    }

    public bool ReadBool(string prompt)
    {
        while (true)
        {
            var input = ReadString($"{prompt} (y/n)").ToLower();
            if (input == "y" || input == "yes")
                return true;
            if (input == "n" || input == "no")
                return false;
            ShowError("Please enter 'y' or 'n'.");
        }
    }

    public void WaitForInput()
    {
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }

    public void ClearDisplay()
    {
        Console.Clear();
    }

    public void ShowMenu(string title, List<string> options)
    {
        ShowHeader(title);
        for (int i = 0; i < options.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {options[i]}");
        }
        Console.WriteLine("0. Back");
    }

    public void ShowTable(List<string> headers, List<List<string>> rows)
    {
        if (headers == null || !headers.Any()) return;

        Console.WriteLine("\n" + string.Join(" | ", headers));
        Console.WriteLine(new string('-', headers.Sum(h => h.Length) + (headers.Count - 1) * 3));

        foreach (var row in rows)
        {
            Console.WriteLine(string.Join(" | ", row));
        }
    }
}
