using System.Collections.Generic;

namespace AdventureS25;

public static class NPCs
{
    private static Dictionary<string, NPC> nameToNPC = new Dictionary<string, NPC>();

    public static void Initialize()
    {
        NPC professorJon = new NPC(
            "Professor Jon",
            AsciiArt.jonCharacter,
            "A quirky scientist with wild hair, always eager to talk about Pals.",
            "Jon: Welcome to my lab! Have you ever wondered how Pals evolve?" );
        nameToNPC.Add("Professor Jon", professorJon);

        NPC nurseNoelia = new NPC(
            "Nurse Noelia",
            AsciiArt.nurseCharacter,
            "A kind nurse who takes care of injured Pals at the Pal Center.",
            "Noelia: Please take care of your Pals, and let me know if any are hurt!" );
        nameToNPC.Add("Nurse Noelia", nurseNoelia);

        // Place NPCs in locations
        Map.AddNPC(professorJon, "Professor Jon's Lab");
        Map.AddNPC(nurseNoelia, "Pal Center");
    }

    public static void TalkToNPC(Command command)
    {
        var npcs = Player.CurrentLocation.NPCs;
        if (npcs.Count == 1)
        {
            var npc = npcs[0];
            Console.Clear();
            States.ChangeState(StateTypes.Talking);
            Console.WriteLine(CommandList.conversationCommands);
            Console.WriteLine(npc.AsciiArt);
            Console.WriteLine($"{npc.Description}");
            Console.WriteLine(npc.Dialogue);

            // If Jon, give a sample quest
            if (npc.Name == "Professor Jon")
            {
                // Handle 'Meet Professor Jon' quest
                var meetJonQuest = Player.Quests.FirstOrDefault(q => q.Name == "Meet Professor Jon");
                if (meetJonQuest != null && !meetJonQuest.IsCompleted)
                {
                    meetJonQuest.IsCompleted = true;
                    // Give the player a new pal if they don't have it
                    var sproutle = Player.OwnedPals.FirstOrDefault(p => p.Name == "Sproutle");
                    if (sproutle == null)
                    {
                        sproutle = new Pal("Sproutle", AsciiArt.sandiePal, "A cheerful grass pal.", "Let's grow together!", 30, 8, 7, 12);
                        Player.AddPalToCollection(sproutle);
                        Console.WriteLine("\nJon: Welcome! Here, take this pal, Sproutle, as your companion.");
                    }
                    else
                    {
                        Console.WriteLine("\nJon: Welcome back! You already have your pal, Sproutle.");
                    }
                    Console.WriteLine("Jon: Now, test your battle skills by finding a wild pal to battle!");
                    // Optionally, add a new quest for battling
                    Quest battleQuest = new Quest("Test Your Battle Skills", "Find and battle a wild pal in the grasslands.");
                    Console.Clear();
                    States.ChangeState(StateTypes.Exploring);
                    Player.Look();
                    Player.AddQuest(battleQuest);
                    return;
                }
            }
        }
        else if (npcs.Count == 0)
        {
            Console.WriteLine("There is no one here to talk to.");
        }
        else
        {
            Console.WriteLine("There is more than one person here. Be more specific.");
        }
    }

    public static NPC GetNPCByName(string npcName)
    {
        if (nameToNPC.ContainsKey(npcName))
        {
            return nameToNPC[npcName];
        }
        return null;
    }
}
