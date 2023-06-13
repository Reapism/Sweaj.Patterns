namespace Sweaj.Patterns.Data
{
    public enum ValueResultStatus : byte
    {
        Empty = 0,
        Value = 1,
        Cache = 2,
        ValueFactoryFromCache = 3,
        DataStore = 4,
        ValueFactoryFromDataStore = 5,
        ThirdParty = 6
    }
}
