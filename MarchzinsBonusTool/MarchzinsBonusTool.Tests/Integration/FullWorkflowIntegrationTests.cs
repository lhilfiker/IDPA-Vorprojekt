using System;
using System.IO;
using System.Threading.Tasks;
using Avalonia.Headless.XUnit;
using Xunit;
using MarchzinsBonusTool.Views;
using MarchzinsBonusTool.ViewModels;
using MarchzinsBonusTool.Infrastructure;
using MarchzinsBonusTool.Business;

namespace MarchzinsBonusTool.Tests.Integration
{
    public class FullWorkflowIntegrationTests : IDisposable
    {
        private readonly string testSettingsPath;

        public FullWorkflowIntegrationTests()
        {
            // Setup test environment
            var testDir = Path.Combine(Path.GetTempPath(), "MarchzinsBonusToolIntegrationTests", Guid.NewGuid().ToString());
            Directory.CreateDirectory(testDir);
            testSettingsPath = testDir;
        }

        [AvaloniaFact]
        public async Task FullWorkflow_InputToResult_WorksCorrectly()
        {
            var window = new MainWindow();
            var viewModel = window.DataContext as MainViewModel;

            //Complete workflow
            // 1. Load defaults
            viewModel.LoadDefaults();
            Assert.NotEmpty(viewModel.SparkapitalInput);

            // 2. Modify inputs
            viewModel.SparkapitalInput = "25000";
            viewModel.KundenNameInput = "Hans Muster";
            viewModel.NormalerZinssatzInput = "1.5";
            viewModel.BonusZinssatzInput = "3.0";
            viewModel.SteuersatzInput = "35";
            viewModel.GeburtsdatumInput = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 10);

            // 3. Calculate
            viewModel.Calculate();

            Assert.True(viewModel.HasResults);
            Assert.NotEmpty(viewModel.DisplayNettoZinsen);
            Assert.NotEmpty(viewModel.DisplayBruttoZinsenTotal);
            Assert.NotEmpty(viewModel.FormattedSparkapital);
            Assert.Contains("Hans Muster", viewModel.FormattedGeburtsdatum ?? "");

            // 4. Start new calculation
            viewModel.NewCalculationCommand.Execute(null);
            Assert.False(viewModel.HasResults);

            // 5. Verify inputs are still there (not cleared)
            Assert.Equal("25000", viewModel.SparkapitalInput);
        }

        [AvaloniaFact]
        public async Task SettingsChange_AffectsCalculation_Correctly()
        {
            var window = new MainWindow();
            var viewModel = window.DataContext as MainViewModel;

            viewModel.SparkapitalInput = "10000";
            viewModel.NormalerZinssatzInput = "2.0";
            viewModel.BonusZinssatzInput = "4.0";
            viewModel.SteuersatzInput = "35";
            viewModel.GeburtsdatumInput = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 15);

            var settings = Settings.Load();
            var originalCurrency = settings.Currency;
            settings.Currency = "EUR";
            settings.Save();

            viewModel.RefreshSettings();

            viewModel.Calculate();

            Assert.True(viewModel.HasResults);
            Assert.Equal("EUR", viewModel.SelectedCurrency);

