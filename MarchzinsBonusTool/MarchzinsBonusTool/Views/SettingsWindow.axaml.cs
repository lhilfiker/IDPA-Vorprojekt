using Avalonia.Controls;
using Avalonia.Interactivity;
using MarchzinsBonusTool.Infrastructure;
using MarchzinsBonusTool.ViewModels;
using System;

namespace MarchzinsBonusTool.Views
{
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();
            
            Loaded += OnLoaded;
        }

        private void OnLoaded(object? sender, RoutedEventArgs e)
        {
            // Subscribe to RequestClose event when DataContext is set
            if (DataContext is SettingsViewModel vm)
            {
                void OnRequestClose(object? s, EventArgs args)
                {
                    vm.RequestClose -= OnRequestClose;
                    Close();
                }
                
                vm.RequestClose += OnRequestClose;
            }
            
            UpdateLocalizedTexts();
        }

        private void UpdateLocalizedTexts()
        {
            Title = Localization.Get("Settings");
        }

        protected override void OnClosed(EventArgs e)
        {
            
            base.OnClosed(e);
        }
    }
}