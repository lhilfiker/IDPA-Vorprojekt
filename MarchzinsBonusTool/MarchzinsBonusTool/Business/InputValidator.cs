using System;
using System.Globalization;
using System.Text.RegularExpressions;
using MarchzinsBonusTool.ViewModels;

namespace MarchzinsBonusTool.Business
{
    /// <summary>
    /// Validates user input for the Marchzins calculations.
    /// </summary>
    public class InputValidator
    {
        /// <summary>
        /// Validates the Sparkapital input.
        /// </summary>
        public ValidationResult ValidateSparkapital(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return new ValidationResult(false, "Sparkapital ist erforderlich");

            if (!IsValidDecimal(input))
                return new ValidationResult(false, "Ungültiges Zahlenformat");

            decimal value = ParseDecimal(input);
            if (value <= 0)
                return new ValidationResult(false, "Sparkapital muss grösser als 0 sein");

            if (value > 10_000_000_000)
                return new ValidationResult(false, "Sparkapital ist zu gross");

            return new ValidationResult(true, string.Empty);
        }

        /// <summary>
        /// Validates a interest rate input.
        /// </summary>
        public ValidationResult ValidateZinssatz(string input, string fieldName)
        {
            if (string.IsNullOrWhiteSpace(input))
                return new ValidationResult(false, $"{fieldName} ist erforderlich");

            if (!IsValidDecimal(input))
                return new ValidationResult(false, "Ungültiges Zahlenformat");

            decimal value = ParseDecimal(input);
            if (value < 0)
                return new ValidationResult(false, $"{fieldName} darf nicht negativ sein");

            if (value > 100)
                return new ValidationResult(false, $"{fieldName} darf nicht grösser als 100% sein");

            return new ValidationResult(true, string.Empty);
        }

        /// <summary>
        /// Validates the tax rate input.
        /// </summary>
        public ValidationResult ValidateSteuersatz(string input)
        {
            return ValidateZinssatz(input, "Steuersatz");
        }

        /// <summary>
        /// Validates a date input.
        /// </summary>
        public ValidationResult ValidateDate(DateTime date)
        {
            if (date < new DateTime(1900, 1, 1))
                return new ValidationResult(false, "Datum ist zu weit in der Vergangenheit");

            if (date > DateTime.Now.AddYears(10))
                return new ValidationResult(false, "Datum ist zu weit in der Zukunft");

            return new ValidationResult(true, string.Empty);
        }

        /// <summary>
        /// Checks if all inputs in the model are valid for calculation.
        /// </summary>
        public bool IsValidCalculation(MainViewModel model)
        {
            if (model == null)
                return false;

            var sparkapitalResult = ValidateSparkapital(model.SparkapitalInput);
            if (!sparkapitalResult.IsValid)
                return false;

            var normalZinsResult = ValidateZinssatz(model.NormalerZinssatzInput, "Normaler Zinssatz");
            if (!normalZinsResult.IsValid)
                return false;

            var bonusZinsResult = ValidateZinssatz(model.BonusZinssatzInput, "Bonus Zinssatz");
            if (!bonusZinsResult.IsValid)
                return false;

            var steuerResult = ValidateSteuersatz(model.SteuersatzInput);
            if (!steuerResult.IsValid)
                return false;

            var dateResult = ValidateDate(model.GeburtsdatumInput);
            if (!dateResult.IsValid)
                return false;

            return true;
        }

        /// <summary>
        /// Checks if a string represents a valid decimal number.
        /// </summary>
        private bool IsValidDecimal(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return false;

            // Remove common thousands separators and normalize decimal separator
            string normalized = input.Replace("'", "")
                                    .Replace(" ", "")
                                    .Replace(",", ".");

            // Check for multiple decimal points
            int decimalCount = 0;
            foreach (char c in normalized)
            {
                if (c == '.')
                    decimalCount++;
            }
            if (decimalCount > 1)
                return false;

            return decimal.TryParse(normalized, NumberStyles.Any, CultureInfo.InvariantCulture, out _);
        }

        /// <summary>
        /// Parses a decimal from a string input.
        /// </summary>
        private decimal ParseDecimal(string input)
        {
            string normalized = input.Replace("'", "")
                                    .Replace(" ", "")
                                    .Replace(",", ".");

            decimal.TryParse(normalized, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal result);
            return result;
        }
    }
}
