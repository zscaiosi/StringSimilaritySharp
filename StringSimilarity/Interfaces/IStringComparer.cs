namespace StringSimilarity.Interfaces
{
    public interface IStringComparer
    {
        decimal CompareTwoStrings(string a, string b);
        bool AreStringsValid(params string[] p);
    }
}