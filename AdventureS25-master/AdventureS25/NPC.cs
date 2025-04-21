namespace AdventureS25;

public class NPC
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string AsciiArt { get; private set; }

    public NPC(string name, string description, string asciiArt)
    {
        Name = name;
        Description = description;
        AsciiArt = asciiArt;
        ExplorationCommandValidator.AddNoun(name);
    }

    public string GetLocationDescription()
    {
        return $"{AsciiArt}\n{Name} - {Description}";
    }
}
