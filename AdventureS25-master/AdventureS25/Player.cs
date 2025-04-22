namespace AdventureS25;

public static class Player
{
    public static string Name { get; set; } = string.Empty;
    public static Location CurrentLocation;
    public static List<Item> Inventory;
    public static List<Pal> OwnedPals;
    public static List<Quest> Quests = new List<Quest>();

    public static void Initialize()
    {
        Inventory = new List<Item>();
        OwnedPals = new List<Pal>();
        Quests = new List<Quest>();
        CurrentLocation = Map.StartLocation;
    }

    public static void Read(Command command)
    {
        if (command.Noun != "note")
        {
            TextPrinter.Print("You can't read that.");
            return;
        }
        var note = Items.GetItemByName("note");
        if (!Inventory.Contains(note) && !CurrentLocation.HasItem(note))
        {
            TextPrinter.Print("There is no note here to read.");
            Console.Clear();
            Player.Look();
            return;
        }
        // Mark the quest as complete
        var quest = Quests.FirstOrDefault(q => q.Name == "Read the Note");
        if (quest != null && !quest.IsCompleted)
        {
            quest.IsCompleted = true;
            Console.Clear();
            Player.Look();
            TextPrinter.Print("\nYou unfold the note.\n\nDear Adventurer,\n\nListen up fucker! I heard you're trying to become some kind of Pal Tamer or whatever. GOOD NEWS! I'm gonna help you not completely suck at it! I've been studying this AMAZING new Pal specimen that's perfect for beginners.\n\nGet your ass over to my Fusion Lab ASAP!!! Don't make me come find you, because I WILL, and you WON'T like it! This is important COMPUTER SCIENCE happening here!\n\nSincerely, \nProf. Jon (the smartest Computer Scientist in this dimension)");
            // Add next quest
            Console.Clear();
            Player.Look();
            TextPrinter.Print("\nQuest completed!");
            Quest meetJon = new Quest("Meet Professor Jon", "Visit Professor Jon at his lab. He wants to meet you about a special research project.");
            AddQuest(meetJon);
        }
        else
        {
            TextPrinter.Print("You have already read the note.");
        }
    }


    public static void Move(Command command)
    {
        // Prevent leaving the room until 'Read the Note' quest is completed
        var readNoteQuest = Quests.FirstOrDefault(q => q.Name == "Read the Note");
        if (readNoteQuest != null && !readNoteQuest.IsCompleted)
        {
            TextPrinter.Print("You should read the note before leaving!");
            return;
        }
        if (CurrentLocation.CanMoveInDirection(command))
        {
            Console.Clear();
            CurrentLocation = CurrentLocation.GetLocationInDirection(command);
            Console.WriteLine(CurrentLocation.GetDescription());
        }
        else
        {
            TextPrinter.Print("You can't move " + command.Noun + ".");
            Console.Clear();
            Player.Look();
        }
    }

    public static string GetLocationDescription()
    {
        return CurrentLocation.GetDescription();
    }

    public static void Take(Command command)
    {
        // figure out which item to take: turn the noun into an item
        Item item = Items.GetItemByName(command.Noun);

        if (item == null)
        {
            TextPrinter.Print("I don't know what " + command.Noun + " is.");
            Console.Clear();
            Look();
        }
        else if (!CurrentLocation.HasItem(item))
        {
            TextPrinter.Print("There is no " + command.Noun + " here.");
            Console.Clear();
            Look();
        }
        else if (!item.IsTakeable)
        {
            TextPrinter.Print("The " + command.Noun + " can't be taken.");
            Console.Clear();
            Look();
        }
        else
        {
            Inventory.Add(item);
            CurrentLocation.RemoveItem(item);
            item.Pickup();
            TextPrinter.Print("You take the " + command.Noun + ".");
            Console.Clear();
            Look();
        }
    }

    public static void ShowInventory()
    {
        if (Inventory.Count == 0)
        {
            TextPrinter.Print("You are empty-handed.");
            Console.Clear();
            Look();
        }
        else
        {
            TextPrinter.Print("You are carrying:");
            foreach (Item item in Inventory)
            {
                string article = SemanticTools.CreateArticle(item.Name);
                TextPrinter.Print(article + " " + item.Name);
            }
        }
        Console.Clear();
        Player.Look();
    }

    public static void AddPalToCollection(Pal pal)
    {
        if (!OwnedPals.Contains(pal))
        {
            OwnedPals.Add(pal);
            TextPrinter.Print($"{pal.Name} has been added to your collection!");
        }
        else
        {
            TextPrinter.Print($"You already own {pal.Name}.");
        }
    }

    public static void ShowOwnedPals()
    {
        if (OwnedPals.Count == 0)
        {
            TextPrinter.Print("You do not have any pals yet.");
        }
        else
        {
            TextPrinter.Print("Your Pals:");
            for (int i = 0; i < OwnedPals.Count; i++)
            {
                var pal = OwnedPals[i];
                TextPrinter.Print($"[{i + 1}] {pal.Name} (Level {pal.Level}, XP: {pal.XP}/{pal.XPToNextLevel}, HP: {pal.CurrentHP}/{pal.MaxHP})");
            }
        }
    }

