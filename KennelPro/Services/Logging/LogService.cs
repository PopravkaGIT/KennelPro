namespace KennelPro.Services.Logging;

public class LogService
{
    public void Info(string message)
    {
        Console.WriteLine($"[INFO] {message}");
    }

    public void Warning(string message)
    {
        Console.WriteLine($"[WARNING] {message}");
    }

    public void Error(string message)
    {
        Console.WriteLine($"[ERROR] {message}");
    }
}