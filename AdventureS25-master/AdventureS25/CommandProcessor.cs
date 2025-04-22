namespace AdventureS25;

public static class CommandProcessor
{
    public static Command Process()
    {
        // get input
        string rawInput = GetInput();
        
        // make sure two words
        Command command = Parser.Parse(rawInput);

        Debugger.Write("Verb: [" + command.Verb + "]");
        Debugger.Write("Noun: [" + command.Noun + "]");
        
        // make sure we have the words in our vocabulary
        bool isValid = CommandValidator.IsValid(command);
        command.IsValid = isValid;

        // do stuff with the command
        Debugger.Write("isValid = " + isValid);

        return command;
    }
    
    public static string GetInput()
    {
        while (true)
        {
            int promptLeft = Console.CursorLeft;
            int promptTop = Console.CursorTop;
            Console.Write("> ");
            string input = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(input))
                return input.Trim();
            // If input is blank/whitespace, move cursor back to prompt line
            Console.SetCursorPosition(promptLeft, promptTop);
        }
    }
}