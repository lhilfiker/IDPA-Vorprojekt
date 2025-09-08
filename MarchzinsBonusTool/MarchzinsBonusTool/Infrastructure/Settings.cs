using System;
using System.IO;
using Newtonsoft.Json;

namespace MarchzinsBonusTool.Infrastructure
{
    /// <summary>
    /// Application settings that are persisted to disk.
    /// </summary>
    public class Settings
    {
        private static readonly string SettingsPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "MarchzinsBonusTool",
            "settings.json");

        public Language Language { get; set; } = Language.German;
        public char ThousandsSeparator { get; set; } = '\'';
        public char DecimalSeparator { get; set; } = '.';
        public string Currency { get; set; } = "CHF";
        public DefaultValues Defaults { get; set; } = new DefaultValues();

        /// <summary>
        /// Saves the current settings to disk.
        /// </summary>
        public void Save()
        {
        }

        /// <summary>
        /// Loads settings from disk or returns default settings.
        /// </summary>
        public static Settings Load()
        {
        }

        /// <summary>
        /// Resets all settings to their default values.
        /// </summary>
        public void ResetToDefaults()
        {
        }
    }
}