namespace Sweaj.Patterns.Configuration
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public sealed class FromConfigurationAttribute : Attribute
    {
        public FromConfigurationAttribute(ConfigurationSourceType configurationSource, string configurationSourcePath)
        {
            ConfigurationSource = configurationSource;
            Source = configurationSourcePath;

        }

        public string Source { get; }
        public ConfigurationSourceType ConfigurationSource { get; }
    }
}
