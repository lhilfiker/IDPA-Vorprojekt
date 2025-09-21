using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Input;
using MarchzinsBonusTool.Infrastructure;
using MarchzinsBonusTool.ViewModels;
using System;
using System.Threading.Tasks;
using Avalonia.Controls.Documents;

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
    
    // Header
    var headerTitle = this.FindControl<TextBlock>("HeaderTitle");
    if (headerTitle != null)
        headerTitle.Text = Localization.Get("MainTitle");
    
    // Input Section
    var customerDataTitle = this.FindControl<TextBlock>("CustomerDataTitle");
    if (customerDataTitle != null)
        customerDataTitle.Text = Localization.Get("CustomerData");
    
    var customerNameLabel = this.FindControl<TextBlock>("CustomerNameLabel");
    if (customerNameLabel != null)
        customerNameLabel.Text = Localization.Get("CustomerName");
    
    var customerNameTextBox = this.FindControl<TextBox>("CustomerNameTextBox");
    if (customerNameTextBox != null)
        customerNameTextBox.Watermark = Localization.Get("CustomerNamePlaceholder");
    
    var birthDateLabel = this.FindControl<TextBlock>("BirthDateLabel");
    if (birthDateLabel != null)
        birthDateLabel.Text = Localization.Get("BirthDate");
    
    var currentDateLabel = this.FindControl<TextBlock>("CurrentDateLabel");
    if (currentDateLabel != null)
        currentDateLabel.Text = Localization.Get("CurrentDate");
    
    var savingsCapitalLabel = this.FindControl<TextBlock>("SavingsCapitalLabel");
    if (savingsCapitalLabel != null)
        savingsCapitalLabel.Text = Localization.Get("SavingsCapital");
    
    var sparkapitalTextBox = this.FindControl<TextBox>("SparkapitalTextBox");
    if (sparkapitalTextBox != null)
        sparkapitalTextBox.Watermark = Localization.Get("SavingsCapitalPlaceholder");
    
    var interestRatesTitle = this.FindControl<TextBlock>("InterestRatesTitle");
    if (interestRatesTitle != null)
        interestRatesTitle.Text = Localization.Get("InterestRatesParameters");
    
    var normalInterestRateLabel = this.FindControl<TextBlock>("NormalInterestRateLabel");
    if (normalInterestRateLabel != null)
        normalInterestRateLabel.Text = Localization.Get("NormalInterestRate");
    
    var increasedInterestRateLabel = this.FindControl<TextBlock>("IncreasedInterestRateLabel");
    if (increasedInterestRateLabel != null)
        increasedInterestRateLabel.Text = Localization.Get("IncreasedInterestRate");
    
    var withholdingTaxLabel = this.FindControl<TextBlock>("WithholdingTaxLabel");
    if (withholdingTaxLabel != null)
        withholdingTaxLabel.Text = Localization.Get("WithholdingTax");
    
    // Buttons
    var calculateButton = this.FindControl<Button>("CalculateButton");
    if (calculateButton != null)
        calculateButton.Content = Localization.Get("Calculate");
    
    var resetButton = this.FindControl<Button>("ResetButton");
    if (resetButton != null)
        resetButton.Content = Localization.Get("Reset");
    
    // Instructions
    var instructionsTitle = this.FindControl<TextBlock>("InstructionsTitle");
    if (instructionsTitle != null)
        instructionsTitle.Text = Localization.Get("Instructions");
    
    var instructionNote = this.FindControl<TextBlock>("InstructionNote");
    if (instructionNote != null)
        instructionNote.Text = Localization.Get("InstructionNote");
    
    // Error Section
    var errorHeaderText = this.FindControl<TextBlock>("ErrorHeaderText");
    if (errorHeaderText != null)
        errorHeaderText.Text = Localization.Get("ErrorHeader");
    
    var customerDataErrorTitle = this.FindControl<TextBlock>("CustomerDataErrorTitle");
    if (customerDataErrorTitle != null)
        customerDataErrorTitle.Text = Localization.Get("CustomerData");
    
    var errorBirthDateFutureText = this.FindControl<TextBlock>("ErrorBirthDateFutureText");
    if (errorBirthDateFutureText != null)
        errorBirthDateFutureText.Text = Localization.Get("ErrorBirthDateFuture");
    
    var errorCapitalPositiveText = this.FindControl<TextBlock>("ErrorCapitalPositiveText");
    if (errorCapitalPositiveText != null)
        errorCapitalPositiveText.Text = Localization.Get("ErrorCapitalPositive");
    
    var interestRatesErrorTitle = this.FindControl<TextBlock>("InterestRatesErrorTitle");
    if (interestRatesErrorTitle != null)
        interestRatesErrorTitle.Text = Localization.Get("InterestRatesParameters");
    
    var errorInvalidNumberText = this.FindControl<TextBlock>("ErrorInvalidNumberText");
    if (errorInvalidNumberText != null)
        errorInvalidNumberText.Text = Localization.Get("ErrorInvalidNumber");
    
    var errorTaxRangeText = this.FindControl<TextBlock>("ErrorTaxRangeText");
    if (errorTaxRangeText != null)
        errorTaxRangeText.Text = Localization.Get("ErrorTaxRange");
    
    var calculateButtonDisabled = this.FindControl<Button>("CalculateButtonDisabled");
    if (calculateButtonDisabled != null)
        calculateButtonDisabled.Content = Localization.Get("Calculate");
    
    var errorOverviewTitle = this.FindControl<TextBlock>("ErrorOverviewTitle");
    if (errorOverviewTitle != null)
        errorOverviewTitle.Text = Localization.Get("ErrorOverview");
    
    var errorBirthDateBulletText = this.FindControl<TextBlock>("ErrorBirthDateBulletText");
    if (errorBirthDateBulletText != null)
        errorBirthDateBulletText.Text = Localization.Get("ErrorBirthDateBullet");
    
    var errorCapitalBulletText = this.FindControl<TextBlock>("ErrorCapitalBulletText");
    if (errorCapitalBulletText != null)
        errorCapitalBulletText.Text = Localization.Get("ErrorCapitalBullet");
    
    var errorInterestBulletText = this.FindControl<TextBlock>("ErrorInterestBulletText");
    if (errorInterestBulletText != null)
        errorInterestBulletText.Text = Localization.Get("ErrorInterestBullet");
    
    var errorTaxBulletText = this.FindControl<TextBlock>("ErrorTaxBulletText");
    if (errorTaxBulletText != null)
        errorTaxBulletText.Text = Localization.Get("ErrorTaxBullet");
    
    var errorCorrectFieldsText = this.FindControl<TextBlock>("ErrorCorrectFieldsText");
    if (errorCorrectFieldsText != null)
        errorCorrectFieldsText.Text = Localization.Get("ErrorCorrectFields");
    
    var errorTroubleshootingTipsTitle = this.FindControl<TextBlock>("ErrorTroubleshootingTipsTitle");
    if (errorTroubleshootingTipsTitle != null)
        errorTroubleshootingTipsTitle.Text = Localization.Get("ErrorTroubleshootingTips");
    
    var errorTipDateText = this.FindControl<TextBlock>("ErrorTipDateText");
    if (errorTipDateText != null)
        errorTipDateText.Text = Localization.Get("ErrorTipDate");
    
    var errorTipPositiveText = this.FindControl<TextBlock>("ErrorTipPositiveText");
    if (errorTipPositiveText != null)
        errorTipPositiveText.Text = Localization.Get("ErrorTipPositive");
    
    var errorTipDecimalText = this.FindControl<TextBlock>("ErrorTipDecimalText");
    if (errorTipDecimalText != null)
        errorTipDecimalText.Text = Localization.Get("ErrorTipDecimal");
    
    var errorTipPercentText = this.FindControl<TextBlock>("ErrorTipPercentText");
    if (errorTipPercentText != null)
        errorTipPercentText.Text = Localization.Get("ErrorTipPercent");
    
    // Results Section
    var calculationSuccessfulText = this.FindControl<TextBlock>("CalculationSuccessfulText");
    if (calculationSuccessfulText != null)
        calculationSuccessfulText.Text = Localization.Get("CalculationSuccessful");
    
    var calculationBasisTitle = this.FindControl<TextBlock>("CalculationBasisTitle");
    if (calculationBasisTitle != null)
        calculationBasisTitle.Text = Localization.Get("CalculationBasis");
    
    var newCalculationButton = this.FindControl<Button>("NewCalculationButton");
    if (newCalculationButton != null)
        newCalculationButton.Content = Localization.Get("NewCalculation");
    
    var customerInformationTitle = this.FindControl<TextBlock>("CustomerInformationTitle");
    if (customerInformationTitle != null)
        customerInformationTitle.Text = Localization.Get("CustomerInformation");
    
    var customerInfoText = this.FindControl<TextBlock>("CustomerInfoText");
    if (customerInfoText != null)
        customerInfoText.Text = Localization.Get("CustomerInfoText");
    
    var detailedResultTitle = this.FindControl<TextBlock>("DetailedResultTitle");
    if (detailedResultTitle != null)
        detailedResultTitle.Text = Localization.Get("DetailedResult");
    
    var calculationDetailsLabel = this.FindControl<TextBlock>("CalculationDetailsLabel");
    if (calculationDetailsLabel != null)
        calculationDetailsLabel.Text = Localization.Get("CalculationDetails");
    
    var summaryLabel = this.FindControl<TextBlock>("SummaryLabel");
    if (summaryLabel != null)
        summaryLabel.Text = Localization.Get("Summary");