            settings.Currency = originalCurrency;
            settings.Save();
        }

        [AvaloniaFact]
        public async Task LanguageChange_AffectsUI_Immediately()
        {
            var window = new MainWindow();
            
            Localization.SetLanguage(Language.German);
            var germanTitle = Localization.Get("MainTitle");

            Localization.SetLanguage(Language.English);
            var englishTitle = Localization.Get("MainTitle");

            Assert.NotEqual(germanTitle, englishTitle);
            Assert.Contains("March", englishTitle);
            Assert.Contains("Marchzins", germanTitle);

            Localization.SetLanguage(Language.German);
        }

        [Fact]
        public void ValidationToCalculation_Integration_WorksCorrectly()
        {
            var validator = new InputValidator();
            
            string sparkapital = "15000.50";
            string normalZins = "1.8";
            string bonusZins = "3.2";
            string steuer = "35";
            DateTime geburtsdatum = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 12);

            var sparkapitalResult = validator.ValidateSparkapital(sparkapital);
            var normalZinsResult = validator.ValidateZinssatz(normalZins, "Normal");
            var bonusZinsResult = validator.ValidateZinssatz(bonusZins, "Bonus");
            var steuerResult = validator.ValidateSteuersatz(steuer);
            var dateResult = validator.ValidateDate(geburtsdatum);

            Assert.True(sparkapitalResult.IsValid);
            Assert.True(normalZinsResult.IsValid);
            Assert.True(bonusZinsResult.IsValid);
            Assert.True(steuerResult.IsValid);
            Assert.True(dateResult.IsValid);

            decimal parsedSparkapital = decimal.Parse(sparkapital.Replace(",", "."), System.Globalization.CultureInfo.InvariantCulture);
            decimal parsedNormalZins = decimal.Parse(normalZins.Replace(",", "."), System.Globalization.CultureInfo.InvariantCulture);
            decimal parsedBonusZins = decimal.Parse(bonusZins.Replace(",", "."), System.Globalization.CultureInfo.InvariantCulture);
            decimal parsedSteuer = decimal.Parse(steuer.Replace(",", "."), System.Globalization.CultureInfo.InvariantCulture);

            var calculator = new MarchzinsCalculator(
                parsedSparkapital,
                geburtsdatum,
                parsedNormalZins,
                parsedBonusZins,
                parsedSteuer,
                DateTime.Now,
                "Integration Test");

            var results = calculator.GetAllResults();

            Assert.NotNull(results);
            Assert.True((decimal)results["NettoZinsen"] >= 0);
            Assert.Equal("Integration Test", results["KundenName"]);
        }

        [Fact]
        public void ErrorHandling_Integration_WorksCorrectly()
        {
            var viewModel = new MainViewModel();

            viewModel.SparkapitalInput = "-1000";  // Invalid: negative
            viewModel.NormalerZinssatzInput = "abc";  // Invalid: not a number
            viewModel.SteuersatzInput = "150";  // Invalid: > 100%

            bool canCalculate = viewModel.CalculateCommand.CanExecute(null);
            Assert.False(canCalculate);

            // Error message should be set
            Assert.True(viewModel.HasErrors);
        }

        [Fact]
        public void Settings_Persistence_Integration_Works()
        {
            var settings = new Settings();
            settings.Language = Language.English;
            settings.Currency = "USD";
            settings.ThousandsSeparator = ',';
            settings.DecimalSeparator = '.';

            settings.Save();

            var loadedSettings = Settings.Load();

            Assert.Equal(Language.English, loadedSettings.Language);
            Assert.Equal("USD", loadedSettings.Currency);
            Assert.Equal(',', loadedSettings.ThousandsSeparator);
            Assert.Equal('.', loadedSettings.DecimalSeparator);
        }

        [Fact]
        public void BoundaryConditions_Integration_WorksCorrectly()
        {
            var viewModel = new MainViewModel();

            viewModel.SparkapitalInput = "0.01";  // Minimum valid
            viewModel.NormalerZinssatzInput = "0";  // Minimum valid
            viewModel.BonusZinssatzInput = "100";  // Maximum valid
            viewModel.SteuersatzInput = "0";  // Minimum valid
            viewModel.GeburtsdatumInput = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);  // First day of month

            viewModel.Calculate();

            Assert.True(viewModel.HasResults);
            Assert.NotEmpty(viewModel.DisplayNettoZinsen);
        }

        public void Dispose()
        {
            // Cleanup test directory
            try
            {
                if (Directory.Exists(testSettingsPath))
                {
                    Directory.Delete(testSettingsPath, true);
                }
            }
            catch
            {
                // Ignore cleanup errors
            }
        }
    }
}