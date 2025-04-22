namespace AdventureS25;

public class Pal
{
    public int XP { get; private set; }
    public int Level { get; private set; }
    public int XPToNextLevel { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string AsciiArt { get; private set; }

    // Battle stats
    public int MaxHP { get; private set; }
    public int CurrentHP { get; set; }
    public int Attack { get; private set; }
    public int Defense { get; set; }
    public int Special { get; private set; }

    public Pal(string name, string asciiArt, string description, int maxHP = 30, int attack = 8, int defense = 5, int special = 12)
    {
        Name = name;
        Description = description;
        AsciiArt = asciiArt;
        MaxHP = maxHP;
        CurrentHP = maxHP;
        Attack = attack;
        Defense = defense;
        Special = special;
        XP = 0;
        Level = 1;
        XPToNextLevel = 25;
        ExplorationCommandValidator.AddNoun(name);
    }

    public void GainXP(int amount)
    {
        XP += amount;
        TextPrinter.Print($"\n{Name} gained {amount} XP!");
        while (XP >= XPToNextLevel)
        {
            XP -= XPToNextLevel;
            LevelUp();
        }
    }

    private void LevelUp()
    {
        Level++;
        XPToNextLevel = (int)(XPToNextLevel * 1.5 + 10); // Increase XP needed for next level
        MaxHP += 5;
        Attack += 2;
        Defense += 2;
        Special += 2;
        CurrentHP = MaxHP;
        TextPrinter.Print($"{Name} leveled up! Now level {Level}!");
        TextPrinter.Print($"Stats increased: HP {MaxHP}, Attack {Attack}, Defense {Defense}, Special {Special}");
        TextPrinter.Print($"XP: {XP}/{XPToNextLevel}");
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

    public void PrintDescription()
    {
        Console.WriteLine($"{AsciiArt}\n");
        TextPrinter.Print($"Name: {Name}");
        TextPrinter.Print($"Description: {Description}");
        TextPrinter.Print($"Level: {Level}");
        TextPrinter.Print($"HP: {CurrentHP}/{MaxHP}");
        TextPrinter.Print($"Attack: {Attack}");
        TextPrinter.Print($"Defense: {Defense}");
        TextPrinter.Print($"Special: {Special}");
        TextPrinter.Print($"XP: {XP}/{XPToNextLevel}");
    }
}
