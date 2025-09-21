using System;
using System.IO;
using Xunit;
using MarchzinsBonusTool.Infrastructure;

namespace MarchzinsBonusTool.Tests.Infrastructure
{
    public class SettingsTests : IDisposable
    {
        private readonly string testSettingsPath;

        public SettingsTests()
        {
            // Create a unique test directory to avoid conflicts
            var testDir = Path.Combine(Path.GetTempPath(), "MarchzinsBonusToolTests", Guid.NewGuid().ToString());
            Directory.CreateDirectory(testDir);
        }

        [Fact]
        public void ResetToDefaults_RestoresAllDefaults()
        {
            var settings = new Settings();
            
            // Modify some values
            settings.Language = Language.English;
            settings.ThousandsSeparator = ',';
            settings.DecimalSeparator = ';';
            settings.Currency = "EUR";
            
            // Reset to defaults
            settings.ResetToDefaults();
            
            // Verify defaults are restored
            Assert.Equal(Language.German, settings.Language);
            Assert.Equal('\'', settings.ThousandsSeparator);
            Assert.Equal('.', settings.DecimalSeparator);
            Assert.Equal("CHF", settings.Currency);
            Assert.NotNull(settings.Defaults);
        }

        [Fact]
        public void Validate_WithInvalidSeparators_CorrectsThem()
        {
            var settings = new Settings();
            
            // Set invalid configuration (same separators)
            settings.ThousandsSeparator = '.';
            settings.DecimalSeparator = '.';
            
            // Validate should correct this
            settings.Validate();
            
            Assert.Equal('\'', settings.ThousandsSeparator);
            Assert.Equal('.', settings.DecimalSeparator);
        }

        [Fact]
        public void Validate_WithNullDefaults_CreatesNewDefaults()
        {
            var settings = new Settings();
            settings.Defaults = null;
            
            settings.Validate();
            
            Assert.NotNull(settings.Defaults);
        }

        [Fact]
        public void Validate_WithEmptyCurrency_SetsDefaultCurrency()
        {
            var settings = new Settings();
            settings.Currency = "";
            
            settings.Validate();
            
            Assert.Equal("CHF", settings.Currency);
        }

        [Fact]
        public void Validate_WithNullCurrency_SetsDefaultCurrency()
        {
            var settings = new Settings();
            settings.Currency = null;
            
            settings.Validate();
            
            Assert.Equal("CHF", settings.Currency);
        }

        public void Dispose()
        {
            // Cleanup test directory
            try
            {
                var testDir = Path.GetDirectoryName(testSettingsPath);
                if (Directory.Exists(testDir))
                {
                    Directory.Delete(testDir, true);
                }
            }
            catch
            {
                // Ignore cleanup errors
            }
        }
    }
}