    public static Pal PromptSelectPal(string prompt = "Select a Pal:")
    {
        if (OwnedPals.Count == 0)
        {
            TextPrinter.Print("You have no pals!");
            return null;
        }
        if (OwnedPals.Count == 1)
        {
            return OwnedPals[0];
        }
        TextPrinter.Print(prompt);
        for (int i = 0; i < OwnedPals.Count; i++)
        {
            var pal = OwnedPals[i];
            TextPrinter.Print($"[{i + 1}] {pal.Name} (Level {pal.Level}, XP: {pal.XP}/{pal.XPToNextLevel}, HP: {pal.CurrentHP}/{pal.MaxHP})");
        }
        int choice = -1;
        while (choice < 1 || choice > OwnedPals.Count)
        {
            TextPrinter.Print($"Enter number (1-{OwnedPals.Count}): ");
            string input = CommandProcessor.GetInput();
            while (string.IsNullOrWhiteSpace(input))
            {
                input = CommandProcessor.GetInput();
            }
            int.TryParse(input, out choice);
        }
        return OwnedPals[choice - 1];
    }


    public static void Look()
    {
        Console.WriteLine(CurrentLocation.GetDescription());
    }

    public static void Drop(Command command)
    {       
        Item item = Items.GetItemByName(command.Noun);

        if (item == null)
        {
            string article = SemanticTools.CreateArticle(command.Noun);
            TextPrinter.Print("I don't know what " + article + " " + command.Noun + " is.");
        }
        else if (!Inventory.Contains(item))
        {
            TextPrinter.Print("You're not carrying the " + command.Noun + ".");
        }
        else
        {
            Inventory.Remove(item);
            CurrentLocation.AddItem(item);
            TextPrinter.Print("You drop the " + command.Noun + ".");
        }

    }

    public static void Drink(Command command)
    {
        if (command.Noun == "beer")
        {
            TextPrinter.Print("** drinking beer");
            Conditions.ChangeCondition(ConditionTypes.IsDrunk, true);
            RemoveItemFromInventory("beer");
            AddItemToInventory("beer-bottle");
        }
    }

    public static void AddItemToInventory(string itemName)
    {
        Item item = Items.GetItemByName(itemName);

        if (item == null)
        {
            return;
        }
        
        Inventory.Add(item);
    }

    public static void RemoveItemFromInventory(string itemName)
    {
        Item item = Items.GetItemByName(itemName);
        if (item == null)
        {
            return;
        }
        Inventory.Remove(item);
    }

    public static void MoveToLocation(string locationName)
    {
        // look up the location object based on the name
        Location newLocation = Map.GetLocationByName(locationName);
        
        // if there's no location with that name
        if (newLocation == null)
        {
            TextPrinter.Print("Trying to move to unknown location: " + locationName + ".");
            return;
        }
            
        // set our current location to the new location
        CurrentLocation = newLocation;
        
        // print out a description of the location
        Look();
    }

    public static void AddQuest(Quest quest)
    {
        if (!Quests.Any(q => q.Name == quest.Name))
        {
            Quests.Add(quest);
            TextPrinter.Print($"\nNew quest added: {quest.Name}!");
        }
        else
        {
            TextPrinter.Print($"\nYou already have the quest: {quest.Name}.");
        }
    }

    public static void RemoveQuest(string questName)
    {
        var quest = Quests.FirstOrDefault(q => q.Name == questName);
        if (quest != null)
        {
            Quests.Remove(quest);
            TextPrinter.Print($"\nQuest removed: {questName}");
        }
    }

    public static void ShowQuests()
    {
        if (Quests.Count == 0)
        {
            TextPrinter.Print("\nYou have no active quests.");
        }
        else
        {
            Console.Clear();
            Player.Look();
            Console.WriteLine("\n============ YOUR QUESTS ============\n");
            foreach (var quest in Quests)
            {
                TextPrinter.Print(quest.ToString());
            }
            Console.Clear();
            Player.Look();
        }
    }

    // Handles giving XP rewards for quests
    public static void GiveReward(int xp)
    {
        if (OwnedPals == null || OwnedPals.Count == 0)
        {
            TextPrinter.Print("You don't have a Pal to level up yet! Find or receive a Pal first.");
            return;
        }
        Pal targetPal;
        if (OwnedPals.Count == 1)
        {
            targetPal = OwnedPals[0];
        }
        else
        {
            TextPrinter.Print($"Choose a Pal to receive {xp} XP:");
            for (int i = 0; i < OwnedPals.Count; i++)
            {
                var pal = OwnedPals[i];
                TextPrinter.Print($"[{i + 1}] {pal.Name} (Level {pal.Level}, XP: {pal.XP}/{pal.XPToNextLevel}, HP: {pal.CurrentHP}/{pal.MaxHP})");
            }
            int choice = -1;
            while (choice < 1 || choice > OwnedPals.Count)
            {
                TextPrinter.Print($"Enter number (1-{OwnedPals.Count}): ");
                string input = CommandProcessor.GetInput();
                while (string.IsNullOrWhiteSpace(input))
                {
                    input = CommandProcessor.GetInput();
                }
                int.TryParse(input, out choice);
            }
            targetPal = OwnedPals[choice - 1];
        }
        targetPal.GainXP(xp);
    }
}