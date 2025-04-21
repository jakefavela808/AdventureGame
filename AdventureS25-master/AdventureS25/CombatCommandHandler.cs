namespace AdventureS25;

public static class CombatCommandHandler
{
    private static Dictionary<string, Action<Command>> commandMap =
        new Dictionary<string, Action<Command>>()
        {
            {"1", Fight},
            {"2", Defend},
            {"3", Potion},
            {"4", Run},
        };
    
    public static void Handle(Command command)
    {
        if (commandMap.ContainsKey(command.Verb))
        {
            Action<Command> action = commandMap[command.Verb];
            action.Invoke(command);
        }
    }

    private static void Fight(Command command)
    {
        Console.WriteLine("You fight it in the face parts");
    }
    
    private static void Defend(Command command)
    {
        Console.WriteLine("You defend it in the face parts");
    }

    private static void Potion(Command command)
    {
        Console.WriteLine("You quaff the potion parts");
    }
    
    private static void Run(Command command)
    {
        Console.WriteLine("You flee");
        States.ChangeState(StateTypes.Exploring);
    }
}