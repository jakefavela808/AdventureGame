namespace AdventureS25;

public static class Game
{
    public static string Difficulty { get; set; } = "Normal";
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
            // Prompt for difficulty
            Console.WriteLine("\nSelect difficulty:");
            Console.WriteLine("1. Easy\n2. Normal\n3. Hard");
            string diffInput = CommandProcessor.GetInput();
            string difficulty = "Normal";
            switch (diffInput)
            {
                case "1":
                    difficulty = "Easy";
                    break;
                case "3":
                    difficulty = "Hard";
                    break;
                default:
                    difficulty = "Normal";
                    break;
            }
            Game.Difficulty = difficulty;
            Console.WriteLine($"Difficulty set to: {difficulty}");
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
        NPCs.Initialize();
        Pals.Initialize();
        Player.Initialize();
    }
}