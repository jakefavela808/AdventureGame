using System.Collections.Generic;

namespace AdventureS25;

public static class Pals
{
    private static Dictionary<string, Pal> nameToPal = new Dictionary<string, Pal>();

    public static void Initialize()
    {
        Pal sparky = new Pal(
            "Sparky",
            AsciiArt.sandiePal,
            "A lively electric Pal, always ready for a battle!",
            "Sparky: Ready to battle? Let's see what you've got!" );
        nameToPal.Add("Sparky", sparky);

        Pal aqua = new Pal(
            "Aqua",
            AsciiArt.gloopGlorpPal,
            "A calm water Pal, but fierce in battle.",
            "Aqua: The tides will turn in my favor!" );
        nameToPal.Add("Aqua", aqua);

        // Place Pals in locations
        Map.AddPal(sparky, "Verdent Grasslands");
        Map.AddPal(aqua, "Verdent Grasslands");
    }

    public static void BattlePal(Command command)
    {
        var pals = Player.CurrentLocation.Pals;
        if (pals.Count == 1)
        {
            var pal = pals[0];
            Console.Clear();
            States.ChangeState(StateTypes.Fighting);
            Console.WriteLine(CommandList.combatCommands);
            Console.WriteLine(pal.AsciiArt);
            Console.WriteLine($"{pal.Description}");
            Console.WriteLine($"{pal.Dialogue}");
        }
        else if (pals.Count == 0)
        {
            Console.WriteLine("There is no pal here to battle.");
        }
        else
        {
            Console.WriteLine("There is more than one pal here. Be more specific.");
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
