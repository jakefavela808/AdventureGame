namespace AdventureS25;

public static class ConversationCommandHandler
{
    private static Dictionary<string, Action<Command>> commandMap =
        new Dictionary<string, Action<Command>>()
        {
            {"yes", Yes},
            {"no", No},
            {"help", ShowHelp},
        };
    
    public static void Handle(Command command)
    {
        if (commandMap.ContainsKey(command.Verb))
        {
            Action<Command> action = commandMap[command.Verb];
            action.Invoke(command);
        }
    }

    private static void Yes(Command command)
    {
        TextPrinter.Print("You agreed");
    }
    
    private static void No(Command command)
    {
        TextPrinter.Print("You disagreed");
    }

    private static void ShowHelp(Command command)
    {
        Console.WriteLine(CommandList.conversationCommands);
    }
}