using System;
using System.Globalization;
using System.Windows.Input;
using MarchzinsBonusTool.Business;
using MarchzinsBonusTool.Commands;
using MarchzinsBonusTool.Infrastructure;

namespace MarchzinsBonusTool.ViewModels
{
    /// <summary>
    /// Main ViewModel for the calculator window.
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private NumberFormatter formatter;
        private readonly InputValidator validator;
        private Settings settings;
        
        // Input fields
        private string sparkapitalInput = string.Empty;
        private DateTime geburtsdatumInput = DateTime.Now;
        private string kundenNameInput = string.Empty;
        private string normalerZinssatzInput = string.Empty;
        private string bonusZinssatzInput = string.Empty;
        private string steuersatzInput = string.Empty;
        private string selectedCurrency = "CHF";
        
        // Display fields
        private string displayBonusPeriode = string.Empty;
        private string displayBruttoZinsenNormal = string.Empty;
        private string displayBruttoZinsenBonus = string.Empty;
        private string displayBruttoZinsenTotal = string.Empty;
        private string displaySteuerabzug = string.Empty;
        private string displayNettoZinsen = string.Empty;
        private string errorMessage = string.Empty;
        private bool hasResults = false;
        
        // Additional display fields for new UI
        private string currentDateDisplay = string.Empty;
        private string formattedSparkapital = string.Empty;
        private string formattedGeburtsdatum = string.Empty;
        private string zinssatzDisplay = string.Empty;
        private string normalPeriodDisplay = string.Empty;
        private string bonusPeriodDisplay = string.Empty;
        private string normalCalculationDisplay = string.Empty;
        private string bonusCalculationDisplay = string.Empty;
        private string calculationTimestamp = string.Empty;
        private string sparkapitalError = string.Empty;

        // Commands
        public ICommand CalculateCommand { get; }
        public ICommand ResetCommand { get; }
        public ICommand ClearCommand { get; }
        public ICommand NewCalculationCommand { get; }
        public ICommand PrintCommand { get; }
        public ICommand ExportCommand { get; }

        public MainViewModel()
        {
            settings = Settings.Load();
            formatter = new NumberFormatter(settings);
            validator = new InputValidator();

            // Initialize commands
            CalculateCommand = new RelayCommand(_ => Calculate(), _ => CanCalculate());
            ResetCommand = new RelayCommand(_ => LoadDefaults());
            ClearCommand = new RelayCommand(_ => ClearAllInputs());
            NewCalculationCommand = new RelayCommand(_ => StartNewCalculation());
            PrintCommand = new RelayCommand(_ => Print());
            ExportCommand = new RelayCommand(_ => Export());

            // Set current date display
            UpdateCurrentDateDisplay();
            
            // Load default values on startup
            LoadDefaults();
        }

        #region Properties

        public string SparkapitalInput
        {
            get => sparkapitalInput;
            set
            {
                if (SetProperty(ref sparkapitalInput, value))
                {
                    ValidateInput();
                    ((RelayCommand)CalculateCommand).RaiseCanExecuteChanged();
                }
            }
        }

        public DateTime GeburtsdatumInput
        {
            get => geburtsdatumInput;
            set
            {
                if (SetProperty(ref geburtsdatumInput, value))
                {
                    ValidateInput();
                    ((RelayCommand)CalculateCommand).RaiseCanExecuteChanged();
                }
            }
        }
        
        public DateTimeOffset GeburtsdatumOffset
        {
            get => new DateTimeOffset(geburtsdatumInput);
            set 
            {
                GeburtsdatumInput = value.DateTime;
            }
        }

        public string KundenNameInput
        {
            get => kundenNameInput;
            set => SetProperty(ref kundenNameInput, value);
        }

        public string NormalerZinssatzInput
        {
            get => normalerZinssatzInput;
            set
            {
                if (SetProperty(ref normalerZinssatzInput, value))
                {
                    ValidateInput();
                    ((RelayCommand)CalculateCommand).RaiseCanExecuteChanged();
                }
            }
        }

        public string BonusZinssatzInput
        {
            get => bonusZinssatzInput;
            set
            {
                if (SetProperty(ref bonusZinssatzInput, value))
                {
                    ValidateInput();
                    ((RelayCommand)CalculateCommand).RaiseCanExecuteChanged();
                }
            }
        }

        public string SteuersatzInput
        {
            get => steuersatzInput;
            set
            {
                if (SetProperty(ref steuersatzInput, value))
                {
                    ValidateInput();
                    ((RelayCommand)CalculateCommand).RaiseCanExecuteChanged();
                }
            }
        }

        public string SelectedCurrency
        {
            get => selectedCurrency;
            set => SetProperty(ref selectedCurrency, value);
        }

        public string CurrentDateDisplay
        {
            get => currentDateDisplay;
            private set => SetProperty(ref currentDateDisplay, value);
        }

