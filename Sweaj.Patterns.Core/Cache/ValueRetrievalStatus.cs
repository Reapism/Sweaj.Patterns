namespace Sweaj.Patterns.Cache
{
    public enum ValueRetrievalMethod
    {
        Undefined = 0,
        Cache = 1,
        DataStore = 2,
        OnlyCache = 3,
    }
    public enum ValueRetrievalStatus
    {
        Undefined = 0,
        Empty = 1,
        Cache = 2,
        DataStore = 3,
    }
}
