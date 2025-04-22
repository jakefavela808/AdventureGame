namespace AdventureS25;

public static class CombatCommandValidator
{
    public static bool IsValid(Command command)
    {
        if (command.Verb == "attack" || command.Verb == "special" ||
            command.Verb == "defend" || command.Verb == "run" ||
            command.Verb == "tame" || command.Verb == "help")
        {
            return true;
        }
        Console.WriteLine("Valid commands are: attack, special, defend, run, tame, help");
        return false;
    }
}