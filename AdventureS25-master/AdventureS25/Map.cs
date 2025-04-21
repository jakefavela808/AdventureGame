namespace AdventureS25;

public static class Map
{
    private static Dictionary<string, Location> nameToLocation = 
        new Dictionary<string, Location>();
    public static Location StartLocation;
    
    public static void Initialize()
    {
        Location home = new Location(CommandList.exploreCommands, "Home", AsciiArt.homeLocation, "This is your home. Everything starts here.", LocationType.Indoor);
        nameToLocation.Add("Home", home);
        
        Location verdantGrasslands = new Location(CommandList.exploreCommands, "Verdent Grasslands", AsciiArt.grasslandsLocation, "A lush, open field where grass-type Pals roam freely. The gentle breeze carries sweet scents of wildflowers, and you can see small Pals playing in the tall grass.", LocationType.Outdoor);
        nameToLocation.Add("Verdent Grasslands", verdantGrasslands);

        Location laboratory = new Location(CommandList.exploreCommands, "Professor Jon's Lab", AsciiArt.laboratoryLocation, "A high-tech laboratory where scientists study Pal evolution and genetics. Steel and electric-type Pals assist with experiments, moving between complex machinery.", LocationType.Indoor);
        nameToLocation.Add("Professor Jon's Lab", laboratory);

        Location palCenter = new Location(CommandList.exploreCommands, "Pal Center", AsciiArt.palCenterLocation, "A modern healing facility with state-of-the-art technology for treating injured Pals. The center is staffed by friendly nurses and doctors who can restore your Pals to full health. A large red and white sign hangs above the entrance.", LocationType.Indoor);
        nameToLocation.Add("Pal Center", palCenter);
        
        home.AddConnection("north", verdantGrasslands);
        verdantGrasslands.AddConnection("south", home);
        verdantGrasslands.AddConnection("east", laboratory);
        verdantGrasslands.AddConnection("west", palCenter);
        palCenter.AddConnection("east", verdantGrasslands);
        laboratory.AddConnection("west", verdantGrasslands);

        StartLocation = home;
    }
    

    public static void AddItem(string itemName, string locationName)
    {
        // find out which Location is named locationName
        Location location = GetLocationByName(locationName);
        Item item = Items.GetItemByName(itemName);
        
        // add the item to the location
        if (item != null && location != null)
        {
            location.AddItem(item);
        }
    }

    public static void AddNPC(NPC npc, string locationName)
    {
        Location location = GetLocationByName(locationName);
        if (npc != null && location != null)
        {
            location.AddNPC(npc);
        }
    }

    public static void RemoveNPC(NPC npc, string locationName)
    {
        Location location = GetLocationByName(locationName);
        if (npc != null && location != null)
        {
            location.RemoveNPC(npc);
        }
    }
    
    public static void RemoveItem(string itemName, string locationName)
    {
        // find out which Location is named locationName
        Location location = GetLocationByName(locationName);
        Item item = Items.GetItemByName(itemName);
        
        // remove the item to the location
        if (item != null && location != null)
        {
            location.RemoveItem(item);
        }
    }

    public static Location GetLocationByName(string locationName)
    {
        if (nameToLocation.ContainsKey(locationName))
        {
            return nameToLocation[locationName];
        }
        else
        {
            return null;
        }
    }

    public static void AddConnection(string startLocationName, string direction, 
        string endLocationName)
    {
        // get the location objects based on the names
        Location start = GetLocationByName(startLocationName);
        Location end = GetLocationByName(endLocationName);
        
        // if the locations don't exist
        if (start == null || end == null)
        {
            Console.WriteLine("Tried to create a connection between unknown locations: " +
                              startLocationName + " and " + endLocationName);
            return;
        }
            
        // create the connection
        start.AddConnection(direction, end);
    }

    public static void RemoveConnection(string startLocationName, string direction)
    {
        Location start = GetLocationByName(startLocationName);
        
        if (start == null)
        {
            Console.WriteLine("Tried to remove a connection from an unknown location: " +
                              startLocationName);
            return;
        }

        start.RemoveConnection(direction);
    }
}