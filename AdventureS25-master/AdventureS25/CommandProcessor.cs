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
        string input = "";
        int promptLeft = Console.CursorLeft;
        int promptTop = Console.CursorTop;
        Console.Write("> ");
        while (true)
        {
            var key = Console.ReadKey(intercept: true);
            if (key.Key == ConsoleKey.Enter)
            {
                if (!string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine();
                    return input.Trim();
                }
                else
                {
                    // Clear the current prompt line and reset cursor
                    Console.SetCursorPosition(promptLeft, promptTop);
                    Console.Write(new string(' ', Console.WindowWidth));
                    Console.SetCursorPosition(promptLeft, promptTop);
                    Console.Write("> ");
                    input = "";
                }
            }
            else if (key.Key == ConsoleKey.Backspace)
            {
                if (input.Length > 0)
                {
                    input = input.Substring(0, input.Length - 1);
                    Console.Write("\b \b");
                }
            }
            else if (!char.IsControl(key.KeyChar))
            {
                input += key.KeyChar;
                Console.Write(key.KeyChar);
            }
        }
    }
}