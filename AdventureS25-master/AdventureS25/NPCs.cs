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

            // If Jon, give a sample quest
            if (npc.Name == "Professor Jon")
            {
                // If the player has caught a Pal, always advance the questline
                if (Conditions.IsTrue(ConditionTypes.HasCaughtPal))
                {
                    // Ensure 'Test Your Battle Skills' quest exists and is completed
                    var battleQuest = Player.Quests.FirstOrDefault(q => q.Name == "Test Your Battle Skills");
                    if (battleQuest == null) {
                        battleQuest = new Quest("Test Your Battle Skills", "Find and tame a wild pal in the grasslands.");
                        Player.AddQuest(battleQuest);
                    }
                    if (!battleQuest.IsCompleted)
                    {
                        battleQuest.IsCompleted = true;
                    }
                    Console.WriteLine("\nProfessor Jon: Impressive! You caught your first Pal! Here's a reward for your achievement.");
                    Player.GiveReward(30); // Give XP reward for catching a Pal
                    if (!Player.Quests.Any(q => q.Name == "Visit the Nurse"))
                    {
                        Quest nurseQuest = new Quest("Visit the Nurse", "Visit Nurse Noelia at the Pal Center to heal your Pals.");
                        Player.AddQuest(nurseQuest);
                        Console.WriteLine("\nProfessor Jon: Now, go visit Nurse Noelia at the Pal Center to get your Pals healed up!");
                    }
                    else
                    {
                        Console.WriteLine("\nProfessor Jon: You already have the quest to visit Nurse Noelia. Go see her if you haven't yet.");
                    }
                    return;
                }
                // If the player hasn't met Jon, complete that quest, give Sandie, and add the battle quest
                var meetJonQuest = Player.Quests.FirstOrDefault(q => q.Name == "Meet Professor Jon");
                if (meetJonQuest != null && !meetJonQuest.IsCompleted)
                {
                    meetJonQuest.IsCompleted = true;
                    Player.GiveReward(20);
                    var Sandie = Player.OwnedPals.FirstOrDefault(p => p.Name == "Sandie");
                    if (Sandie == null)
                    {
                        Sandie = new Pal("Sandie", AsciiArt.sandiePal, "A cheerful grass pal.", "Let's grow together!", 30, 8, 7, 12);
                        Player.AddPalToCollection(Sandie);
                        Console.WriteLine("\nJon: Welcome! Here, take this pal, Sandie, as your companion.");
                    }
                    else
                    {
                        Console.WriteLine("\nJon: Welcome back! You already have your pal, Sandie.");
                    }
                    Console.WriteLine("Jon: Now, test your battle skills by finding and taming a wild pal!");
                    Quest battleQuest = new Quest("Test Your Battle Skills", "Find and tame a wild pal in the grasslands.");
                    Player.AddQuest(battleQuest);
                    Console.Clear();
                    States.ChangeState(StateTypes.Exploring);
                    Player.Look();
                    return;
                }
                // Otherwise, default dialogue
                Console.WriteLine(npc.Dialogue);
                return;
            }
            else if (npc.Name == "Nurse Noelia")
            {
                var nurseQuest = Player.Quests.FirstOrDefault(q => q.Name == "Visit the Nurse");
                if (nurseQuest != null && !nurseQuest.IsCompleted)
                {
                    // Heal all player's pals
                    foreach (var pal in Player.OwnedPals)
                    {
                        pal.CurrentHP = pal.MaxHP;
                    }
                    nurseQuest.IsCompleted = true;
                    Console.WriteLine("\nNurse Noelia: Your Pals are fully healed! Good luck on your adventure.");
                }
                else
                {
                    Console.WriteLine("\nNurse Noelia: Please take care of your Pals, and let me know if any are hurt!");
                }
                States.ChangeState(StateTypes.Exploring);
                Player.Look();
                return;
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
