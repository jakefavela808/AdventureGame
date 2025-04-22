namespace AdventureS25;

public static class Items
{
    private static Dictionary<string, Item> nameToItem = 
        new Dictionary<string, Item>();
    
    public static void Initialize()
    {
        // Quest item: sword for Jon's quest
        Item sword = new Item("sword",
            "A long sword, possibly the one Professor Jon lost.", 
            "There is a long sword stuck in a rock here.");
        nameToItem.Add("sword", sword);
        
        Item donut = new Item("donut",
            "A giant concrete donut that you can't take", 
            "A giant concrete donut rests on the floor.",
            false);
        nameToItem.Add("donut", donut);
        
        Item beer = new Item("beer",
            "beer's beer",
            "There is a beer's beer in a beer here.");
        nameToItem.Add("beer", beer);
        
        Item beerBottle = new Item("beer-bottle", "An empty beer bottle",
            "There is an empty beer bottle in a beer here.");
        nameToItem.Add("beer-bottle", beerBottle);
        
        Item apple = new Item("apple",
            "a shiny rotten apple",
            "A shiny rotten apple sits on the floor.");
        nameToItem.Add("apple", apple);
        
        Item spear = new Item("spear",
            "a shiny rotten spear",
            "A shiny rotten spear sits is propped in the corner.");
        nameToItem.Add("spear", spear);

        Item puke = new Item("puke",
            "some puke",
            "A disgusting pile of puke.");
        nameToItem.Add("puke", puke);
        
        // Place the sword in Verdent Grasslands for the quest
        Map.AddItem(sword.Name, "Verdent Grasslands");
        Map.AddItem(apple.Name, "Entrance");
        Map.AddItem(spear.Name, "Entrance");
        Map.AddItem(donut.Name, "Storage");
        Map.AddItem(beer.Name, "Throne Room");
    }

    public static Item GetItemByName(string itemName)
    {
        if (nameToItem.ContainsKey(itemName))
        {
            return nameToItem[itemName];
        }
        return null;
    }
}