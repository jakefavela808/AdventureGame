namespace AdventureS25;

public static class Debugger
{
    private static bool isActive = false;
    
    public static void Write(string message)
    {
        if (isActive)
        {
            Console.WriteLine(message);
        }
    }

    public static void Tron()
    {
        isActive = true;
        Console.WriteLine("Debugging on");
    }
    
    public static void Troff()
    {
        isActive = false;
        Console.WriteLine("Debugging off");
    }
}