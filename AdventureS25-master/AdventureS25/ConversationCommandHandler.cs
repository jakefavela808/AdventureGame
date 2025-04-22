namespace AdventureS25;

public static class ConversationCommandHandler
{
    private static Dictionary<string, Action<Command>> commandMap =
        new Dictionary<string, Action<Command>>()
        {
            {"yes", Yes},
            {"no", No},
            {"leave", Leave},
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
        Console.WriteLine("You agreed");
    }
    
    private static void No(Command command)
    {
        Console.WriteLine("You are disagreed");
    }

    private static void ShowHelp(Command command)
    {
        Console.WriteLine(CommandList.conversationCommands);
    }

    private static void Leave(Command command)
    {
        Console.WriteLine("You are dead");
        States.ChangeState(StateTypes.Exploring);
        Console.Clear();
        Player.Look();
    }
}