        public string DisplayBonusPeriode
        {
            get => displayBonusPeriode;
            private set => SetProperty(ref displayBonusPeriode, value);
        }

        public string DisplayBruttoZinsenNormal
        {
            get => displayBruttoZinsenNormal;
            private set => SetProperty(ref displayBruttoZinsenNormal, value);
        }

        public string DisplayBruttoZinsenBonus
        {
            get => displayBruttoZinsenBonus;
            private set => SetProperty(ref displayBruttoZinsenBonus, value);
        }

        public string DisplayBruttoZinsenTotal
        {
            get => displayBruttoZinsenTotal;
            private set => SetProperty(ref displayBruttoZinsenTotal, value);
        }

        public string DisplaySteuerabzug
        {
            get => displaySteuerabzug;
            private set => SetProperty(ref displaySteuerabzug, value);
        }

        public string DisplayNettoZinsen
        {
            get => displayNettoZinsen;
            private set => SetProperty(ref displayNettoZinsen, value);
        }

        public string ErrorMessage
        {
            get => errorMessage;
            private set => SetProperty(ref errorMessage, value);
        }

        public bool HasErrors => !string.IsNullOrEmpty(ErrorMessage);

        public bool HasResults
        {
            get => hasResults;
            private set => SetProperty(ref hasResults, value);
        }

        public Settings Settings => settings;

        // New properties for updated UI
        public string FormattedSparkapital
        {
            get => formattedSparkapital;
            private set => SetProperty(ref formattedSparkapital, value);
        }

        public string FormattedGeburtsdatum
        {
            get => formattedGeburtsdatum;
            private set => SetProperty(ref formattedGeburtsdatum, value);
        }

        public string ZinssatzDisplay
        {
            get => zinssatzDisplay;
            private set => SetProperty(ref zinssatzDisplay, value);
        }

        public string NormalPeriodDisplay
        {
            get => normalPeriodDisplay;
            private set => SetProperty(ref normalPeriodDisplay, value);
        }

        public string BonusPeriodDisplay
        {
            get => bonusPeriodDisplay;
            private set => SetProperty(ref bonusPeriodDisplay, value);
        }

        public string NormalCalculationDisplay
        {
            get => normalCalculationDisplay;
            private set => SetProperty(ref normalCalculationDisplay, value);
        }

        public string BonusCalculationDisplay
        {
            get => bonusCalculationDisplay;
            private set => SetProperty(ref bonusCalculationDisplay, value);
        }

        public string CalculationTimestamp
        {
            get => calculationTimestamp;
            private set => SetProperty(ref calculationTimestamp, value);
        }

        public string SparkapitalError
        {
            get => sparkapitalError;
            private set => SetProperty(ref sparkapitalError, value);
        }

        #endregion

        #region Methods

        public void Calculate()
        {
            try
            {
                // Clear any previous error
                ErrorMessage = string.Empty;
                SparkapitalError = string.Empty;

                // Parse inputs
                decimal sparkapital = formatter.ParseInput(SparkapitalInput);
                decimal normalerZinssatz = formatter.ParseInput(NormalerZinssatzInput);
                decimal bonusZinssatz = formatter.ParseInput(BonusZinssatzInput);
                decimal steuersatz = formatter.ParseInput(SteuersatzInput);

                // Create calculator
                var calculator = new MarchzinsCalculator(
                    sparkapital,
                    GeburtsdatumInput,
                    normalerZinssatz,
                    bonusZinssatz,
                    steuersatz,
                    DateTime.Now,
                    KundenNameInput);

                // Get results
                var results = calculator.GetAllResults();

                // Format and display results
                int bonusTage = (int)results["BonusPeriodeTage"];
                DisplayBonusPeriode = formatter.FormatInteger(bonusTage);
                DisplayBruttoZinsenNormal = formatter.FormatCurrency((decimal)results["BruttoZinsenNormal"]);
                DisplayBruttoZinsenBonus = formatter.FormatCurrency((decimal)results["BruttoZinsenBonus"]);
                DisplayBruttoZinsenTotal = formatter.FormatCurrency((decimal)results["BruttoZinsenTotal"]);
                DisplaySteuerabzug = formatter.FormatCurrency((decimal)results["Steuerabzug"]);
                DisplayNettoZinsen = formatter.FormatCurrency((decimal)results["NettoZinsen"]);

                // Set additional display fields
                FormattedSparkapital = formatter.FormatCurrency(sparkapital);
                FormattedGeburtsdatum = GeburtsdatumInput.ToString("dd.MM.yyyy");
                ZinssatzDisplay = $"{normalerZinssatz:F2}% / {bonusZinssatz:F2}% (Bonus)";
                
                int totalDays = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
                int normalDays = totalDays - bonusTage;
                
                NormalPeriodDisplay = $"{normalDays} Tage × {normalerZinssatz:F2}%";
                BonusPeriodDisplay = $"{bonusTage} Tage × {bonusZinssatz:F2}%";
                
                NormalCalculationDisplay = $"{FormattedSparkapital} × {normalerZinssatz/100:F4} × {normalDays}/365 = {DisplayBruttoZinsenNormal}";
                BonusCalculationDisplay = $"{FormattedSparkapital} × {bonusZinssatz/100:F4} × {bonusTage}/365 = {DisplayBruttoZinsenBonus}";
                
                CalculationTimestamp = $"Berechnet am {DateTime.Now:dd.MM.yyyy} um {DateTime.Now:HH:mm} Uhr";

                HasResults = true;
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Fehler bei der Berechnung: {ex.Message}";
                HasResults = false;
            }
        }

