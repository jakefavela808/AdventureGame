namespace AdventureS25;

public static class Debugger
{
    private static bool isActive = false;
    
    public static void Write(string message)
    {
        if (isActive)
        {
            TextPrinter.Print(message);
        }
    }

    public static void Tron()
    {
        isActive = true;
        TextPrinter.Print("Debugging on");
    }
    
    public static void Troff()
    {
        isActive = false;
        TextPrinter.Print("Debugging off");
    }
}