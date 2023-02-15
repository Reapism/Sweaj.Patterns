using System.Globalization;

namespace Sweaj.Patterns.Localization
{
    public interface ILocalizationService
    {
        string GetLocalizedString(string key);
        CultureInfo GetCurrentCulture();
        void SetCulture(CultureInfo culture);
    }
}
