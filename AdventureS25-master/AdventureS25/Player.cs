namespace AdventureS25;

public static class Player
{
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

    public static void Move(Command command)
    {
        if (CurrentLocation.CanMoveInDirection(command))
        {
            Console.Clear();
            CurrentLocation = CurrentLocation.GetLocationInDirection(command);
            Console.WriteLine(CurrentLocation.GetDescription());
        }
        else
        {
            Console.WriteLine("You can't move " + command.Noun + ".");
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
            Console.WriteLine("I don't know what " + command.Noun + " is.");
        }
        else if (!CurrentLocation.HasItem(item))
        {
            Console.WriteLine("There is no " + command.Noun + " here.");
        }
        else if (!item.IsTakeable)
        {
            Console.WriteLine("The " + command.Noun + " can't be taked.");
        }
        else
        {
            Inventory.Add(item);
            CurrentLocation.RemoveItem(item);
            item.Pickup();
            Console.WriteLine("You take the " + command.Noun + ".");
        }
    }

    public static void ShowInventory()
    {
        if (Inventory.Count == 0)
        {
            Console.WriteLine("You are empty-handed.");
        }
        else
        {
            Console.WriteLine("You are carrying:");
            foreach (Item item in Inventory)
            {
                string article = SemanticTools.CreateArticle(item.Name);
                Console.WriteLine(" " + article + " " + item.Name);
            }
        }
    }

    public static void AddPalToCollection(Pal pal)
    {
        if (!OwnedPals.Contains(pal))
        {
            OwnedPals.Add(pal);
            Console.WriteLine($"{pal.Name} has been added to your collection!");
        }
        else
        {
            Console.WriteLine($"You already own {pal.Name}.");
        }
    }

    public static void ShowOwnedPals()
    {
        if (OwnedPals.Count == 0)
        {
            Console.WriteLine("You do not have any pals yet.");
        }
        else
        {
            Console.WriteLine("Your Pals:");
            foreach (var pal in OwnedPals)
            {
                Console.WriteLine($"- {pal.Name} (HP: {pal.CurrentHP}/{pal.MaxHP})");
            }
        }
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
            Console.WriteLine("I don't know what " + article + " " + command.Noun + " is.");
        }
        else if (!Inventory.Contains(item))
        {
            Console.WriteLine("You're not carrying the " + command.Noun + ".");
        }
        else
        {
            Inventory.Remove(item);
            CurrentLocation.AddItem(item);
            Console.WriteLine("You drop the " + command.Noun + ".");
        }

    }

    public static void Drink(Command command)
    {
        if (command.Noun == "beer")
        {
            Console.WriteLine("** drinking beer");
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
            Console.WriteLine("Trying to move to unknown location: " + locationName + ".");
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
            Console.WriteLine($"\nNew quest added: {quest.Name}!");
        }
        else
        {
            Console.WriteLine($"\nYou already have the quest: {quest.Name}.");
        }
    }

    public static void RemoveQuest(string questName)
    {
        var quest = Quests.FirstOrDefault(q => q.Name == questName);
        if (quest != null)
        {
            Quests.Remove(quest);
            Console.WriteLine($"\nQuest removed: {questName}");
        }
    }

    public static void ShowQuests()
    {
        if (Quests.Count == 0)
        {
            Console.WriteLine("\nYou have no active quests.");
        }
        else
        {
            Console.WriteLine("\n============ YOUR QUESTS ============");
            foreach (var quest in Quests)
            {
                Console.WriteLine(quest);
            }
        }
    }
}