namespace AdventureS25;

public static class CombatCommandHandler
{
    private static Dictionary<string, Action<Command>> commandMap =
        new Dictionary<string, Action<Command>>()
        {
            {"attack", Fight},
            {"special", Special},
            {"defend", Defend},
            {"run", Run},
            {"tame", Tame},
{"help", ShowHelp},
        };


    // Battle state
    private static Pal playerPal;
    private static Pal wildPal;
    private static bool playerTurn = true;
    private static bool battleActive = false;
    private static Random rng = new Random();

    public static void StartBattle(Pal wild)
    {
        // For now, use the wildPal as the only participant (player does not have a team)
        wildPal = wild;
        wildPal.ResetHP();
        playerPal = new Pal("PlayerPal", AsciiArt.sandiePal, "Your trusted pal!", "Let's win this!", 35, 10, 7, 13);
        playerPal.ResetHP();
        playerTurn = true;
        battleActive = true;
        Console.Clear();
        Console.WriteLine(CommandList.combatCommands + "\n");
        PrintBattleIntro();
        PrintBattleStatus();
        Console.WriteLine("What will you do?");
    }

    public static void Handle(Command command)
    {
        if (!battleActive)
        {
            Console.WriteLine("There is no active battle.");
            return;
        }
        if (playerTurn)
        {
            if (commandMap.ContainsKey(command.Verb))
            {
                Action<Command> action = commandMap[command.Verb];
                action.Invoke(command);
                // If the command was "help", do NOT end the player's turn
                if (command.Verb != "help" && battleActive && !wildPal.IsFainted())
                {
                    playerTurn = false;
                    WildPalTurn();
                }
            }
            else
            {
                Console.WriteLine("Invalid action. Type 'attack', 'special', 'defend', 'run', or 'tame'\n");
            }
        }
        else
        {
            WildPalTurn();
        }
        if (battleActive && !playerTurn)
        {
            playerTurn = true;
            PrintBattleStatus();
            Console.WriteLine("What will you do?");
        }
    }

    private static void PrintBattleIntro()
    {
        Console.WriteLine($"A wild {wildPal.Name} appears!");
        Console.WriteLine(wildPal.AsciiArt);
        Console.WriteLine(wildPal.Description);
        Console.WriteLine(wildPal.Dialogue + "\n");
        Console.WriteLine($"You send out {playerPal.Name}!\n");
        Console.WriteLine(playerPal.AsciiArt);
        Console.WriteLine(playerPal.Description);
        Console.WriteLine(playerPal.Dialogue + "\n");
    }

    private static void PrintBattleStatus()
    {
        Console.WriteLine($"-- {playerPal.Name} HP: {playerPal.CurrentHP}/{playerPal.MaxHP} | {wildPal.Name} HP: {wildPal.CurrentHP}/{wildPal.MaxHP} --\n");
    }

    private static void Fight(Command command)
    {
        int damage = Math.Max(1, playerPal.Attack - wildPal.Defense/2 + rng.Next(-2, 3));
        wildPal.TakeDamage(damage);
        Console.WriteLine($"{playerPal.Name} attacks! {wildPal.Name} takes {damage} damage.");
        CheckBattleEnd();
    }

    private static void Special(Command command)
    {
        int damage = Math.Max(2, playerPal.Special - wildPal.Defense + rng.Next(0, 4));
        wildPal.TakeDamage(damage);
        Console.WriteLine($"{playerPal.Name} uses a special move! {wildPal.Name} takes {damage} damage.");
        CheckBattleEnd();
    }

    private static void Defend(Command command)
    {
        Console.WriteLine($"{playerPal.Name} braces for impact and will take less damage next turn!");
        // Set a defense boost for the next wild pal attack
        playerPal.Defense += 5;
    }

    private static void Run(Command command)
    {
        int chance = rng.Next(100);
        if (chance < 60)
        {
            Console.WriteLine("You successfully ran away!");
            EndBattle();
            Console.Clear();
            States.ChangeState(StateTypes.Exploring);
            Player.Look();
        }
        else
        {
            Console.WriteLine("Couldn't escape!");
        }
    }

    private static void Tame(Command command)
    {
        int chance = rng.Next(100);
        if (chance < 25 + (wildPal.MaxHP - wildPal.CurrentHP))
        {
            Console.WriteLine($"You tamed {wildPal.Name}! It joins your party.");
            Player.AddPalToCollection(wildPal);
            Player.CurrentLocation.Pals.Remove(wildPal);
            EndBattle();
        }
        else
        {
            Console.WriteLine($"{wildPal.Name} resisted taming!");
        }
    }

    private static void WildPalTurn()
    {
        if (!battleActive) return;
        if (wildPal.IsFainted()) return;
        // Simple AI: randomly choose attack or special
        int move = rng.Next(2);
        if (move == 0)
        {
            int damage = Math.Max(1, wildPal.Attack - playerPal.Defense/2 + rng.Next(-2, 3));
            playerPal.TakeDamage(damage);
            Console.WriteLine($"{wildPal.Name} attacks! {playerPal.Name} takes {damage} damage.");
        }
        else
        {
            int damage = Math.Max(2, wildPal.Special - playerPal.Defense + rng.Next(0, 4));
            playerPal.TakeDamage(damage);
            Console.WriteLine($"{wildPal.Name} uses a special move! {playerPal.Name} takes {damage} damage.");
        }
        // If player defended, reset defense boost
        playerPal.Defense = Math.Max(playerPal.Defense - 5, 7);
        CheckBattleEnd();
    }

    private static void CheckBattleEnd()
    {
        if (wildPal.IsFainted())
        {
            Console.WriteLine($"{wildPal.Name} fainted! You win the battle!\n");
            EndBattle();
            States.ChangeState(StateTypes.Exploring);
            Player.Look();
        }
        else if (playerPal.IsFainted())
        {
            Console.WriteLine($"{playerPal.Name} fainted! You lost the battle...\n");
            EndBattle();
            States.ChangeState(StateTypes.Exploring);
            Player.Look();
        }
    }

    private static void EndBattle()
    {
        battleActive = false;
        playerPal = null;
        wildPal = null;
        playerTurn = true;
    }

    private static void ShowHelp(Command command)
    {
        Console.WriteLine(CommandList.combatCommands);
    }

    private static void ShowPals(Command command)
    {
        Player.ShowOwnedPals();
    }
}