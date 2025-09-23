using System.Collections.Generic;

namespace MarchzinsBonusTool.Infrastructure
{
    /// <summary>
    /// Handles localization of UI strings.
    /// </summary>
    public static class Localization
    {
        private static Language currentLanguage = Language.German;
        
        private static readonly Dictionary<string, Dictionary<Language, string>> translations = 
            new Dictionary<string, Dictionary<Language, string>>
        {
            // Window Titles
            ["MainTitle"] = new() { 
                [Language.German] = "Marchzins-Bonus Tool",
                [Language.English] = "March Interest Bonus Tool"
            },
            ["Settings"] = new() { 
                [Language.German] = "Einstellungen",
                [Language.English] = "Settings"
            },

            // Main Form Sections
            ["CustomerData"] = new() {
                [Language.German] = "Kundendaten",
                [Language.English] = "Customer Data"
            },
            ["InterestRatesParameters"] = new() {
                [Language.German] = "Zinssätze & Parameter",
                [Language.English] = "Interest Rates & Parameters"
            },
            ["Instructions"] = new() {
                [Language.German] = "Anleitung",
                [Language.English] = "Instructions"
            },

            // Customer Data Fields
            ["CustomerName"] = new() {
                [Language.German] = "Kundenname (optional)",
                [Language.English] = "Customer Name (optional)"
            },
            ["CustomerNamePlaceholder"] = new() {
                [Language.German] = "Max Mustermann",
                [Language.English] = "John Doe"
            },
            ["BirthDate"] = new() {
                [Language.German] = "Geburtsdatum",
                [Language.English] = "Date of Birth"
            },
            ["CurrentDate"] = new() {
                [Language.German] = "Aktuelles Datum",
                [Language.English] = "Current Date"
            },
            ["SavingsCapital"] = new() {
                [Language.German] = "Sparkapital",
                [Language.English] = "Savings Capital"
            },
            ["SavingsCapitalPlaceholder"] = new() {
                [Language.German] = "100'000.00",
                [Language.English] = "100,000.00"
            },

            // Interest Rates Fields
            ["NormalInterestRate"] = new() {
                [Language.German] = "Normaler Zinssatz (%)",
                [Language.English] = "Normal Interest Rate (%)"
            },
            ["IncreasedInterestRate"] = new() {
                [Language.German] = "Erhöhter Zinssatz (%)",
                [Language.English] = "Increased Interest Rate (%)"
            },
            ["WithholdingTax"] = new() {
                [Language.German] = "Verrechnungssteuer (%)",
                [Language.English] = "Withholding Tax (%)"
            },

            // Buttons
            ["Calculate"] = new() {
                [Language.German] = "Berechnen",
                [Language.English] = "Calculate"
            },
            ["Reset"] = new() {
                [Language.German] = "Zurücksetzen",
                [Language.English] = "Reset"
            },
            ["NewCalculation"] = new() {
                [Language.German] = "Neue Berechnung",
                [Language.English] = "New Calculation"
            },

            // Instructions Text
            ["Instruction1"] = new() {
                [Language.German] = "Kundendaten eingeben",
                [Language.English] = "Enter customer data"
            },
            ["Instruction2"] = new() {
                [Language.German] = "Geburtsdatum im aktuellen Monat wählen",
                [Language.English] = "Select birth date in current month"
            },
            ["Instruction3"] = new() {
                [Language.German] = "Sparkapital eingeben",
                [Language.English] = "Enter savings capital"
            },
            ["Instruction4"] = new() {
                [Language.German] = "Zinssätze überprüfen",
                [Language.English] = "Check interest rates"
            },
            ["Instruction5"] = new() {
                [Language.German] = "Berechnen klicken",
                [Language.English] = "Click Calculate"
            },
            ["InstructionNote"] = new() {
                [Language.German] = "Der Marchzins-Bonus gilt vom 1. des Monats bis zum Geburtstag.",
                [Language.English] = "The March interest bonus applies from the 1st of the month until the birthday."
            },

            // Error Messages
            ["ErrorHeader"] = new() {
                [Language.German] = "Bitte korrigieren Sie die folgenden Fehler, bevor Sie fortfahren:",
                [Language.English] = "Please correct the following errors before proceeding:"
            },
            ["ErrorOverview"] = new() {
                [Language.German] = "Fehlerübersicht",
                [Language.English] = "Error Overview"
            },
            ["ErrorBirthDateFuture"] = new() {
                [Language.German] = "Geburtsdatum darf nicht in der Zukunft liegen",
                [Language.English] = "Birth date cannot be in the future"
            },
            ["ErrorCapitalPositive"] = new() {
                [Language.German] = "Kapital muss positiv sein",
                [Language.English] = "Capital must be positive"
            },
            ["ErrorInvalidNumber"] = new() {
                [Language.German] = "Bitte geben Sie eine gültige Zahl ein",
                [Language.English] = "Please enter a valid number"
            },
            ["ErrorTaxRange"] = new() {
                [Language.German] = "Steuersatz muss zwischen 0-100% liegen",
                [Language.English] = "Tax rate must be between 0-100%"
            },
            ["ErrorBirthDateBullet"] = new() {
                [Language.German] = "• Geburtsdatum: Zukunftsdatum nicht erlaubt",
                [Language.English] = "• Birth Date: Future date not allowed"
            },
            ["ErrorCapitalBullet"] = new() {
                [Language.German] = "• Sparkapital: Negative Werte sind ungültig",
                [Language.English] = "• Savings Capital: Negative values are invalid"
            },
            ["ErrorInterestBullet"] = new() {
                [Language.German] = "• Bonus-Zinssatz: Nur Zahlen erlaubt",
                [Language.English] = "• Bonus Interest Rate: Only numbers allowed"
            },
            ["ErrorTaxBullet"] = new() {
                [Language.German] = "• Steuersatz: Muss zwischen 0-100% sein",
                [Language.English] = "• Tax Rate: Must be between 0-100%"
            },
            ["ErrorCorrectFields"] = new() {
                [Language.German] = "Korrigieren Sie diese Felder, um fortzufahren.",
                [Language.English] = "Correct these fields to continue."
            },
            ["ErrorTroubleshootingTips"] = new() {
                [Language.German] = "Tipps zur Fehlerbehebung:",
                [Language.English] = "Troubleshooting Tips:"
            },
            ["ErrorTipDate"] = new() {
                [Language.German] = "• Datum im Format TT.MM.JJJJ",
                [Language.English] = "• Date in DD.MM.YYYY format"
            },
            ["ErrorTipPositive"] = new() {
                [Language.German] = "• Nur positive Zahlen für Kapital",
                [Language.English] = "• Only positive numbers for capital"
            },
            ["ErrorTipDecimal"] = new() {
                [Language.German] = "• Dezimalzahlen mit Punkt (1.50)",
                [Language.English] = "• Decimal numbers with dot (1.50)"
            },
            ["ErrorTipPercent"] = new() {
                [Language.German] = "• Prozentsätze ohne % eingeben",
                [Language.English] = "• Enter percentages without % sign"
            },

            // Results Section
            ["CalculationSuccessful"] = new() {
                [Language.German] = "Berechnung erfolgreich abgeschlossen!",
                [Language.English] = "Calculation completed successfully!"
            },
            ["CalculationBasis"] = new() {
                [Language.German] = "Berechnungsgrundlage",
                [Language.English] = "Calculation Basis"
            },
            ["DetailedResult"] = new() {
                [Language.German] = "Detailliertes Ergebnis",
                [Language.English] = "Detailed Result"
            },
            ["Customer"] = new() {
                [Language.German] = "Kunde:",
                [Language.English] = "Customer:"
            },
            ["Capital"] = new() {
                [Language.German] = "Kapital:",
                [Language.English] = "Capital:"
            },
            ["Birthday"] = new() {
                [Language.German] = "Geburtstag:",
                [Language.English] = "Birthday:"
            },
            ["InterestRates"] = new() {
                [Language.German] = "Zinssätze:",
                [Language.English] = "Interest Rates:"
            },
            ["CustomerInformation"] = new() {
                [Language.German] = "Kundeninformation",
                [Language.English] = "Customer Information"
            },
            ["CustomerInfoText"] = new() {
                [Language.German] = "Der Marchzins-Bonus wurde erfolgreich berechnet. Bitte archivieren Sie diese Berechnung für Ihre Unterlagen.",
                [Language.English] = "The March interest bonus has been successfully calculated. Please archive this calculation for your records."
            },
            ["CalculationDetails"] = new() {
                [Language.German] = "Berechnungsdetails:",
                [Language.English] = "Calculation Details:"
            },
            ["NormalPeriod"] = new() {
                [Language.German] = "Normal-Periode:",
                [Language.English] = "Normal Period:"
            },
            ["BonusPeriod"] = new() {
                [Language.German] = "Bonus-Periode:",
                [Language.English] = "Bonus Period:"
            },
            ["Summary"] = new() {
                [Language.German] = "Zusammenfassung:",
                [Language.English] = "Summary:"
            },
            ["GrossInterestTotal"] = new() {
                [Language.German] = "Brutto-Zinsen gesamt:",
                [Language.English] = "Gross Interest Total:"
            },
            ["WithholdingTaxAmount"] = new() {
                [Language.German] = "Verrechnungssteuer (35%): -",
                [Language.English] = "Withholding Tax (35%): -"
            },
            ["NetInterest"] = new() {
                [Language.German] = "NETTO-ZINSEN:",
                [Language.English] = "NET INTEREST:"
            },

            // Language Codes
            ["LanguageDE"] = new() {
                [Language.German] = "DE",
                [Language.English] = "DE"
            },
            ["LanguageEN"] = new() {
                [Language.German] = "EN",
                [Language.English] = "EN"
            },

            // Common Terms
            ["Yes"] = new() {
                [Language.German] = "Ja",
                [Language.English] = "Yes"
            },
            ["No"] = new() {
                [Language.German] = "Nein",
                [Language.English] = "No"
            },
            ["OK"] = new() {
                [Language.German] = "OK",
                [Language.English] = "OK"
            },
            ["Cancel"] = new() {
                [Language.German] = "Abbrechen",
                [Language.English] = "Cancel"
            },
            ["Save"] = new() {
                [Language.German] = "Speichern",
                [Language.English] = "Save"
            },
            ["Close"] = new() {
                [Language.German] = "Schliessen",
                [Language.English] = "Close"
            },
            ["Instruction1Text"] = new() {
                [Language.German] = "Kundendaten eingeben",
                [Language.English] = "Enter customer data"
            },
            ["Instruction2Text"] = new() {
                [Language.German] = "Geburtsdatum im aktuellen Monat wählen", 
                [Language.English] = "Select birth date in current month"
            },
            ["Instruction3Text"] = new() {
                [Language.German] = "Sparkapital eingeben",
                [Language.English] = "Enter savings capital"
            },
            ["Instruction4Text"] = new() {
                [Language.German] = "Zinssätze überprüfen",
                [Language.English] = "Check interest rates"
            },
            ["Instruction5Text"] = new() {
                [Language.German] = "Berechnen klicken",
                [Language.English] = "Click Calculate"
            },
            ["CustomerRun"] = new() {
                [Language.German] = "Kunde:",
                [Language.English] = "Customer:"
            },
            ["CapitalRun"] = new() {
                [Language.German] = "Kapital:",
                [Language.English] = "Capital:"
            },
            ["BirthdayRun"] = new() {
                [Language.German] = "Geburtstag:",
                [Language.English] = "Birthday:"
            },
            ["InterestRatesRun"] = new() {
                [Language.German] = "Zinssätze:",
                [Language.English] = "Interest Rates:"
            },
            ["NormalPeriodRun"] = new() {
                [Language.German] = "Normal-Periode:",
                [Language.English] = "Normal Period:"
            },
            ["BonusPeriodRun"] = new() {
                [Language.German] = "Bonus-Periode:",
                [Language.English] = "Bonus Period:"
            },
            ["GrossInterestTotalRun"] = new() {
                [Language.German] = "Brutto-Zinsen gesamt:",
                [Language.English] = "Gross Interest Total:"
            },
            ["WithholdingTaxAmountRun"] = new() {
                [Language.German] = "Verrechnungssteuer (35%): -",
                [Language.English] = "Withholding Tax (35%): -"
            },
            ["NetInterestRun"] = new() {
                [Language.German] = "NETTO-ZINSEN:",
                [Language.English] = "NET INTEREST:"
            },
            ["CalculationTimestamp"] = new() {
                [Language.German] = "Berechnet am {date} um {time} Uhr",
                [Language.English] = "Calculated on {date} at {time}"
            },
            ["Days"] = new() {
                [Language.German] = "Tage",
                [Language.English] = "Days"
            },
            ["Language"] = new() {
                [Language.German] = "Sprache",
                [Language.English] = "Language"
            },
            ["DefaultCurrency"] = new() {
                [Language.German] = "Standard-Währung",
                [Language.English] = "Default Currency"
            },
            ["NumberFormat"] = new() {
                [Language.German] = "Zahlenformat",
                [Language.English] = "Number Format"
            },
            ["ThousandsSeparator"] = new() {
                [Language.German] = "Tausendertrennzeichen",
                [Language.English] = "Thousands Separator"
            },
            ["DecimalSeparator"] = new() {
                [Language.German] = "Dezimaltrennzeichen",
                [Language.English] = "Decimal Separator"
            },
            ["DefaultValues"] = new() {
                [Language.German] = "Voreinstellungen",
                [Language.English] = "Default Values"
            },
            ["NormalPercent"] = new() {
                [Language.German] = "Normal %",
                [Language.English] = "Normal %"
            },
            ["BonusPercent"] = new() {
                [Language.German] = "Bonus %",
                [Language.English] = "Bonus %"
            },
            ["TaxPercent"] = new() {
                [Language.German] = "Steuer %",
                [Language.English] = "Tax %"
            },
            ["ResetToDefaults"] = new() {
                [Language.German] = "Zurücksetzen",
                [Language.English] = "Reset to Defaults"
            }
        };

        /// <summary>
        /// Gets a localized string for the given key.
        /// </summary>
        public static string Get(string key)
        {
            if (translations.ContainsKey(key) && translations[key].ContainsKey(currentLanguage))
            {
                return translations[key][currentLanguage];
            }
            return key; // Return unchanged word
        }

        /// <summary>
        /// Sets the current language.
        /// </summary>
        public static void SetLanguage(Language language)
        {
            currentLanguage = language;
        }
    }
}