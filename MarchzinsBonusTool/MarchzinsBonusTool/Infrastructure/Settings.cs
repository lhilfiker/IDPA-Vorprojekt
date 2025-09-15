using System;
using System.IO;
using Newtonsoft.Json;

namespace MarchzinsBonusTool.Infrastructure
{
    /// <summary>
    /// Supported application languages.
    /// </summary>
    public enum Language
    {
        German = 0,
        English = 1
    }

    /// <summary>
    /// Default values for calculation inputs.
    /// These values are used to pre-populate the UI forms.
    /// </summary>
    public class DefaultValues
    {
        public decimal DefaultCapital { get; set; } = 10000m;
        public decimal DefaultNormalInterestRate { get; set; } = 1.5m;
        public decimal DefaultBonusInterestRate { get; set; } = 2.5m;
        public decimal DefaultTaxRate { get; set; } = 35m;

        /// <summary>
        /// Validates that all default values are within reasonable ranges.
        /// </summary>
        public void Validate()
        {
            if (DefaultCapital < 0)
                DefaultCapital = 10000m;

            if (DefaultNormalInterestRate < 0 || DefaultNormalInterestRate > 100)
                DefaultNormalInterestRate = 1.5m;

            if (DefaultBonusInterestRate < 0 || DefaultBonusInterestRate > 100)
                DefaultBonusInterestRate = 2.5m;

            if (DefaultTaxRate < 0 || DefaultTaxRate > 100)
                DefaultTaxRate = 35m;
        }
    }

    /// <summary>
    /// Application settings that are persisted to disk.
    /// Provides configuration management for user preferences, number formatting,
    /// language settings, and default calculation values.
    /// </summary>
    public class Settings
    {
        private static readonly string SettingsPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "MarchzinsBonusTool",
            "settings.json");

        public Infrastructure.Language Language { get; set; } = Infrastructure.Language.German;
        public char ThousandsSeparator { get; set; } = '\'';
        public char DecimalSeparator { get; set; } = '.';
        public string Currency { get; set; } = "CHF";
        public DefaultValues Defaults { get; set; } = new DefaultValues();

        /// <summary>
        /// Saves the current settings to disk.
        /// Creates the directory structure if it doesn't exist.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown when settings cannot be saved</exception>
        public void Save()
        {
            try
            {
                Validate();

                var directory = Path.GetDirectoryName(SettingsPath);
                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                var json = JsonConvert.SerializeObject(this, Formatting.Indented);
                File.WriteAllText(SettingsPath, json);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Fehler beim Speichern der Einstellungen: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Loads settings from disk or returns default settings.
        /// Automatically creates backup of corrupted files.
        /// </summary>
        /// <returns>Settings instance with loaded or default values</returns>
        public static Settings Load()
        {
            try
            {
                if (!File.Exists(SettingsPath))
                {
                    return new Settings();
                }

                var json = File.ReadAllText(SettingsPath);

                if (string.IsNullOrWhiteSpace(json))
                {
                    return new Settings();
                }

                var settings = JsonConvert.DeserializeObject<Settings>(json);

                if (settings == null)
                {
                    return new Settings();
                }

                settings.Validate();
                return settings;
            }
            catch (Exception)
            {
                CreateBackupOfCorruptedFile();
                return new Settings();
            }
        }

        /// <summary>
        /// Resets all settings to their default values.
        /// </summary>
        public void ResetToDefaults()
        {
            Language = Infrastructure.Language.German;
            ThousandsSeparator = '\'';
            DecimalSeparator = '.';
            Currency = "CHF";
            Defaults = new DefaultValues();
        }

        /// <summary>
        /// Validates the current settings and corrects invalid values.
        /// Ensures data consistency and prevents configuration errors.
        /// </summary>
        public void Validate()
        {
            if (ThousandsSeparator == DecimalSeparator)
            {
                ThousandsSeparator = '\'';
                DecimalSeparator = '.';
            }

            if (string.IsNullOrWhiteSpace(Currency))
            {
                Currency = "CHF";
            }

            if (Defaults == null)
            {
                Defaults = new DefaultValues();
            }

            if (!Enum.IsDefined(typeof(Infrastructure.Language), Language))
            {
                Language = Infrastructure.Language.German;
            }
        }

        /// <summary>
        /// Creates a backup of corrupted settings file for debugging.
        /// </summary>
        private static void CreateBackupOfCorruptedFile()
        {
            try
            {
                if (File.Exists(SettingsPath))
                {
                    var directory = Path.GetDirectoryName(SettingsPath);
                    if (!string.IsNullOrEmpty(directory))
                    {
                        var backupPath = SettingsPath + ".backup." + DateTime.Now.ToString("yyyyMMdd_HHmmss");
                        File.Copy(SettingsPath, backupPath);
                    }
                }
            }
            catch
            {
                // Ignore backup errors to prevent cascading failures
            }
        }
    }
}