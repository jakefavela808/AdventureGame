namespace AdventureS25;

public static class SemanticTools
{
    public static string CreateArticle(string word)
    {
        word = word.ToLower();
        if (word.StartsWith("a") || 
            word.StartsWith("e") || 
            word.StartsWith("i") || 
            word.StartsWith("o") || 
            word.StartsWith("u"))
        {
            return "an";
        }

        return "a";
    }
}