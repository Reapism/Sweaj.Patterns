namespace Sweaj.Patterns.Data.Services
{
    public enum DataRetrievalStatus : byte
    {
        Database = 0,
        EmptyFromDatabase = 1,
        Cache = 2,
        EmptyFromCache = 3,
        File = 4,
        EmptyFromFile = 5,
        WebResource = 6,
        EmptyFromWebResource = 7,
    }
}
