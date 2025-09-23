using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using MarchzinsBonusTool.Infrastructure;
using MarchzinsBonusTool.ViewModels;

namespace MarchzinsBonusTool.Views;

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

        var settingsTitle = this.FindControl<TextBlock>("SettingsTitle");
        if (settingsTitle != null)
            settingsTitle.Text = Localization.Get("Settings");

        var languageTitle = this.FindControl<TextBlock>("LanguageTitle");
        if (languageTitle != null)
            languageTitle.Text = Localization.Get("Language");

        var defaultCurrencyTitle = this.FindControl<TextBlock>("DefaultCurrencyTitle");
        if (defaultCurrencyTitle != null)
            defaultCurrencyTitle.Text = Localization.Get("DefaultCurrency");

        var numberFormatTitle = this.FindControl<TextBlock>("NumberFormatTitle");
        if (numberFormatTitle != null)
            numberFormatTitle.Text = Localization.Get("NumberFormat");

        var thousandsSeparatorLabel = this.FindControl<TextBlock>("ThousandsSeparatorLabel");
        if (thousandsSeparatorLabel != null)
            thousandsSeparatorLabel.Text = Localization.Get("ThousandsSeparator");

        var decimalSeparatorLabel = this.FindControl<TextBlock>("DecimalSeparatorLabel");
        if (decimalSeparatorLabel != null)
            decimalSeparatorLabel.Text = Localization.Get("DecimalSeparator");

        var defaultValuesTitle = this.FindControl<TextBlock>("DefaultValuesTitle");
        if (defaultValuesTitle != null)
            defaultValuesTitle.Text = Localization.Get("DefaultValues");

        var normalPercentLabel = this.FindControl<TextBlock>("NormalPercentLabel");
        if (normalPercentLabel != null)
            normalPercentLabel.Text = Localization.Get("NormalPercent");

        var bonusPercentLabel = this.FindControl<TextBlock>("BonusPercentLabel");
        if (bonusPercentLabel != null)
            bonusPercentLabel.Text = Localization.Get("BonusPercent");

        var taxPercentLabel = this.FindControl<TextBlock>("TaxPercentLabel");
        if (taxPercentLabel != null)
            taxPercentLabel.Text = Localization.Get("TaxPercent");

        var resetButton = this.FindControl<Button>("ResetButton");
        if (resetButton != null)
            resetButton.Content = Localization.Get("ResetToDefaults");

        var saveButton = this.FindControl<Button>("SaveButton");
        if (saveButton != null)
            saveButton.Content = Localization.Get("Save");

        var cancelButton = this.FindControl<Button>("CancelButton");
        if (cancelButton != null)
            cancelButton.Content = Localization.Get("Cancel");
    }

    protected override void OnClosed(EventArgs e)
    {
        base.OnClosed(e);
    }
}