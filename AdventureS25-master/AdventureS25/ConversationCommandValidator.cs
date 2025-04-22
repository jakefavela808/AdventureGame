namespace AdventureS25;

public static class ConversationCommandValidator
{
    public static bool IsValid(Command command)
    {
        if (command.Verb == "yes" || command.Verb == "no")
        {
            return true;
        }
        Console.WriteLine("Valid commands are: yes, no");
        return false;
    }
}