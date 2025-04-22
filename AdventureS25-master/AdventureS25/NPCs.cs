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
            "A local professor who studies Pals and their habitats. Seems to be perpetually on the brink of a coding breakthrough... or a mental breakdown.",
            $"Jon: Welcome to my lab, {Player.Name}! You ever *burp* wonder how Pals evolve? Yeah, it's wild science!" );
        nameToNPC.Add("Professor Jon", professorJon);

        NPC nurseNoelia = new NPC(
            "Nurse Noelia",
            AsciiArt.nurseCharacter,
            "A kind nurse who takes care of injured Pals at the Pal Center.",
            $"Noelia: Please take care of your Pals, {Player.Name}, and let me know if any are hurt!" );
        nameToNPC.Add("Nurse Noelia", nurseNoelia);

        NPC matt = new NPC(
            "Matt",
            AsciiArt.mattCharacter,
            "A wise man who lives by the river. He often chats with travelers.",
            $"Matt: Thank you for visiting me, {Player.Name}." );
        nameToNPC.Add("Matt", matt);

        // Place NPCs in locations
        Map.AddNPC(professorJon, "Professor Jon's Lab");
        Map.AddNPC(nurseNoelia, "Pal Center");
        Map.AddNPC(matt , "Riverside Cabin");
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
            AsciiArt.Print(npc.AsciiArt);
            TextPrinter.Print($"{npc.Description}");

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
                    TextPrinter.Print($"\nJon: *burp* Not bad, kid! You actually caught a Pal. Here's a reward, don't spend it all in one place, {Player.Name}!");
                    Player.GiveReward(30); // Give XP reward for catching a Pal
                    if (!Player.Quests.Any(q => q.Name == "Visit the Nurse"))
                    {
                        Quest nurseQuest = new Quest("Visit the Nurse", $"Visit Nurse Noelia at the Pal Center to heal your Pals, {Player.Name}.");
                        TextPrinter.Print($"\nJon: Listen, {Player.Name}, you gotta go see Nurse Noelia at the Pal Center. Get your Pals healed up, science waits for no one!");
                        Console.Clear();
                        States.ChangeState(StateTypes.Exploring);
                        Player.Look();
                        Player.AddQuest(nurseQuest);
                    }
                    else
                    {
                        TextPrinter.Print($"\nJon: Ugh, you already got the quest, {Player.Name}! Go see Noelia, stop wasting my time!");
                    }
                    return;
                }
                // If the player hasn't met Jon, complete that quest, give Sandie, and add the battle quest
                var meetJonQuest = Player.Quests.FirstOrDefault(q => q.Name == "Meet Professor Jon");
                if (meetJonQuest != null && !meetJonQuest.IsCompleted)
                {
                    meetJonQuest.IsCompleted = true;
                    Console.WriteLine("Quest completed!");
                    var Sandie = Player.OwnedPals.FirstOrDefault(p => p.Name == "Sandie");
                    if (Sandie == null)
                    {
                        Sandie = new Pal("Sandie", AsciiArt.sandiePal, "A cheerful grass pal.", 30, 8, 7, 12);
                        TextPrinter.Print($"\nJon: *burp* P-P-Perfect timing, {Player.Name}! I've been coding all night, barely slept, and I just brought Sandie to life! This Pal is, uh, one of a kind, trust me!");
                        TextPrinter.Print($"Jon: Here take Sandie, {Player.Name}! I'm *burp* t-trusting you with her.\n");
                        Sandie.PrintDescription();
                        Player.AddPalToCollection(Sandie);
                    }
                    else
                    {
                        TextPrinter.Print($"\nJon: Oh, you're back, {Player.Name}. You already got Sandie, don't get greedy!");
                    }
                    TextPrinter.Print($"\nJon: Alright, {Player.Name}, go test your battle skills! Find a wild Pal, tame it, and, uh, try not to die!");
                    Quest battleQuest = new Quest("Test Your Battle Skills", $"Find and tame a wild pal in the grasslands, {Player.Name}.");
                    Console.Clear();
                    States.ChangeState(StateTypes.Exploring);
                    Player.Look();
                    Player.AddQuest(battleQuest);
                    return;
                }
                // If 'Meet Professor Jon' quest is completed but player hasn't caught a Pal
                if (meetJonQuest != null && meetJonQuest.IsCompleted && !Conditions.IsTrue(ConditionTypes.HasCaughtPal))
                {
                    TextPrinter.Print($"Jon: *burp* Go catch a Pal and come back, {Player.Name}! Don't waste my time until you do your damn quest!");
                    Console.Clear();
                    States.ChangeState(StateTypes.Exploring);
                    Player.Look();
                    return;
                }
                // Otherwise, default dialogue
                TextPrinter.Print(npc.Dialogue);
                return;
            }
            else if (npc.Name == "Nurse Noelia")
            {
                var nurseQuest = Player.Quests.FirstOrDefault(q => q.Name == "Visit the Nurse");
                var deliverQuest = Player.Quests.FirstOrDefault(q => q.Name == "Deliver Medicine");

                // If nurse quest is active and not completed
                if (nurseQuest != null && !nurseQuest.IsCompleted)
                {
                    bool anyHurt = Player.OwnedPals.Any(pal => pal.CurrentHP < pal.MaxHP);
                    if (anyHurt)
                    {
                        foreach (var pal in Player.OwnedPals)
                            pal.CurrentHP = pal.MaxHP;
                        nurseQuest.IsCompleted = true;
                        TextPrinter.Print("\nNurse Noelia: Your Pals are fully healed! Thank you for coming to see me.");
                    }
                    else
                    {
                        nurseQuest.IsCompleted = true;
                        TextPrinter.Print("\nNurse Noelia: All your Pals are already fully healed! But I'm glad you stopped by.");
                    }
                    // Trigger new quest after healing
                    if (!Player.Quests.Any(q => q.Name == "Deliver Medicine"))
                    {
                        Quest newDeliver = new Quest("Deliver Medicine", $"Deliver Nurse Noelia's medicine to Matt in the Riverside Cabin, {Player.Name}.");
                        TextPrinter.Print($"Nurse Noelia: {Player.Name}, could you do me a favor? Please deliver this medicine to Matt in the Riverside Cabin. He's been feeling unwell lately. Here, take this bottle of medicine.");
                        Player.AddQuest(newDeliver);
                        Player.AddItemToInventory("medicine");
                        TextPrinter.Print("You received the medicine from Nurse Noelia.");
                    }
                    else
                    {
                        TextPrinter.Print($"Nurse Noelia: {Player.Name}, don't forget to deliver the medicine to Matt in the Riverside Cabin!");
                    }
                    States.ChangeState(StateTypes.Exploring);
                    Player.Look();
                    return;
                }
                // If deliver quest is active and not completed, keep existing logic
                else if (deliverQuest != null && !deliverQuest.IsCompleted)
                {
                    TextPrinter.Print($"Nurse Noelia: {Player.Name}, please deliver the medicine to Matt in the Riverside Cabin as soon as you can.");
                    return;
                }
                // If nurse quest not active, just heal/check pals, do not affect quest logic
                else
                {
                    bool anyHurt = Player.OwnedPals.Any(pal => pal.CurrentHP < pal.MaxHP);
                    if (anyHurt)
                    {
                        foreach (var pal in Player.OwnedPals)
                            pal.CurrentHP = pal.MaxHP;
                        TextPrinter.Print("\nNurse Noelia: Your Pals are fully healed! Come see me anytime they're hurt.");
                    }
                    else
                    {
                        TextPrinter.Print("\nNurse Noelia: All your Pals are already fully healed!");
                    }
                    Console.Clear();
                    States.ChangeState(StateTypes.Exploring);
                    Player.Look();
                    return;
                }
            }
            else if (npc.Name == "Matt")
            {
                var deliverQuest = Player.Quests.FirstOrDefault(q => q.Name == "Deliver Medicine");
                if (deliverQuest != null && !deliverQuest.IsCompleted)
                {
                    var medicine = Items.GetItemByName("medicine");
                    if (Player.Inventory.Contains(medicine))
                    {
                        Player.RemoveItemFromInventory("medicine");
                        deliverQuest.IsCompleted = true;
                        TextPrinter.Print("\nMatt: Oh! Nurse Noelia sent this? Thank you so much, young one. Please tell her I am grateful! Here, take this as a token of my appreciation.");
                        Player.GiveReward(20);
                    }
                    else
                    {
                        TextPrinter.Print("\nMatt: Do you have the medicine from Nurse Noelia? Please bring it to me if you have it.");
                        return;
                    }
                }
                else if (deliverQuest != null && deliverQuest.IsCompleted)
                {
                    TextPrinter.Print($"\nMatt: Thank you again for delivering the medicine, {Player.Name}. I feel much better!");
                    Console.Clear();
                }
                else
                {
                    TextPrinter.Print(npc.Dialogue);
                }
                Console.Clear();
                States.ChangeState(StateTypes.Exploring);
                Player.Look();
                return;
            }
        }
        else if (npcs.Count == 0)
        {
            TextPrinter.Print("There is no one here to talk to.");
            Console.Clear();
            Player.Look();
        }
        else
        {
            TextPrinter.Print("There is more than one person here. Be more specific.");
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
