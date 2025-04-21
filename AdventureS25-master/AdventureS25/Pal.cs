namespace AdventureS25;

public class Pal
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string Dialogue { get; private set; }
    public string AsciiArt { get; private set; }

    public Pal(string name, string asciiArt, string description, string dialogue)
    {
        Name = name;
        Description = description;
        Dialogue = dialogue;
        AsciiArt = asciiArt;
        ExplorationCommandValidator.AddNoun(name);
    }

    public string GetLocationDescription()
    {
        return $"{AsciiArt}\n{Name} - {Description}";
    }
}
