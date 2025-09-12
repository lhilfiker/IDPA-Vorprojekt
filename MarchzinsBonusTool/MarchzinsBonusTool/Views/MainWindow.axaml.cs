using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Input;
using MarchzinsBonusTool.Infrastructure;
using MarchzinsBonusTool.ViewModels;
using System;
using System.Threading.Tasks;

namespace MarchzinsBonusTool.Views
{
    public partial class MainWindow : Window
    {
        private MainViewModel? viewModel;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = viewModel = new MainViewModel();
            
            Loaded += OnLoaded;
        }

        private void OnLoaded(object? sender, RoutedEventArgs e)
        {
            UpdateLocalizedTexts();
            UpdateLanguageButtons();
        }

        private void UpdateLocalizedTexts()
        {
            Title = Localization.Get("MainTitle");
            // TODO: all translations            
        }

        private void UpdateLanguageButtons()
        {
            var settings = Settings.Load();
            var deButton = this.FindControl<Button>("LanguageDE");
            var enButton = this.FindControl<Button>("LanguageEN");
            
            if (deButton != null && enButton != null)
            {
                if (settings.Language == Language.German)
                {
                    deButton.Background = Avalonia.Media.Brushes.White;
                    deButton.Foreground = Avalonia.Media.Brush.Parse("#6B8FFF");
                    enButton.Background = Avalonia.Media.Brushes.Transparent;
                    enButton.Foreground = Avalonia.Media.Brushes.White;
                }
                else
                {
                    enButton.Background = Avalonia.Media.Brushes.White;
                    enButton.Foreground = Avalonia.Media.Brush.Parse("#6B8FFF");
                    deButton.Background = Avalonia.Media.Brushes.Transparent;
                    deButton.Foreground = Avalonia.Media.Brushes.White;
                }
            }
        }

        private void OnLanguageDEClick(object? sender, RoutedEventArgs e)
        {
            var settings = Settings.Load();
            settings.Language = Language.German;
            settings.Save();
            Localization.SetLanguage(Language.German);
            UpdateLocalizedTexts();
            UpdateLanguageButtons();
            viewModel?.RefreshSettings();
        }

        private void OnLanguageENClick(object? sender, RoutedEventArgs e)
        {
            var settings = Settings.Load();
            settings.Language = Language.English;
            settings.Save();
            Localization.SetLanguage(Language.English);
            UpdateLocalizedTexts();
            UpdateLanguageButtons();
            viewModel?.RefreshSettings();
        }

        private async void OnSettingsClick(object? sender, RoutedEventArgs e)
        {
            var settingsWindow = new SettingsWindow();
            var settingsViewModel = new SettingsViewModel();
            settingsWindow.DataContext = settingsViewModel;

            // Subscribe to the RequestClose event
            void OnRequestClose(object? s, EventArgs args)
            {
                settingsViewModel.RequestClose -= OnRequestClose;
                settingsWindow.Close();
            }
            
            settingsViewModel.RequestClose += OnRequestClose;

            await settingsWindow.ShowDialog(this);
            
            // Refresh settings and UI after settings window closes
            viewModel?.RefreshSettings();
            UpdateLocalizedTexts();
            UpdateLanguageButtons();
        }
    }
}