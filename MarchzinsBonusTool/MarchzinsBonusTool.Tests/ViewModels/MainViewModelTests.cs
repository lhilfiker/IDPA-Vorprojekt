using System;
using Xunit;
using MarchzinsBonusTool.ViewModels;

namespace MarchzinsBonusTool.Tests.ViewModels
{
    public class MainViewModelTests
    {
        [Fact]
        public void Constructor_InitializesPropertiesCorrectly()
        {
            var viewModel = new MainViewModel();
            
            Assert.NotNull(viewModel.CalculateCommand);
            Assert.NotNull(viewModel.ResetCommand);
            Assert.NotNull(viewModel.ClearCommand);
            Assert.NotNull(viewModel.NewCalculationCommand);
            Assert.NotNull(viewModel.PrintCommand);
            Assert.NotNull(viewModel.ExportCommand);
            Assert.NotNull(viewModel.Settings);
            Assert.False(viewModel.HasResults);
            Assert.False(viewModel.HasErrors);
        }

        [Fact]
        public void SparkapitalInput_WhenChanged_UpdatesProperty()
        {
            var viewModel = new MainViewModel();
            string testValue = "1000.50";
            
            viewModel.SparkapitalInput = testValue;
            
            Assert.Equal(testValue, viewModel.SparkapitalInput);
        }

        [Fact]
        public void GeburtsdatumInput_WhenChanged_UpdatesProperty()
        {
            var viewModel = new MainViewModel();
            var testDate = new DateTime(2024, 3, 15);
            
            viewModel.GeburtsdatumInput = testDate;
            
            Assert.Equal(testDate, viewModel.GeburtsdatumInput);
        }

        [Fact]
        public void GeburtsdatumOffset_ReflectsGeburtsdatumInput()
        {
            var viewModel = new MainViewModel();
            var testDate = new DateTime(2024, 3, 15);
            
            viewModel.GeburtsdatumInput = testDate;
            
            Assert.Equal(testDate, viewModel.GeburtsdatumOffset.DateTime);
        }

        [Fact]
        public void GeburtsdatumOffset_WhenSet_UpdatesGeburtsdatumInput()
        {
            var viewModel = new MainViewModel();
            var testDate = new DateTime(2024, 3, 15);
            var offset = new DateTimeOffset(testDate);
            
            viewModel.GeburtsdatumOffset = offset;
            
            Assert.Equal(testDate, viewModel.GeburtsdatumInput);
        }

        [Fact]
        public void LoadDefaults_SetsExpectedValues()
        {
            var viewModel = new MainViewModel();
            
            viewModel.LoadDefaults();
            
            Assert.NotEmpty(viewModel.SparkapitalInput);
            Assert.NotEmpty(viewModel.NormalerZinssatzInput);
            Assert.NotEmpty(viewModel.BonusZinssatzInput);
            Assert.NotEmpty(viewModel.SteuersatzInput);
            Assert.Equal("CHF", viewModel.SelectedCurrency);
            Assert.False(viewModel.HasResults);
        }

        [Fact]
        public void LoadDefaults_SetsCorrectBirthDate()
        {
            var viewModel = new MainViewModel();
            var now = DateTime.Now;
            
            viewModel.LoadDefaults();
            
            Assert.Equal(now.Year, viewModel.GeburtsdatumInput.Year);
            Assert.Equal(now.Month, viewModel.GeburtsdatumInput.Month);
            // Should be 15th or last day of month if month has fewer than 15 days
            Assert.True(viewModel.GeburtsdatumInput.Day <= 15);
            Assert.True(viewModel.GeburtsdatumInput.Day <= DateTime.DaysInMonth(now.Year, now.Month));
        }

        [Fact]
        public void ClearAllInputs_ResetsAllFields()
        {
            var viewModel = new MainViewModel();
            
            // Set some values first
            viewModel.SparkapitalInput = "1000";
            viewModel.KundenNameInput = "Test";
            viewModel.NormalerZinssatzInput = "2.5";
            
            viewModel.ClearAllInputs();
            
            Assert.Empty(viewModel.SparkapitalInput);
            Assert.Empty(viewModel.KundenNameInput);
            Assert.Empty(viewModel.NormalerZinssatzInput);
            Assert.Empty(viewModel.BonusZinssatzInput);
            Assert.Empty(viewModel.SteuersatzInput);
            Assert.False(viewModel.HasResults);
        }

        [Fact]
        public void HasErrors_ReflectsErrorMessageState()
        {
            var viewModel = new MainViewModel();
            
            Assert.False(viewModel.HasErrors);
            
            // Set invalid input to trigger validation
            viewModel.SparkapitalInput = "invalid";
            
            // HasErrors should reflect whether there's an error message
            Assert.Equal(!string.IsNullOrEmpty(viewModel.ErrorMessage), viewModel.HasErrors);
        }

        [Fact]
        public void RefreshSettings_UpdatesCurrency()
        {
            var viewModel = new MainViewModel();
            var originalCurrency = viewModel.SelectedCurrency;
            
            viewModel.RefreshSettings();
            
            // Currency should be updated from settings
            Assert.NotNull(viewModel.SelectedCurrency);
        }

        [Fact]
        public void KundenNameInput_AcceptsNullAndEmpty()
        {
            var viewModel = new MainViewModel();
            
            viewModel.KundenNameInput = null;
            Assert.Null(viewModel.KundenNameInput);
            
            viewModel.KundenNameInput = "";
            Assert.Equal("", viewModel.KundenNameInput);
            
            viewModel.KundenNameInput = "Test Name";
            Assert.Equal("Test Name", viewModel.KundenNameInput);
        }

        [Fact]
        public void Calculate_WithValidInputs_SetsHasResultsTrue()
        {
            var viewModel = new MainViewModel();
            
            // Set valid inputs
            viewModel.SparkapitalInput = "10000";
            viewModel.NormalerZinssatzInput = "2.0";
            viewModel.BonusZinssatzInput = "4.0";
            viewModel.SteuersatzInput = "35";
            viewModel.GeburtsdatumInput = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 15);
            
            viewModel.Calculate();
            
            Assert.True(viewModel.HasResults);
            Assert.NotEmpty(viewModel.DisplayNettoZinsen);
        }

        [Fact]
        public void Calculate_WithInvalidInputs_DoesNotSetResults()
        {
            var viewModel = new MainViewModel();
            
            // Set invalid inputs
            viewModel.SparkapitalInput = "invalid";
            viewModel.NormalerZinssatzInput = "abc";
            
            viewModel.Calculate();
            
            // Should not have results due to invalid input
            Assert.False(viewModel.HasResults);
        }

        [Fact]
        public void NewCalculationCommand_ClearsResults()
        {
            var viewModel = new MainViewModel();
            
            // Set some results first
            viewModel.SparkapitalInput = "10000";
            viewModel.NormalerZinssatzInput = "2.0";
            viewModel.BonusZinssatzInput = "4.0";
            viewModel.SteuersatzInput = "35";
            viewModel.GeburtsdatumInput = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 15);
            viewModel.Calculate();
            
            Assert.True(viewModel.HasResults);
            
            // Execute new calculation command
            viewModel.NewCalculationCommand.Execute(null);
            
            Assert.False(viewModel.HasResults);
        }

        [Fact]
        public void CurrentDateDisplay_IsNotEmpty()
        {
            var viewModel = new MainViewModel();
            
            Assert.NotEmpty(viewModel.CurrentDateDisplay);
            
            // Should contain today's date in some format
            var today = DateTime.Now.ToString("dd.MM.yyyy");
            Assert.Equal(today, viewModel.CurrentDateDisplay);
        }
    }
}