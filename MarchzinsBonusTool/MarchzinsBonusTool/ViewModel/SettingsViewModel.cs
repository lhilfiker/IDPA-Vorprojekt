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
            settings = Settings.Load();
            
            SaveCommand = new RelayCommand(_ => Save());
            ResetCommand = new RelayCommand(_ => ResetToDefaults());
            CancelCommand = new RelayCommand(_ => Cancel());

            LoadFromSettings();
        }

        #region Properties

        public Language SelectedLanguage
        {
            get => selectedLanguage;
            set => SetProperty(ref selectedLanguage, value);
        }

        public string ThousandsSeparator
        {
            get => thousandsSeparator;
            set => SetProperty(ref thousandsSeparator, value);
        }

        public string DecimalSeparator
        {
            get => decimalSeparator;
            set => SetProperty(ref decimalSeparator, value);
        }

        public string Currency
        {
            get => currency;
            set => SetProperty(ref currency, value);
        }

        public string DefaultSparkapital
        {
            get => defaultSparkapital;
            set => SetProperty(ref defaultSparkapital, value);
        }

        public string DefaultNormalerZinssatz
        {
            get => defaultNormalerZinssatz;
            set => SetProperty(ref defaultNormalerZinssatz, value);
        }

        public string DefaultBonusZinssatz
        {
            get => defaultBonusZinssatz;
            set => SetProperty(ref defaultBonusZinssatz, value);
        }

        public string DefaultSteuersatz
        {
            get => defaultSteuersatz;
            set => SetProperty(ref defaultSteuersatz, value);
        }

        public Array Languages => Enum.GetValues(typeof(Language));

        #endregion

        #region Methods

        private void LoadFromSettings()
        {
            SelectedLanguage = settings.Language;
            ThousandsSeparator = settings.ThousandsSeparator.ToString();
            DecimalSeparator = settings.DecimalSeparator.ToString();
            Currency = settings.Currency;
            DefaultSparkapital = settings.Defaults.Sparkapital.ToString();
            DefaultNormalerZinssatz = settings.Defaults.NormalerZinssatz.ToString();
            DefaultBonusZinssatz = settings.Defaults.BonusZinssatz.ToString();
            DefaultSteuersatz = settings.Defaults.Steuersatz.ToString();
        }

        private void Save()
        {
            // Update settings from UI
            settings.Language = SelectedLanguage;
            
            if (!string.IsNullOrEmpty(ThousandsSeparator))
                settings.ThousandsSeparator = ThousandsSeparator[0];
            
            if (!string.IsNullOrEmpty(DecimalSeparator))
                settings.DecimalSeparator = DecimalSeparator[0];
            
            settings.Currency = Currency;

            // Parse and save default values
            if (decimal.TryParse(DefaultSparkapital, out decimal sparkapital))
                settings.Defaults.Sparkapital = sparkapital;
            
            if (decimal.TryParse(DefaultNormalerZinssatz, out decimal normalZins))
                settings.Defaults.NormalerZinssatz = normalZins;
            
            if (decimal.TryParse(DefaultBonusZinssatz, out decimal bonusZins))
                settings.Defaults.BonusZinssatz = bonusZins;
            
            if (decimal.TryParse(DefaultSteuersatz, out decimal steuer))
                settings.Defaults.Steuersatz = steuer;

            // Apply language change
            Localization.SetLanguage(SelectedLanguage);

            // Save to disk
            settings.Save();

            // Close window
            RequestClose?.Invoke(this, EventArgs.Empty);
        }

        private void ResetToDefaults()
        {
            settings.ResetToDefaults();
            LoadFromSettings();
        }

        private void Cancel()
        {
            RequestClose?.Invoke(this, EventArgs.Empty);
        }

        #endregion
    }
}