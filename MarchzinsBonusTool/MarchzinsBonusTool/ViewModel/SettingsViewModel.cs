using System;
using System.Windows.Input;
using MarchzinsBonusTool.Commands;
using MarchzinsBonusTool.Infrastructure;

namespace MarchzinsBonusTool.ViewModels
{
    /// <summary>
    /// ViewModel for the settings window.
    /// </summary>
    public class SettingsViewModel : ViewModelBase
    {
        private Settings settings;
        private Language selectedLanguage;
        private string thousandsSeparator = string.Empty;
        private string decimalSeparator = string.Empty;
        private string currency = string.Empty;
        private string defaultSparkapital = string.Empty;
        private string defaultNormalerZinssatz = string.Empty;
        private string defaultBonusZinssatz = string.Empty;
        private string defaultSteuersatz = string.Empty;

        public ICommand SaveCommand { get; }
        public ICommand ResetCommand { get; }
        public ICommand CancelCommand { get; }

        public event EventHandler? RequestClose;

        public SettingsViewModel()
        {
        }

        #region Properties

        public Language SelectedLanguage
        {
        }

        public string ThousandsSeparator
        {
        }

        public string DecimalSeparator
        {
        }

        public string Currency
        {
        }

        public string DefaultSparkapital
        {
        }

        public string DefaultNormalerZinssatz
        {
        }

        public string DefaultBonusZinssatz
        {
        }

        public string DefaultSteuersatz
        {
        }

        public Array Languages => Enum.GetValues(typeof(Language));

        #endregion

        #region Methods

        private void LoadFromSettings()
        {
        }

        private void Save()
        {
        }

        private void ResetToDefaults()
        {
        }

        private void Cancel()
        {
        }

        #endregion
    }
}