namespace AdventureS25;

public static class Game
{
    public static void PlayGame()
    {
        Console.WriteLine(AsciiArt.titleAndLogo);
        Console.WriteLine("1. Start Game\n2. About Game\n3. Quit Game");
        string input = CommandProcessor.GetInput();

        if (input == "1")
        {
            Console.Clear();
            Console.WriteLine("What is your name?");
            string name = CommandProcessor.GetInput();
            Console.WriteLine($"Welcome, {name}!");
            Console.Clear();
            Initialize();
            Console.WriteLine(Player.GetLocationDescription());
            
            bool isPlaying = true;

            while (isPlaying)
            {
                Command command = CommandProcessor.Process();

                if (command.IsValid)
                {
                    if (command.Verb == "exit")
                    {
                        Console.WriteLine("Game Over!");
                        isPlaying = false;
                    }
                    else
                    {
                        CommandHandler.Handle(command);
                    }
                }
            }
        }
        else if (input == "2")
        {
            Console.Clear();
            Console.WriteLine(AsciiArt.titleAndLogo);
            Console.WriteLine("About Game");
            Console.WriteLine("This is an adventure game where you explore and interact with the environment.");
            Console.WriteLine("Press any key to return to the main menu...");
            Console.Write("> ");
            Console.ReadKey();
            Console.Clear();
            PlayGame();
        }
        else if (input == "3")
        {
            Console.WriteLine("Quitting Game");
        }
        else
        {
            Console.WriteLine("Invalid input. Please try again.");
            PlayGame();
        }
    }

    private static void Initialize()
    {
        Conditions.Initialize();
        States.Initialize();
        Map.Initialize();
        Items.Initialize();
        Player.Initialize();
    }
}