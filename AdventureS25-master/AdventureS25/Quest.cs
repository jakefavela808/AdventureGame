namespace AdventureS25;

public class Quest
{
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsCompleted { get; set; }

    public Quest(string name, string description)
    {
        Name = name;
        Description = description;
        IsCompleted = false;
    }

    public override string ToString()
    {
        return $"{Name}: {Description} {(IsCompleted ? "(Completed)" : "")}";
    }
}
