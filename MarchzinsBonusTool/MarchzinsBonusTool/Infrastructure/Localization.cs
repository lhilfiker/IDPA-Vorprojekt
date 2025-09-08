using System.Collections.Generic;

namespace MarchzinsBonusTool.Infrastructure
{
    /// <summary>
    /// Handles localization of UI strings.
    /// </summary>
    public static class Localization
    {
        private static Language currentLanguage = Language.German;
        
        private static readonly Dictionary<string, Dictionary<Language, string>> translations = 
            new Dictionary<string, Dictionary<Language, string>>
        {
            ["MainTitle"] = new() { 
                [Language.German] = "Marchzins-Bonus Rechner",
                [Language.English] = "March Interest Bonus Calculator"
            }
        };

        /// <summary>
        /// Gets a localized string for the given key.
        /// </summary>
        public static string Get(string key)
        {
        }

        /// <summary>
        /// Sets the current language.
        /// </summary>
        public static void SetLanguage(Language language)
        {
        }
    }
}