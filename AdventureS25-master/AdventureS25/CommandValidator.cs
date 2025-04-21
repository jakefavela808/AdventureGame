namespace AdventureS25;

public static class CommandValidator
{
    public static bool IsValid(Command command)
    {
        if (States.CurrentStateType == StateTypes.Exploring)
        {
            return ExplorationCommandValidator.IsValid(command);
        }
        else if (States.CurrentStateType == StateTypes.Fighting)
        {
            return CombatCommandValidator.IsValid(command);
        }
        else if (States.CurrentStateType == StateTypes.Talking)
        {
            return ConversationCommandValidator.IsValid(command);
        }
        
        return false;
    }
}