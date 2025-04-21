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
            "A quirky scientist with wild hair, always eager to talk about Pals.");
        nameToNPC.Add("Professor Jon", professorJon);
        ExplorationCommandValidator.AddNoun("jon");

        NPC nurseNoelia = new NPC(
            "Nurse Noelia",
            AsciiArt.nurseCharacter,
            "A kind nurse who takes care of injured Pals at the Pal Center.");
        nameToNPC.Add("Nurse Noelia", nurseNoelia);
        ExplorationCommandValidator.AddNoun("noelia");

        // Place NPCs in locations
        Map.AddNPC(professorJon, "Professor Jon's Lab");
        Map.AddNPC(nurseNoelia, "Pal Center");
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
