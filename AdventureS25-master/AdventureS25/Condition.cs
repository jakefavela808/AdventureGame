namespace AdventureS25;

public class Condition
{
    public ConditionTypes Type;
    public bool IsActive;
    
    private List<Action> methodsToCallOnActivate = new List<Action>();
    private List<Action> methodsToCallOnDeactivate = new List<Action>();

    public Condition(ConditionTypes type)
    {
        Type = type;
        IsActive = false;
    }

    public void Activate()
    {
        IsActive = true;
        foreach (Action action in methodsToCallOnActivate)
        {
            action.Invoke();
        }
    }

    public void Deactivate()
    {
        IsActive = false;
        foreach (Action action in methodsToCallOnDeactivate)
        {
            action.Invoke();
        }
    }

    public void AddToActivateList(Action action)
    {
        methodsToCallOnActivate.Add(action);
    }

    public void AddToDeactivateList(Action action)
    {
        methodsToCallOnDeactivate.Add(action);
    }
}