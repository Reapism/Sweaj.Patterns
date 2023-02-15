using System.Globalization;

namespace Sweaj.Patterns.Localization
{
    public interface ILocalizedStringProvider
    {
        string GetString(string key, CultureInfo culture);
    }
}
