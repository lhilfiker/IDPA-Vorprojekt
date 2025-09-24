using System;
using System.Globalization;
using MarchzinsBonusTool.Infrastructure.Exceptions;

namespace MarchzinsBonusTool.Infrastructure
{
    /// <summary>
    /// Handles number formatting and parsing based on user settings.
    /// </summary>
    public class NumberFormatter
    {
        private readonly char thousandsSeparator;
        private readonly char decimalSeparator;
        private readonly string currencySymbol;
        private readonly CultureInfo culture;

        public NumberFormatter(Settings settings)
        {
            thousandsSeparator = settings.ThousandsSeparator;
            decimalSeparator = settings.DecimalSeparator;
            currencySymbol = settings.Currency;
            culture = CreateCustomCulture();
        }

        /// <summary>
        /// Parses user input to a decimal value.
        /// </summary>
        public decimal ParseInput(string userInput)
        {
            if (string.IsNullOrWhiteSpace(userInput))
            {
                throw new ValidationException(
                    "UserInput",
                    "Input cannot be null or empty.",
                    "Eingabe darf nicht leer sein."
                );
            }

            // clean up input
            string cleanInput = userInput.Trim();
            cleanInput = cleanInput.Replace(currencySymbol, "").Trim();
            cleanInput = cleanInput.Replace("%", "").Trim();

            // change thousands separator to nothing
            if (thousandsSeparator != '\0')
            {
                cleanInput = cleanInput.Replace(thousandsSeparator.ToString(), "");
            }

            // change decimal separator to '.'
            if (decimalSeparator != '.')
            {
                cleanInput = cleanInput.Replace(decimalSeparator, '.');
            }

            // Parse with invariant culture
            if (decimal.TryParse(cleanInput, NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign,
                               CultureInfo.InvariantCulture, out decimal result))
            {
                return result;
            }

            throw new ValidationException(
                "NumberFormat",
                $"Cannot parse '{userInput}' as a valid number.",
                "Ungültiges Zahlenformat. Verwenden Sie Dezimalzahlen wie 1.50 oder 100'000."
            );
        }

        /// <summary>
        /// Formats a decimal value as currency.
        /// </summary>
        public string FormatCurrency(decimal value)
        {
            // Format with 2 decimal places
            string formattedNumber = FormatDecimal(value, 2);

            // add currency symbol
            return $"{currencySymbol} {formattedNumber}";
        }

        /// <summary>
        /// Formats a decimal value as a percentage.
        /// </summary>
        public string FormatPercent(decimal value)
        {
            // Format with 2 decimal places (value is not multiplied by 100,
            // because it is assumed to already be a percentage)
            string formattedNumber = FormatDecimal(value, 2);
            return $"{formattedNumber}%";
        }

        /// <summary>
        /// Formats an integer value.
        /// </summary>
        public string FormatInteger(int value)
        {
            return FormatDecimal(value, 0);
        }

        /// <summary>
        /// Creates a custom culture based on user settings.
        /// </summary>
        private CultureInfo CreateCustomCulture()
        {
            // create a copy of invariant culture
            CultureInfo customCulture = (CultureInfo)CultureInfo.InvariantCulture.Clone();

            // Adjusting number formatting
            NumberFormatInfo numberFormat = (NumberFormatInfo)customCulture.NumberFormat.Clone();

            // Set decimal and thousands separators
            numberFormat.NumberDecimalSeparator = decimalSeparator.ToString();
            numberFormat.NumberGroupSeparator = thousandsSeparator.ToString();
            numberFormat.NumberGroupSizes = new int[] { 3 }; // Gruppierung in 3er-Blöcken

            // currency formatting
            numberFormat.CurrencySymbol = currencySymbol;
            numberFormat.CurrencyDecimalSeparator = decimalSeparator.ToString();
            numberFormat.CurrencyGroupSeparator = thousandsSeparator.ToString();
            numberFormat.CurrencyGroupSizes = new int[] { 3 };
            numberFormat.CurrencyDecimalDigits = 2;

            // prozent formatting
            numberFormat.PercentDecimalSeparator = decimalSeparator.ToString();
            numberFormat.PercentGroupSeparator = thousandsSeparator.ToString();
            numberFormat.PercentGroupSizes = new int[] { 3 };
            numberFormat.PercentDecimalDigits = 2;

            customCulture.NumberFormat = numberFormat;

            return customCulture;
        }

        /// <summary>
        /// Helper method to format decimal values with custom separators.
        /// </summary>
        private string FormatDecimal(decimal value, int decimalPlaces)
        {
            // Round to desired decimal places
            decimal roundedValue = Math.Round(value, decimalPlaces);

            // Format without thousands separator first
            string format = decimalPlaces > 0 ? $"F{decimalPlaces}" : "F0";
            string baseFormatted = roundedValue.ToString(format, CultureInfo.InvariantCulture);

            // Parts in integer and decimal parts
            string[] parts = baseFormatted.Split('.');
            string integerPart = parts[0];
            string decimalPart = parts.Length > 1 ? parts[1] : "";

            // Format integer part with thousands separator
            string formattedInteger = FormatIntegerPart(integerPart);

            // Combine with decimal part if present
            if (decimalPlaces > 0 && !string.IsNullOrEmpty(decimalPart))
            {
                return $"{formattedInteger}{decimalSeparator}{decimalPart}";
            }

            return formattedInteger;
        }

        /// <summary>
        /// Helper method to add thousands separators to integer part.
        /// </summary>
        private string FormatIntegerPart(string integerPart)
        {
            if (string.IsNullOrEmpty(integerPart) || thousandsSeparator == '\0')
            {
                return integerPart;
            }

            // Treat negative numbers
            bool isNegative = integerPart.StartsWith("-");
            if (isNegative)
            {
                integerPart = integerPart.Substring(1);
            }

            // Add thousands separators from right to left
            string result = "";
            for (int i = integerPart.Length - 1, count = 0; i >= 0; i--, count++)
            {
                if (count > 0 && count % 3 == 0)
                {
                    result = thousandsSeparator + result;
                }
                result = integerPart[i] + result;
            }

            return isNegative ? "-" + result : result;
        }
    }
}