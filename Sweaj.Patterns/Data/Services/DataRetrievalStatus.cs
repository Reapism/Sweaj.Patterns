namespace Sweaj.Patterns.Data.Services
{
    public enum DataRetrievalStatus : byte
    {
        Empty = 0,
        Database = 1,
        Cache = 2,
        File = 3,
        WebResource = 4,
    }
}
