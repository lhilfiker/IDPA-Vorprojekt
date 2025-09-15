using System;
using System.Collections.Generic;
using Xunit;
using MarchzinsBonusTool.Business;

namespace MarchzinsBonusTool.Tests.Business
{
    public class MarchzinsCalculatorTests
    {
        private const decimal SPARKAPITAL = 10000m;
        private const decimal NORMALER_ZINSSATZ = 2.0m; // 2%
        private const decimal BONUS_ZINSSATZ = 4.0m;    // 4%
        private const decimal STEUERSATZ = 35.0m;       // 35%

        private readonly DateTime GEBURTSTAG = new DateTime(2024, 3, 15);
        private readonly DateTime BERECHNUNGSDATUM = new DateTime(2024, 3, 20);

        [Fact]
        public void GetBonusPeriodeTage_StandardSzenario_ReturnsCorrectDays()
        {
            var calculator = CreateCalculator();
            int bonusTage = calculator.GetBonusPeriodeTage();
            Assert.Equal(15, bonusTage); // 1. bis 15. März
        }

        [Fact]
        public void GetBruttoZinsenNormal_AndBonus_ReturnsCorrectAmounts()
        {
            var calculator = CreateCalculator();
            decimal normal = calculator.GetBruttoZinsenNormal();
            decimal bonus = calculator.GetBruttoZinsenBonus();

            decimal expectedNormal = 10000m * 2.0m / 100 * 16 / 365;
            decimal expectedBonus = 10000m * 4.0m / 100 * 15 / 365;

            Assert.Equal(expectedNormal, normal);
            Assert.Equal(expectedBonus, bonus);
        }

        [Fact]
        public void GetNettoZinsen_ReturnsBruttoMinusTax()
        {
            var calculator = CreateCalculator();
            decimal bruttoTotal = calculator.GetBruttoZinsenTotal();
            decimal steuerabzug = calculator.GetSteuerabzug();
            decimal nettoZinsen = calculator.GetNettoZinsen();

            Assert.Equal(bruttoTotal - steuerabzug, nettoZinsen);
        }

        [Fact]
        public void GetAllResults_ContainsExpectedKeys()
        {
            var calculator = CreateCalculator(kundenName: "Max Mustermann");
            var results = calculator.GetAllResults();

            Assert.True(results.ContainsKey("Sparkapital"));
            Assert.True(results.ContainsKey("BruttoZinsenNormal"));
            Assert.True(results.ContainsKey("NettoZinsen"));
        }

        [Fact]
        public void Calculator_ZeroSparkapital_ReturnsZeroInterest()
        {
            var calculator = CreateCalculator(sparkapital: 0m);
            Assert.Equal(0m, calculator.GetBruttoZinsenTotal());
            Assert.Equal(0m, calculator.GetNettoZinsen());
        }

        private MarchzinsCalculator CreateCalculator(
            decimal sparkapital = SPARKAPITAL,
            DateTime? geburtstag = null,
            decimal normalerZinssatz = NORMALER_ZINSSATZ,
            decimal bonusZinssatz = BONUS_ZINSSATZ,
            decimal steuersatz = STEUERSATZ,
            DateTime? berechnungsDatum = null,
            string? kundenName = null)
        {
            return new MarchzinsCalculator(
                sparkapital,
                geburtstag ?? GEBURTSTAG,
                normalerZinssatz,
                bonusZinssatz,
                steuersatz,
                berechnungsDatum ?? BERECHNUNGSDATUM,
                kundenName
            );
        }
    }
}