var instruction1Text = this.FindControl<TextBlock>("Instruction1Text");
if (instruction1Text != null)
    instruction1Text.Text = Localization.Get("Instruction1Text");

var instruction2Text = this.FindControl<TextBlock>("Instruction2Text");
if (instruction2Text != null)
    instruction2Text.Text = Localization.Get("Instruction2Text");

var instruction3Text = this.FindControl<TextBlock>("Instruction3Text");
if (instruction3Text != null)
    instruction3Text.Text = Localization.Get("Instruction3Text");

var instruction4Text = this.FindControl<TextBlock>("Instruction4Text");
if (instruction4Text != null)
    instruction4Text.Text = Localization.Get("Instruction4Text");

var instruction5Text = this.FindControl<TextBlock>("Instruction5Text");
if (instruction5Text != null)
    instruction5Text.Text = Localization.Get("Instruction5Text");

var customerLabelRun = this.FindControl<TextBlock>("CustomerLabelRun");
if (customerLabelRun != null)
    customerLabelRun.Text = Localization.Get("Customer");

var capitalLabelRun = this.FindControl<TextBlock>("CapitalLabelRun");
if (capitalLabelRun != null)
    capitalLabelRun.Text = Localization.Get("Capital");

var birthdayLabelRun = this.FindControl<TextBlock>("BirthdayLabelRun");
if (birthdayLabelRun != null)
    birthdayLabelRun.Text = Localization.Get("Birthday");

