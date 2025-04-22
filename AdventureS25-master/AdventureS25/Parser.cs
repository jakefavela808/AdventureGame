namespace AdventureS25;

public static class Parser
{
    public static Command Parse(string input)
    {
        input = RemoveExtraSpaces(input);
        input = input.ToLower();
        
        // break the input into individual words
        List<string> words = input.Split(' ').ToList();

        Command command = new Command();
        
        if (words.Count == 2)
        {
            command.Verb = words[0];
            command.Noun = words[1];
        }
        else if (words.Count == 1)
        {
            command.Verb = words[0];
        }
        else
        {
            TextPrinter.Print("I don't understand that.");
            Console.Clear();
            Player.Look();
        }
        
        return command;
    }    
    
    private static string RemoveExtraSpaces(string input)
    {
        input = input.Trim();

        while (input.Contains("  "))
        {
            input = input.Replace("  ", " ");
        }
        
        return input;
    }
}