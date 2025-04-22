using System.Collections.Generic;

namespace AdventureS25;

public static class Pals
{
    private static Dictionary<string, Pal> nameToPal = new Dictionary<string, Pal>();

    public static void Initialize()
    {
        Pal gloopGlorp = new Pal(
            "Gloop Glorp",
            AsciiArt.gloopGlorpPal,
            "A lively electric Pal, always ready for a battle!",
            32, 9, 6, 14);
        nameToPal.Add("Gloop Glorp", gloopGlorp);

        Pal clydeCapybara = new Pal(
            "Clyde Capybara",
            AsciiArt.clydeCapybaraPal,
            "A calm water Pal, but fierce in battle.",
            28, 7, 8, 15);
        nameToPal.Add("Clyde Capybara", clydeCapybara);

        Pal pibble = new Pal(
            "Pibble",
            AsciiArt.Pibble,
            "A calm water Pal, but fierce in battle.",
            28, 7, 8, 15);
        nameToPal.Add("Pibble", pibble);

        Pal morty = new Pal(
            "Morty",
            AsciiArt.mortyPal,
            "A calm water Pal, but fierce in battle.",
            28, 7, 8, 15);
        nameToPal.Add("Morty", morty);


        // Place Pals in locations
        Map.AddPal(gloopGlorp, "Verdent Grasslands");
        Map.AddPal(clydeCapybara, "Verdent Grasslands");

        // Ensure only one pal per location (dynamic, no hardcoding)
        var rand = new System.Random();
        foreach (var location in Map.Locations)
        {
            if (location.Pals.Count > 1)
            {
                int keepIdx = rand.Next(location.Pals.Count);
                var palToKeep = location.Pals[keepIdx];
                location.Pals.Clear();
                location.Pals.Add(palToKeep);
            }
        }
    }

    public static void BattlePal(Command command)
    {
        var pals = Player.CurrentLocation.Pals;
        if (pals.Count == 1)
        {
            var pal = pals[0];
            CombatCommandHandler.StartBattle(pal);
        }
        else if (pals.Count == 0)
        {
            TextPrinter.Print("There is no pal here to battle.");
        }
        else
        {
            TextPrinter.Print("There is more than one pal here. Be more specific.");
        }
    }

    public static Pal GetPalByName(string palName)
    {
        if (nameToPal.ContainsKey(palName))
        {
            return nameToPal[palName];
        }
        return null;
    }
}