        public void LoadDefaults()
        {
            settings = Settings.Load();
            formatter = new NumberFormatter(settings);
            var defaults = settings.Defaults;

            SparkapitalInput = FormatForInput(defaults.Sparkapital);
            NormalerZinssatzInput = FormatForInput(defaults.NormalerZinssatz);
            BonusZinssatzInput = FormatForInput(defaults.BonusZinssatz);
            SteuersatzInput = FormatForInput(defaults.Steuersatz);
            
            // Set birth date to the 15th of current month
            var now = DateTime.Now;
            GeburtsdatumInput = new DateTime(now.Year, now.Month, Math.Min(15, DateTime.DaysInMonth(now.Year, now.Month)));
            
            KundenNameInput = string.Empty;
            SelectedCurrency = settings.Currency;
            UpdateCurrentDateDisplay();
            ClearResults();
        }

        public void ClearAllInputs()
        {
            SparkapitalInput = string.Empty;
            NormalerZinssatzInput = string.Empty;
            BonusZinssatzInput = string.Empty;
            SteuersatzInput = string.Empty;
            KundenNameInput = string.Empty;
            GeburtsdatumInput = DateTime.Now;
            UpdateCurrentDateDisplay();
            ClearResults();
        }

        private void ClearResults()
        {
            DisplayBonusPeriode = string.Empty;
            DisplayBruttoZinsenNormal = string.Empty;
            DisplayBruttoZinsenBonus = string.Empty;
            DisplayBruttoZinsenTotal = string.Empty;
            DisplaySteuerabzug = string.Empty;
            DisplayNettoZinsen = string.Empty;
            FormattedSparkapital = string.Empty;
            FormattedGeburtsdatum = string.Empty;
            ZinssatzDisplay = string.Empty;
            NormalPeriodDisplay = string.Empty;
            BonusPeriodDisplay = string.Empty;
            NormalCalculationDisplay = string.Empty;
            BonusCalculationDisplay = string.Empty;
            CalculationTimestamp = string.Empty;
            HasResults = false;
        }

        private void StartNewCalculation()
        {
            ClearResults();
            HasResults = false;
        }

        private void Print()
        {
            // Placeholder for print functionality
            // In a real application, this would trigger a print dialog
        }

        private void Export()
        {
            // Placeholder for export functionality
            // In a real application, this would export to PDF or Excel
        }

        private bool CanCalculate()
        {
            return validator.IsValidCalculation(this);
        }

        private void ValidateInput()
        {
            // Clear error message when starting validation
            ErrorMessage = string.Empty;
            SparkapitalError = string.Empty;

            // Validate each field
            if (!string.IsNullOrWhiteSpace(SparkapitalInput))
            {
                var result = validator.ValidateSparkapital(SparkapitalInput);
                if (!result.IsValid)
                {
                    ErrorMessage = result.ErrorMessage;
                    return;
                }
            }

            if (!string.IsNullOrWhiteSpace(SteuersatzInput))
            {
                var result = validator.ValidateSteuersatz(SteuersatzInput);
                if (!result.IsValid)
                {
                    ErrorMessage = result.ErrorMessage;
                    return;
                }
            }

            var dateResult = validator.ValidateDate(GeburtsdatumInput);
            if (!dateResult.IsValid)
            {
                ErrorMessage = dateResult.ErrorMessage;
            }
        }

        private string FormatForInput(decimal value)
        {
            // Format the value for display in input field
            return value.ToString("F2", CultureInfo.InvariantCulture);
        }

        private void UpdateCurrentDateDisplay()
        {
            CurrentDateDisplay = DateTime.Now.ToString("dd.MM.yyyy");
        }

        public void RefreshSettings()
        {
            settings = Settings.Load();
            formatter = new NumberFormatter(settings);
            SelectedCurrency = settings.Currency;
            UpdateCurrentDateDisplay();
            OnPropertyChanged(nameof(Settings));
        }

        #endregion
    }
}