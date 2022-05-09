namespace AlgorithmLogic.Tools.Loggers;

public class ConsoleLogger : IProgressLogger
{
    public void LogProgress(string message)
    {
        Console.WriteLine("------------------------------------------------");
        Console.WriteLine(message);
        Console.WriteLine("------------------------------------------------");
    }
}