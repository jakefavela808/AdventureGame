namespace AdventureS25;

public class Pal
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string Dialogue { get; private set; }
    public string AsciiArt { get; private set; }

    // Battle stats
    public int MaxHP { get; private set; }
    public int CurrentHP { get; set; }
    public int Attack { get; private set; }
    public int Defense { get; set; }
    public int Special { get; private set; }

    public Pal(string name, string asciiArt, string description, string dialogue, int maxHP = 30, int attack = 8, int defense = 5, int special = 12)
    {
        Name = name;
        Description = description;
        Dialogue = dialogue;
        AsciiArt = asciiArt;
        MaxHP = maxHP;
        CurrentHP = maxHP;
        Attack = attack;
        Defense = defense;
        Special = special;
        ExplorationCommandValidator.AddNoun(name);
    }

    public void ResetHP()
    {
        CurrentHP = MaxHP;
    }

    public void TakeDamage(int amount)
    {
        CurrentHP -= amount;
        if (CurrentHP < 0) CurrentHP = 0;
    }

    public bool IsFainted()
    {
        return CurrentHP <= 0;
    }

    public string GetLocationDescription()
    {
        return $"{AsciiArt}\n{Name} - {Description}";
    }
}
