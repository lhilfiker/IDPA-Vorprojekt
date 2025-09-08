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
        }

        /// <summary>
        /// Gets the gross interest for the normal period.
        /// </summary>
        public decimal GetBruttoZinsenNormal()
        {
        }

        /// <summary>
        /// Gets the gross interest for the bonus period.
        /// </summary>
        public decimal GetBruttoZinsenBonus()
        {
        }

        /// <summary>
        /// Gets the total gross interest (normal + bonus).
        /// </summary>
        public decimal GetBruttoZinsenTotal()
        {
        }

        /// <summary>
        /// Gets the tax deduction amount.
        /// </summary>
        public decimal GetSteuerabzug()
        {
        }

        /// <summary>
        /// Gets the net interest after tax deduction.
        /// </summary>
        public decimal GetNettoZinsen()
        {
        }

        /// <summary>
        /// Gets all calculation results as a dictionary.
        /// </summary>
        public Dictionary<string, object> GetAllResults()
        {
        }
    }
}