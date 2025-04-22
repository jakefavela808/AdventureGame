namespace AdventureS25;

public static class Game
{
    public static string Difficulty { get; set; } = "Normal";
    public static void PlayGame()
    {
        Console.Clear();
        AsciiArt.Print(AsciiArt.titleAndLogo);
        Console.WriteLine("1. Start Game\n2. About Game\n3. Quit Game");
        string input = CommandProcessor.GetInput();

        if (input == "1")
        {
            Console.Clear();
            TextPrinter.Print("What is your name?");
            string name = CommandProcessor.GetInput();
            Player.Name = name;
            TextPrinter.Print($"Welcome, {name}!");
            // Prompt for difficulty
            TextPrinter.Print("\nSelect difficulty:");
            TextPrinter.Print("1. Easy - XP Gain +20%, Enemy Damage -20%, Tame Chance +20%.");
            TextPrinter.Print("2. Normal - XP Gain +0%, Enemy Damage +0%, Tame Chance +0%.");
            TextPrinter.Print("3. Hard - XP Gain -15%, Enemy Damage +25%, Tame Chance -15%.");
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
            TextPrinter.Print($"Difficulty set to: {difficulty}");
            Console.Clear();
            Initialize();
            Console.WriteLine(Player.GetLocationDescription());
            Quest introQuest = new Quest("Read the Note", "Read the note in your home to begin your adventure.");
            Player.AddQuest(introQuest);
            
            bool isPlaying = true;

            while (isPlaying)
            {
                Command command = CommandProcessor.Process();

                if (command.IsValid)
                {
                    if (command.Verb == "exit")
                    {
                        TextPrinter.Print("Game Over!");
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
            AsciiArt.Print(AsciiArt.titleAndLogo);
            TextPrinter.Print("About Game");
            TextPrinter.Print("This is an adventure game where you explore and interact with the environment.");
            TextPrinter.Print("Press any key to return to the main menu...");
            Console.Write("> ");
            Console.ReadKey();
            Console.Clear();
            PlayGame();
        }
        else if (input == "3")
        {
            TextPrinter.Print("Quitting Game");
        }
        else
        {
            TextPrinter.Print("Invalid input. Please try again.");
            Console.Clear();
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