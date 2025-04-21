using System.Collections.Generic;

namespace AdventureS25;

public static class NPCs
{
    private static Dictionary<string, NPC> nameToNPC = new Dictionary<string, NPC>();

    public static void Initialize()
    {
        NPC professorJon = new NPC(
            "Professor Jon",
            "A quirky scientist with wild hair, always eager to talk about Pals.",
            "   \\O//\n    | |\n   /   \\");
        nameToNPC.Add("Professor Jon", professorJon);
        ExplorationCommandValidator.AddNoun("Professor Jon");
        ExplorationCommandValidator.AddNoun("professor jon");
        ExplorationCommandValidator.AddNoun("jon");

        NPC nurseJoy = new NPC(
            "Nurse Joy",
            "A kind nurse who takes care of injured Pals at the Pal Center.",
            "  ( ^_^ )\n  /|   |\\\n   |   | ");
        nameToNPC.Add("Nurse Joy", nurseJoy);
        ExplorationCommandValidator.AddNoun("Nurse Joy");
        ExplorationCommandValidator.AddNoun("nurse joy");
        ExplorationCommandValidator.AddNoun("joy");

        // Place NPCs in locations
        Map.AddNPC(professorJon, "Professor Jon's Lab");
        Map.AddNPC(nurseJoy, "Pal Center");
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
