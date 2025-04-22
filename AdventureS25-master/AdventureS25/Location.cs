namespace AdventureS25;

public enum LocationType
{
    Indoor,
    Outdoor
}


public class Location
{
    public LocationType Type { get; set; }
    private string name;
    public string GetName() { return name; }

    // Helper to convert location names to title case
    private string ToTitleCase(string input)
    {
        if (string.IsNullOrEmpty(input)) return input;
        var ti = System.Globalization.CultureInfo.CurrentCulture.TextInfo;
        return ti.ToTitleCase(input.ToLower());
    }
    public string Description;
    public string AsciiArt;
    public string AvailableCommands;
    
    public Dictionary<string, Location> Connections;
    public List<Item> Items = new List<Item>();
    public List<NPC> NPCs = new List<NPC>();
    public List<Pal> Pals = new List<Pal>();
    
    public Location(string availableCommandsInput, string nameInput, string asciiArtInput, string descriptionInput, LocationType type)
    {
        name = nameInput.ToUpper();
        Description = descriptionInput;
        AsciiArt = asciiArtInput;
        AvailableCommands = availableCommandsInput;

        Connections = new Dictionary<string, Location>();
        NPCs = new List<NPC>();
        Pals = new List<Pal>();
    }

    public void AddConnection(string direction, Location location)
    {
        Connections.Add(direction, location);
    }

    public bool CanMoveInDirection(Command command)
    {
        if (Connections.ContainsKey(command.Noun))
        {
            return true;
        }
        return false;
    }

    public Location GetLocationInDirection(Command command)
    {
        return Connections[command.Noun];
    }

    public string GetDescription()
    {
        string fullDescription = AvailableCommands + "\n" + "============ " + name + " ============" + "\n\n" + AsciiArt + "\n" + Description;

        // Show a message if an NPC is present (but no ASCII art or desc)
        if (NPCs.Count > 0)
        {
            fullDescription += "\nYou see " + NPCs[0].Name + " here.";
        }
        // Show a message if a Pal is present
        if (Pals.Count > 0)
        {
            fullDescription += "\nA pal named " + Pals[0].Name + " is here.";
        }
        foreach (Item item in Items)
        {
            fullDescription += "\n" + item.GetLocationDescription();
        }

        // List possible directions and where they lead
        if (Connections.Count > 0)
        {
            // Format: North (Verdant Grasslands) East (Professor Jon's Lab)
            List<string> dirPairs = new List<string>();
            foreach (var kvp in Connections)
            {
                string dir = char.ToUpper(kvp.Key[0]) + kvp.Key.Substring(1).ToLower();
                string locName = kvp.Value != null ? ToTitleCase(kvp.Value.GetName()) : "Unknown";
                dirPairs.Add($"{dir} ({locName})");
            }
            fullDescription += "\n\nPossible directions: " + string.Join(" ", dirPairs);
        }
        
        return fullDescription;
    }

    public void AddItem(Item item)
    {
        Debugger.Write("Adding item "+ item.Name + "to " + name);
        Items.Add(item);
    }

    public void AddNPC(NPC npc)
    {
        Debugger.Write($"Adding NPC {npc.Name} to {name}");
        NPCs.Add(npc);
    }

    public void AddPal(Pal pal)
    {
        Debugger.Write($"Adding Pal {pal.Name} to {name}");
        Pals.Add(pal);
    }

    public void RemoveNPC(NPC npc)
    {
        NPCs.Remove(npc);
    }

    public void RemovePal(Pal pal)
    {
        Pals.Remove(pal);
    }

    public bool HasItem(Item itemLookingFor)
    {
        foreach (Item item in Items)
        {
            if (item.Name == itemLookingFor.Name)
            {
                return true;
            }
        }
        
        return false;
    }

    public void RemoveItem(Item item)
    {
        Items.Remove(item);
    }

    public void RemoveConnection(string direction)
    {
        if (Connections.ContainsKey(direction))
        {
            Connections.Remove(direction);
        }
    }
}