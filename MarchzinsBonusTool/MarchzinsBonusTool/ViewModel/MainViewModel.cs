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
        
        // Display fields
        private string displayBonusPeriode = string.Empty;
        private string displayBruttoZinsenNormal = string.Empty;
        private string displayBruttoZinsenBonus = string.Empty;
        private string displayBruttoZinsenTotal = string.Empty;
        private string displaySteuerabzug = string.Empty;
        private string displayNettoZinsen = string.Empty;
        private string errorMessage = string.Empty;
        private bool hasResults = false;

        // Commands
        public ICommand CalculateCommand { get; }
        public ICommand ResetCommand { get; }
        public ICommand ClearCommand { get; }

        public MainViewModel()
        {
        }

        #region Properties

        public string SparkapitalInput
        {
        }

        public DateTime GeburtsdatumInput
        {
        }

        public string KundenNameInput
        {
        }

        public string NormalerZinssatzInput
        {
        }

        public string BonusZinssatzInput
        {
        }

        public string SteuersatzInput
        {
        }

        public string DisplayBonusPeriode
        {
        }

        public string DisplayBruttoZinsenNormal
        {
        }

        public string DisplayBruttoZinsenBonus
        {
        }

        public string DisplayBruttoZinsenTotal
        {
        }

        public string DisplaySteuerabzug
        {
        }

        public string DisplayNettoZinsen
        {
        }

        public string ErrorMessage
        {
        }

        public bool HasErrors
        {
        }
    }

        public bool HasResults
        {
        }

        public Settings Settings => settings;

        #endregion

        #region Methods

        public void Calculate()
        {
        }

        public void LoadDefaults()
        {
        }

        public void ClearAllInputs()
        {
        }

        private void ClearResults()
        {
        }

        private bool CanCalculate()
        {
        }

        private void ValidateInput()
        {
        }

        private string FormatForInput(decimal value)
        {
        }

        public void RefreshSettings()
        {
        }

        #endregion
    }
}