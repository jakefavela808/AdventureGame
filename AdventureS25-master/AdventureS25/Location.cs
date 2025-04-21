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
    public string Description;
    public string AsciiArt;
    public string AvailableCommands;
    
    public Dictionary<string, Location> Connections;
    public List<Item> Items = new List<Item>();
    public List<NPC> NPCs = new List<NPC>();
    
    public Location(string availableCommandsInput, string nameInput, string asciiArtInput, string descriptionInput, LocationType type)
    {
        name = nameInput.ToUpper();
        Description = descriptionInput;
        AsciiArt = asciiArtInput;
        AvailableCommands = availableCommandsInput;

        Connections = new Dictionary<string, Location>();
        NPCs = new List<NPC>();
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
        string fullDescription = AvailableCommands + "\n" + "============ " + name + " ============" + "\n" + AsciiArt + "\n" + Description;

        // Show a message if an NPC is present (but no ASCII art or desc)
        if (NPCs.Count > 0)
        {
            fullDescription += "\nYou see " + NPCs[0].Name + " here.";
        }
        foreach (Item item in Items)
        {
            fullDescription += "\n" + item.GetLocationDescription();
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

    public void RemoveNPC(NPC npc)
    {
        NPCs.Remove(npc);
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