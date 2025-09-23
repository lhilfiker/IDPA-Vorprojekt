using System;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Headless.XUnit;
using Xunit;
using MarchzinsBonusTool.Views;
using MarchzinsBonusTool.ViewModels;

namespace MarchzinsBonusTool.Tests.GUI
{
    public class MainWindowGUITests
    {
        [AvaloniaFact]
        public async Task MainWindow_LoadsSuccessfully()
        {
            var window = new MainWindow();
            
            Assert.NotNull(window);
            Assert.NotNull(window.DataContext);
            Assert.IsType<MainViewModel>(window.DataContext);
        }

        [AvaloniaFact]
        public async Task SparkapitalInput_UpdatesViewModel()
        {
            var window = new MainWindow();
            var viewModel = window.DataContext as MainViewModel;
            var sparkapitalTextBox = window.FindControl<TextBox>("SparkapitalTextBox");
            
            sparkapitalTextBox.Text = "50000";
            sparkapitalTextBox.RaiseEvent(new Avalonia.Input.TextInputEventArgs
            {
                RoutedEvent = TextBox.TextChangedEvent
            });
            
            await Task.Delay(100);
            
            Assert.Equal("50000", viewModel.SparkapitalInput);
        }

        [AvaloniaFact]
        public async Task CalculateButton_IsEnabledWithValidInput()
        {
            var window = new MainWindow();
            var viewModel = window.DataContext as MainViewModel;
            var calculateButton = window.FindControl<Button>("CalculateButton");
            
            // Set valid inputs
            viewModel.SparkapitalInput = "10000";
            viewModel.NormalerZinssatzInput = "2.5";
            viewModel.BonusZinssatzInput = "4.0";
            viewModel.SteuersatzInput = "35";
            viewModel.GeburtsdatumInput = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 15);
            
            await Task.Delay(100);
            
            Assert.True(calculateButton.IsEnabled);
        }

        [AvaloniaFact]
        public async Task CalculateButton_ExecutesCalculation()
        {
            var window = new MainWindow();
            var viewModel = window.DataContext as MainViewModel;
            var calculateButton = window.FindControl<Button>("CalculateButton");
            
            // Set valid inputs
            viewModel.SparkapitalInput = "10000";
            viewModel.NormalerZinssatzInput = "2.0";
            viewModel.BonusZinssatzInput = "4.0";
            viewModel.SteuersatzInput = "35";
            viewModel.GeburtsdatumInput = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 15);
            
            calculateButton.Command.Execute(null);
            await Task.Delay(100);
            
            Assert.True(viewModel.HasResults);
            Assert.NotEmpty(viewModel.DisplayNettoZinsen);
        }

        [AvaloniaFact]
        public async Task ErrorDisplay_ShowsWhenInvalidInput()
        {
            var window = new MainWindow();
            var viewModel = window.DataContext as MainViewModel;
            
            viewModel.SparkapitalInput = "invalid";
            await Task.Delay(100);
            
            Assert.True(viewModel.HasErrors);
            Assert.NotEmpty(viewModel.ErrorMessage);
        }

        [AvaloniaFact]
        public async Task ResetButton_ResetsToDefaults()
        {
            var window = new MainWindow();
            var viewModel = window.DataContext as MainViewModel;
            var resetButton = window.FindControl<Button>("ResetButton");
            
            viewModel.SparkapitalInput = "999";
            viewModel.KundenNameInput = "Test";
            
            resetButton.Command.Execute(null);
            await Task.Delay(100);
            
            Assert.NotEqual("999", viewModel.SparkapitalInput);
            Assert.Empty(viewModel.KundenNameInput);
        }

        [AvaloniaFact]
        public async Task LanguageButtons_ChangeLanguage()
        {
            var window = new MainWindow();
            var deButton = window.FindControl<Button>("LanguageDE");
            var enButton = window.FindControl<Button>("LanguageEN");
            var headerTitle = window.FindControl<TextBlock>("HeaderTitle");
            
            // Act - Click English button
            enButton.RaiseEvent(new Avalonia.Interactivity.RoutedEventArgs(Button.ClickEvent));
            await Task.Delay(100);
            
            Assert.Contains("March", headerTitle.Text);
            
            // Act - Click German button
            deButton.RaiseEvent(new Avalonia.Interactivity.RoutedEventArgs(Button.ClickEvent));
            await Task.Delay(100);
            
            Assert.Contains("Marchzins", headerTitle.Text);
        }

        [AvaloniaFact]
        public async Task NewCalculationButton_ClearsResults()
        {
            var window = new MainWindow();
            var viewModel = window.DataContext as MainViewModel;
            
            // First do a calculation
            viewModel.SparkapitalInput = "10000";
            viewModel.NormalerZinssatzInput = "2.0";
            viewModel.BonusZinssatzInput = "4.0";
            viewModel.SteuersatzInput = "35";
            viewModel.GeburtsdatumInput = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 15);
            viewModel.Calculate();
            
            Assert.True(viewModel.HasResults);
            
            var newCalculationButton = window.FindControl<Button>("NewCalculationButton");
            
            newCalculationButton.Command.Execute(null);
            await Task.Delay(100);
            
            Assert.False(viewModel.HasResults);
        }
    }
}