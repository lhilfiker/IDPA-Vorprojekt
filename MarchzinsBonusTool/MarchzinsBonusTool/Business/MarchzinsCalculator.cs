using System;
using System.Collections.Generic;

namespace MarchzinsBonusTool.Business
{
    /// <summary>
    /// Core business logic for Marchzins calculations.
    /// This class is immutable after construction.
    /// </summary>
    public class MarchzinsCalculator
    {
        private readonly decimal sparkapital;
        private readonly DateTime geburtsdatum;
        private readonly decimal normalerZinssatz;
        private readonly decimal bonusZinssatz;
        private readonly decimal steuersatz;
        private readonly DateTime berechnungsDatum;
        private readonly string? kundenName;

        public MarchzinsCalculator(
            decimal sparkapital,
            DateTime geburtsdatum,
            decimal normalerZinssatz,
            decimal bonusZinssatz,
            decimal steuersatz,
            DateTime berechnungsDatum,
            string? kundenName = null)
        {
            this.sparkapital = sparkapital;
            this.geburtsdatum = geburtsdatum;
            this.normalerZinssatz = normalerZinssatz;
            this.bonusZinssatz = bonusZinssatz;
            this.steuersatz = steuersatz;
            this.berechnungsDatum = berechnungsDatum;
            this.kundenName = kundenName;
        }

        /// <summary>
        /// Gets the number of days in the bonus period (from 1st of month to birthday).
        /// </summary>
        public int GetBonusPeriodeTage()
        {
            // Check if the Birthday month is this month
            if (geburtsdatum.Month != berechnungsDatum.Month || geburtsdatum.Year != berechnungsDatum.Year)
            {
                return 0; // No Bonus if not the same month
            }

            // From the 1. Month to the Birthday (inclusive)
            DateTime monatsanfang = new DateTime(berechnungsDatum.Year, berechnungsDatum.Month, 1);
            DateTime geburtstagImAktuellenJahr = new DateTime(berechnungsDatum.Year, geburtsdatum.Month, geburtsdatum.Day);
            
            return (geburtstagImAktuellenJahr - monatsanfang).Days + 1; // +1 to include the birthday
        }

        /// <summary>
        /// Gets the gross interest for the normal period.
        /// </summary>
        public decimal GetBruttoZinsenNormal()
        {
            int bonusTage = GetBonusPeriodeTage();
            int tageImMonat = DateTime.DaysInMonth(berechnungsDatum.Year, berechnungsDatum.Month);
            int normaleTage = tageImMonat - bonusTage;

            // When no normal days, then 0 interest
            if (normaleTage <= 0)
            {
                return 0;
            }

            // Interest = Capital * Normal Interest Rate * Days / 365
            return sparkapital * normalerZinssatz / 100 * normaleTage / 365;
        }

        /// <summary>
        /// Gets the gross interest for the bonus period.
        /// </summary>
        public decimal GetBruttoZinsenBonus()
        {
            int bonusTage = GetBonusPeriodeTage();

            // if no bonus days, then 0 interest
            if (bonusTage <= 0)
            {
                return 0;
            }

            // Interest = Capital * Bonus Interest Rate * Days / 365
            return sparkapital * bonusZinssatz / 100 * bonusTage / 365;
        }

        /// <summary>
        /// Gets the total gross interest (normal + bonus).
        /// </summary>
        public decimal GetBruttoZinsenTotal()
        {
            return GetBruttoZinsenNormal() + GetBruttoZinsenBonus();
        }

        /// <summary>
        /// Gets the tax deduction amount.
        /// </summary>
        public decimal GetSteuerabzug()
        {
            return GetBruttoZinsenTotal() * steuersatz / 100;
        }

        /// <summary>
        /// Gets the net interest after tax deduction.
        /// </summary>
        public decimal GetNettoZinsen()
        {
            return GetBruttoZinsenTotal() - GetSteuerabzug();
        }

        /// <summary>
        /// Gets all calculation results as a dictionary.
        /// </summary>
        public Dictionary<string, object> GetAllResults()
        {
            return new Dictionary<string, object>
            {
                { "Sparkapital", sparkapital },
                { "KundenName", kundenName ?? "Unbekannt" },
                { "Geburtsdatum", geburtsdatum },
                { "BerechnungsDatum", berechnungsDatum },
                { "NormalerZinssatz", normalerZinssatz },
                { "BonusZinssatz", bonusZinssatz },
                { "Steuersatz", steuersatz },
                { "BonusPeriodeTage", GetBonusPeriodeTage() },
                { "TageImMonat", DateTime.DaysInMonth(berechnungsDatum.Year, berechnungsDatum.Month) },
                { "NormalePeriodeTage", DateTime.DaysInMonth(berechnungsDatum.Year, berechnungsDatum.Month) - GetBonusPeriodeTage() },
                { "BruttoZinsenNormal", Math.Round(GetBruttoZinsenNormal(), 2) },
                { "BruttoZinsenBonus", Math.Round(GetBruttoZinsenBonus(), 2) },
                { "BruttoZinsenTotal", Math.Round(GetBruttoZinsenTotal(), 2) },
                { "Steuerabzug", Math.Round(GetSteuerabzug(), 2) },
                { "NettoZinsen", Math.Round(GetNettoZinsen(), 2) }
            };
        }
    }
}