var interestRatesLabelRun = this.FindControl<TextBlock>("InterestRatesLabelRun");
if (interestRatesLabelRun != null)
    interestRatesLabelRun.Text = Localization.Get("InterestRates");

var normalPeriodLabelRun = this.FindControl<TextBlock>("NormalPeriodLabelRun");
if (normalPeriodLabelRun != null)
    normalPeriodLabelRun.Text = Localization.Get("NormalPeriod");

var bonusPeriodLabelRun = this.FindControl<TextBlock>("BonusPeriodLabelRun");
if (bonusPeriodLabelRun != null)
    bonusPeriodLabelRun.Text = Localization.Get("BonusPeriod");

var grossInterestTotalLabelRun = this.FindControl<TextBlock>("GrossInterestTotalLabelRun");
if (grossInterestTotalLabelRun != null)
    grossInterestTotalLabelRun.Text = Localization.Get("GrossInterestTotal");

var withholdingTaxAmountLabelRun = this.FindControl<TextBlock>("WithholdingTaxAmountLabelRun");
if (withholdingTaxAmountLabelRun != null)
    withholdingTaxAmountLabelRun.Text = Localization.Get("WithholdingTaxAmount");

var netInterestLabelRun = this.FindControl<TextBlock>("NetInterestLabelRun");
if (netInterestLabelRun != null)
    netInterestLabelRun.Text = Localization.Get("NetInterest");
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