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
                var quest = Player.Quests.FirstOrDefault(q => q.Name == "Find the Lost Sword");
                if (quest == null)
                {
                    quest = new Quest("Find the Lost Sword", "Jon has asked you to find his lost sword in the Verdent Grasslands and bring it back to him.");
                    Player.AddQuest(quest);
                    Console.WriteLine("\nJon: Please find my lost sword in the Verdent Grasslands and bring it to me!");
                }
                else if (!quest.IsCompleted)
                {
                    var sword = Items.GetItemByName("sword");
                    if (Player.Inventory.Contains(sword))
                    {
                        quest.IsCompleted = true;
                        Player.RemoveItemFromInventory("sword");
                        Console.WriteLine("\nJon: You found my sword! Thank you so much!");
                        Console.WriteLine("Quest completed! You can reward one of your pals with XP.");
                        var pal = Player.PromptSelectPal("Which pal should receive 20 XP as a quest reward?");
                        if (pal != null)
                        {
                            pal.GainXP(20);
                        }
                    }
                    else
                    {
                        Console.WriteLine("\nJon: Please bring me my lost sword from the Verdent Grasslands!");
                    }
                }
                else
                {
                    Console.WriteLine("\nJon: Thank you again for returning my sword!");
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
