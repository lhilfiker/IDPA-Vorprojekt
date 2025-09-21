using Xunit;
using MarchzinsBonusTool.Infrastructure;

namespace MarchzinsBonusTool.Tests.Infrastructure
{
    public class LocalizationTests
    {
        public LocalizationTests()
        {
            // Reset to German before each test to ensure consistent starting state
            Localization.SetLanguage(Language.German);
        }

        [Fact]
        public void Get_MainTitle_ReturnsCorrectGermanTranslation()
        {
            Localization.SetLanguage(Language.German);
            string result = Localization.Get("MainTitle");
            Assert.Equal("Marchzins-Bonus Tool", result);
        }

        [Fact]
        public void Get_MainTitle_ReturnsCorrectEnglishTranslation()
        {
            Localization.SetLanguage(Language.English);
            string result = Localization.Get("MainTitle");
            Assert.Equal("March Interest Bonus Tool", result);
        }

        [Fact]
        public void Get_WithInvalidKey_ReturnsKeyItself()
        {
            string invalidKey = "NonExistentKey";
            string result = Localization.Get(invalidKey);
            Assert.Equal(invalidKey, result);
        }

        [Fact]
        public void SetLanguage_ChangesSubsequentTranslations()
        {
            Localization.SetLanguage(Language.German);
            string germanTitle = Localization.Get("MainTitle");
            Localization.SetLanguage(Language.English);
            string englishTitle = Localization.Get("MainTitle");
            Assert.Equal("Marchzins-Bonus Tool", germanTitle);
            Assert.Equal("March Interest Bonus Tool", englishTitle);
            Assert.NotEqual(germanTitle, englishTitle);
        }
    }
}