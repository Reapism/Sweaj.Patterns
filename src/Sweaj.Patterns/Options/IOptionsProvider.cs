namespace Sweaj.Patterns.Options
{
    public interface IOptionsProvider<TOptions>
        where TOptions : new()
    {
        public TOptions Options { get; set; }
    }
}