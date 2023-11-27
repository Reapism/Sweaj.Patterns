namespace Sweaj.Patterns.Data.Values
{
    public enum ValueResultStatus : byte
    {
        Empty = 0,
        Value = 1,
        Cache = 2,
        DataStore = 3,
        WebResource = 4,
        ValueFactoryFromCache = 5,
        ValueFactoryFromDataStore = 6,
        File = 7
    }
}
